using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component.Utility;
using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Component.Global;
using static Component.Utility.PushNotifications;

namespace BLL.Implementation
{
    public class BONotification : IBONotification
    {
        public DataContext _dbContext { get; set; }


        public BONotification(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<NotificationDTO> GetNotificationByUserId(int id)
        {
            //List<Notification> notificationsList = _dbContext.Notifications.Where(x => x.User_ID.HasValue && x.User_ID.Value == id && x.Status == 0).OrderByDescending(x => x.Id).ToList();
            //List<NotificationDTO> notificationsResponseList = Mapper.Map<List<Notification>, List<NotificationDTO>>(notificationsList);
            //return notificationsResponseList;
            return new List<NotificationDTO>();
        }
        public List<AdminNotificationBindingModel> GetAdminNotifications()
        {
            List<Notification> notificationsList = _dbContext.Notifications.Where(x => /*x.AdminNotification_Id.HasValue  &&*/ x.Type == (int)PushNotfType.Announcement && x.Status == 0).OrderByDescending(x => x.Id).ToList();
            List<AdminNotificationBindingModel> notificationsResponseList = Mapper.Map<List<Notification>, List<AdminNotificationBindingModel>>(notificationsList);
            return notificationsResponseList;
        }

        public bool SendPushNotification(string text, string title, string server_key = "")
        {

            PushNotificationsHelper.SendAndroidPushNotifications(Message: text, Title: title, DeviceTokens: _dbContext.UserDevices.Where(x1 => x1.Platform == true).Select(a => a.AuthToken).ToList());
            PushNotificationsHelper.SendIOSPushNotifications(Message: text, Title: title, DeviceTokens: _dbContext.UserDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
            return false;
        }
        public async Task<object> SendPushNotificationToApp(AdminNotificationBindingModel model)
        {
            try
            {
                UserTypes userType = UserTypes.User;
                var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false).ToList();
                if (model.TargetAudienceType == TargetAudienceType.User)
                {
                    targetedDevices = targetedDevices.Where(x => x.User_Id != null).ToList();
                }
                else if (model.TargetAudienceType == TargetAudienceType.Driver)
                {
                    targetedDevices = targetedDevices.Where(x => x.Driver_Id != null).ToList();
                    userType = UserTypes.Driver;
                }

                if (model.CityId > 0)
                {
                    List<int> Ids = _dbContext.Users.Where(x => x.IsDeleted == false && x.City_Id == model.CityId).Select(x => x.Id).ToList();
                }
                NotificationModel Notification = new NotificationModel();
                Notification.Title = model.Text;
                Notification.Text = model.Description;
                Notification.PushNotificationType = PushNotificationType.Announcement;
                List<string> usersToPushIOS = new List<string>();
                List<string> usersToPushAndriod = new List<string>();
                usersToPushAndriod = targetedDevices.Where(x => x.Platform == true && x.IsActive == true).Select(a => a.AuthToken).ToList();
                usersToPushIOS = targetedDevices.Where(x => x.Platform == false && x.IsActive == true).Select(a => a.AuthToken).ToList();
                 PushNotificationsHelper.SendIOSPushNotifications(Message: model.Description, Title: model.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList(), userType: userType);
                 PushNotificationsHelper.SendAndroidPushNotifications(Message: model.Description, Title: model.Title, DeviceTokens: targetedDevices.Where(x => x.Platform == true && x.IsActive == true).Select(a => a.AuthToken).ToList(), userType: userType);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool InsertNotification(Notification notification)
        {
            try
            {
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

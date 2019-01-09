using AppModel.BindingModels;
using AppModel.DTOs;
using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IBONotification
    {
        List<NotificationDTO> GetNotificationByUserId(int id);
        List<AdminNotificationBindingModel> GetAdminNotifications();

        bool SendPushNotification(string text, string title, string server_key="");
        bool InsertNotification(Notification notification);
        Task<object> SendPushNotificationToApp(AdminNotificationBindingModel model);
    }
}

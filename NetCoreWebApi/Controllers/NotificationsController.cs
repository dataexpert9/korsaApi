using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Wasalee.Controllers
{
    //[Authorize]
    [Route("/api/User")]
    public class NotificationsController : Controller
    {
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBONotification _bONotification;
        private readonly IHostingEnvironment _environment;
        protected readonly IBOUser _bOUser;
        protected readonly IBODriver _bODriver;
        public NotificationsController(DataContext dataContext, IConfiguration configuration, IBODriver bODriver, IBOUser bOUser, IBONotification bONotification, IHostingEnvironment environment)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bONotification = bONotification;
            _environment = environment;
            _bODriver = bODriver;
            _bOUser = bOUser;
        }
        /// <summary>
        /// Get All Notifications
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <param name="SignInType">0 for User, 1 for deliverer</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserNotifications")]
        public async Task<IActionResult> GetUserNotifications()
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                List<NotificationDTO> notificationsList = new List<NotificationDTO>();
                notificationsList = _bONotification.GetNotificationByUserId(userId);

                if (notificationsList == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get notifications.") } });

                return Ok(new CustomResponse<NotificationDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new NotificationDTOList { NotificationsList = notificationsList } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        ///// <summary>
        ///// Mark notification as read.
        ///// </summary>
        ///// <param name="notificationId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("MarkNotificationAsRead")]
        //public async Task<IActionResult> MarkNotificationAsRead(int NotificationId)
        //{
        //    try
        //    {
        //        using (AlghoneimContext ctx = new AlghoneimContext())
        //        {
        //            var notification = ctx.Notifications.FirstOrDefault(x => x.Id == NotificationId);

        //            if (notification != null)
        //            {
        //                notification.Status = (int)Global.NotificationStatus.Read;
        //                ctx.SaveChanges();
        //                CustomResponse<string> response = new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK };
        //                return Ok(response);
        //            }
        //            else
        //            {
        //                CustomResponse<Error> response = new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = (int)HttpStatusCode.NotFound, Result = new Error { ErrorMessage = "Invalid notificationid" } };
        //                return Ok(response);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Utility.LogError(ex));
        //    }
        //}

        ///// <summary>
        ///// Turn user notifications on or off
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="On"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("UserNoticationsOnOff")]
        //public async Task<IActionResult> UserNoticationsOnOff(int UserId, int SignInType, bool On)
        //{
        //    try
        //    {
        //        using (AlghoneimContext ctx = new AlghoneimContext())
        //        {
        //            if (SignInType == (int)RoleTypes.User)
        //            {

        //                var user = ctx.Users.FirstOrDefault(x => x.Id == UserId);
        //                if (user != null)
        //                {
        //                    user.IsNotificationsOn = On;
        //                    ctx.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                var deliveryMan = ctx.DeliveryMen.FirstOrDefault(x => x.Id == UserId);
        //                if (deliveryMan != null)
        //                {
        //                    deliveryMan.IsNotificationsOn = On;
        //                    ctx.SaveChanges();
        //                }
        //            }

        //        }
        //        return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Utility.LogError(ex));
        //    }
        //}

        [HttpGet]
        [Route("SendPushNotificationTemp")]
        public IActionResult SendPushNotification(string Text, string title = "", bool isDriverAudience = false)
        {
            try
            {
                string serverKey = (isDriverAudience) ? AppSettingsProvider.FCMServerKeyDriverApp : AppSettingsProvider.FCMServerKeyUserApp;
                _bONotification.SendPushNotification(Text, title, serverKey);

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpPost]
        [Route("SendPushNotificationToApp")]
        public async Task<IActionResult> SendPushNotificationToApp([FromBody]AdminNotificationBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var response = await _bONotification.SendPushNotificationToApp(model);
                switch (response)
                {                  
                    case HttpStatusCode.OK:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK, Result= new Error { ErrorMessage= "Notification sent successfully!" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = (int)HttpStatusCode.Conflict, Result = new Error { ErrorMessage = "Failure!" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpGet]
        [Route("GetAdminNotifications")]
        public async Task<IActionResult> GetAdminNotifications()
        {
            try
            {
                List<AdminNotificationBindingModel> notificationsList = new List<AdminNotificationBindingModel>();
                notificationsList = _bONotification.GetAdminNotifications();
                if (notificationsList == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get notifications.") } });
                return Ok(new CustomResponse<List<AdminNotificationBindingModel>> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result =  notificationsList  });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }
    }
}

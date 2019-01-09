using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Push_Notification
{
   public  class NotificationUtility
    {
        //public static void SendPushNotifications(List<UserDevice> usersToPushAndroid, List<UserDevice> usersToPushIOS, Notification Notification, int PushType)
        //{
        //    try
        //    {
        //        HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
        //        {
        //            Global.objPushNotifications.SendAndroidPushNotification
        //            (
        //                usersToPushAndroid,
        //                OtherNotification: Notification,
        //                Type: PushType);

        //            Global.objPushNotifications.SendIOSPushNotification
        //            (
        //                usersToPushIOS,
        //                OtherNotification: Notification,
        //                Type: PushType);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.LogError(ex);
        //    }
        //}
    }
}

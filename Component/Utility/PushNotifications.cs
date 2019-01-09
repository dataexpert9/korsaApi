using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static Component.Global;

namespace Component.Utility
{
    /// <summary>
    /// This class has configuration settings for Enterprise Android and IOS Apps
    /// </summary>
    public class Enterprise
    {
        /// <summary>
        /// IOS Configuration for Enterprise Apps
        /// </summary>
        public class IOS
        {
            /// <summary>
            /// Sandbox Certificate Name(used for IOS)
            /// </summary>
            public static string APNSDistributionCertificateName { get; set; }
            /// <summary>
            /// Production Certificate Name(used for IOS)
            /// </summary>
            public static string APNSDevelopmentCertificateName { get; set; }
            /// <summary>
            /// Production Configuration(IOS)
            /// </summary>
           // public static ApnsConfiguration ProductionConfig { get; set; }
            /// <summary>
            /// Sandbox Configuration(IOS)
            /// </summary>
           // public static ApnsConfiguration SandboxConfig { get; set; }
        }

        /// <summary>
        /// Android Configuration
        /// </summary>
        public class Android
        {
            // public static GcmConfiguration AndroidGCMConfig { get; set; }
            public static string PackageName { get; set; }
        }
    }

    /// <summary>
    /// This class has configuration settings for Playstore Android and IOS Apps
    /// </summary>
    public class PlayStore
    {
        public class IOS
        {
            public static string APNSDistributionCertificateName { get; set; }
            public static string APNSDevelopmentCertificateName { get; set; }
            //  public static ApnsConfiguration ProductionConfig { get; set; }
            //  public static ApnsConfiguration SandboxConfig { get; set; }
        }
        public class Android
        {
            //   public static GcmConfiguration AndroidGCMConfig { get; set; }
            public static string PackageName { get; set; }

        }

    }

    public class PushNotifications
    {
        #region Fields Declaration

        public EventHandler DeviceRemoved;
        //  private ApnsConfiguration ApnsConfig;
        //  private GcmConfiguration FCMConfig;



        private static string ServerKeyDriver = AppSettingsProvider.FCMServerKeyDriverApp;
        private static string ServerKey = AppSettingsProvider.FCMServerKeyUserApp;
        private static string SenderId = AppSettingsProvider.FCMSenderIdUser;

        private string GCMAppStorePackageName;
        private string GCMEnterprisePackageName;
        private string GCMProjectID;
        private string GCMWebAPIKey;
        private string GCMURL;
        public string APNSFilePasswordKey;
        //private string APNSDistributionCertificateName;
        //private string APNSDevelopmentCertificateName;


        #endregion

        //void initializeConfiguration()
        //{
        //    try
        //    {
        //        #region GCMConfiguration

        //        GCMProjectID = ConfigurationManager.AppSettings["GCMProjectID"];
        //        GCMWebAPIKey = ConfigurationManager.AppSettings["GCMWebAPIKey"];
        //        GCMURL = ConfigurationManager.AppSettings["GCMURL"];

        //        #region AndroidEnterprise

        //        Enterprise.Android.PackageName = ConfigurationManager.AppSettings["GCMEnterprisePackageName"];
        //        Enterprise.Android.AndroidGCMConfig = new GcmConfiguration(GCMProjectID, GCMWebAPIKey, Enterprise.Android.PackageName);
        //        Enterprise.Android.AndroidGCMConfig.GcmUrl = GCMURL;

        //        #endregion

        //        #region AndroidPlayStore

        //        PlayStore.Android.PackageName = ConfigurationManager.AppSettings["GCMAppStorePackageName"];
        //        PlayStore.Android.AndroidGCMConfig = new GcmConfiguration(GCMProjectID, GCMWebAPIKey, PlayStore.Android.PackageName);
        //        PlayStore.Android.AndroidGCMConfig.GcmUrl = GCMURL;

        //        #endregion

        //        #endregion

        //        #region ApnsConfiguration

        //        APNSFilePasswordKey = ConfigurationManager.AppSettings["APNSCertPassword"];

        //        #region IOSEnterprise
        //        Enterprise.IOS.APNSDevelopmentCertificateName = ConfigurationManager.AppSettings["APNSEnterpriseDevelopmentCertificateName"];
        //        Enterprise.IOS.APNSDistributionCertificateName = ConfigurationManager.AppSettings["APNSEnterpriseDistributionCertificateName"];

        //        try
        //        {
        //            Enterprise.IOS.ProductionConfig = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production,
        //                                   AppDomain.CurrentDomain.BaseDirectory + Enterprise.IOS.APNSDistributionCertificateName,
        //                                   APNSFilePasswordKey);
        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //        try
        //        {
        //            Enterprise.IOS.SandboxConfig = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,
        //                                    AppDomain.CurrentDomain.BaseDirectory + Enterprise.IOS.APNSDevelopmentCertificateName,
        //                                    APNSFilePasswordKey);
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        #endregion

        //        #region IOSPlayStore

        //        PlayStore.IOS.APNSDevelopmentCertificateName = ConfigurationManager.AppSettings["APNSStoreDevelopmentCertificateName"];
        //        PlayStore.IOS.APNSDistributionCertificateName = ConfigurationManager.AppSettings["APNSStoreDistributionCertificateName"];

        //        PlayStore.IOS.SandboxConfig = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,
        //                        AppDomain.CurrentDomain.BaseDirectory + PlayStore.IOS.APNSDevelopmentCertificateName,
        //                        APNSFilePasswordKey);

        //        #endregion

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public enum FileType
        {
            Photo = 1,
            Video = 2,
            Audio = 3,
            Excel = 4,
            Pdf = 5,
            Doc = 6,
            Pppt = 7,
            Txt = 8,
            Other = 9


        };

        //public PushNotifications(bool ProductionEnvironment)
        //{
        //    try
        //    {
        //        //Enterprise.IOS.APNSDistributionCertificateName = ConfigurationManager.AppSettings["APNSEnterpriseDistributionCertificateName"];
        //        //Enterprise.IOS.APNSDevelopmentCertificateName = ConfigurationManager.AppSettings["APNSEnterpriseDistributionCertificateName"];

        //        initializeConfiguration();

        //        FCMConfig = new GcmConfiguration(GCMProjectID, GCMWebAPIKey, Enterprise.Android.PackageName);
        //        FCMConfig.GcmUrl = GCMURL;

        //        //By default, Enterprise settings.
        //        ApnsConfig = new ApnsConfiguration(
        //            (ProductionEnvironment) ? ApnsConfiguration.ApnsServerEnvironment.Production : ApnsConfiguration.ApnsServerEnvironment.Sandbox,
        //               (ProductionEnvironment) ?
        //               AppDomain.CurrentDomain.BaseDirectory + Enterprise.IOS.APNSDistributionCertificateName :
        //               AppDomain.CurrentDomain.BaseDirectory + Enterprise.IOS.APNSDevelopmentCertificateName,
        //               APNSFilePasswordKey);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.LogError(ex);
        //    }
        //}
        public class NotificationModel
        {
            public string Title { get; set; }
            public string Text { get; set; }
            public Object MessageObject { get; set; }
            public int NotificationId { get; set; }
            public int Type { get; set; }
            public int EntityId { get; set; }
            public PushNotificationType PushNotificationType { get; set; }
        }

        public class IosPush
        {
            public IosPush()
            {
                aps = new aps();
                notification = new NotificationModel();
            }

            public aps aps { get; set; }
            public NotificationModel notification { get; set; }
        }

        public class aps
        {
            public aps()
            {
                alert = new alert();
            }
            public alert alert { get; set; }
            public string sound { get; set; } = "Default";
            public int badge { get; set; } = 1;
            [JsonProperty(PropertyName = "content-available")]
            public int contentavailable { get; set; }
        }

        public class alert
        {
            public string title { get; set; }
            public string body { get; set; }
        }

        //public async Task SendIosPushNotification(List<UserDevice> devices, string Text)
        //{
        //    string serverKey = GCMApiKey;
        //    var result = "-1";
        //    var notificationid = 0;
        //    try
        //    {
        //        var webAddr = "https://fcm.googleapis.com/fcm/send";

        //        foreach (var device in devices)
        //        {
        //            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        //            httpWebRequest.ContentType = "application/json";
        //            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" + serverKey);
        //            //httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
        //            httpWebRequest.Method = "POST";

        //            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //            {
        //                string json = "";

        //                json = "{\"to\": \"" + device.AuthToken + "\",\"data\": {\"message\": \"" + Text + "\",\"title\": \"" + Text + "\",\"notificationid\": \"" + notificationid + "\",\"isread\": \"" + true + "\",}}";

        //                streamWriter.Write(json);
        //                streamWriter.Flush();
        //            }

        //            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                result = streamReader.ReadToEnd();
        //                if (result.Contains("success") && result.Contains("failure"))
        //                {
        //                    dynamic token = JObject.Parse(result);
        //                    string success = token.success.ToString();
        //                    //return success == "1" ? true : false;
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }

        //        // return result;
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}

        //public void SendIOSPushNotification(List<UserDevice> devices, AdminNotifications AdminNotification = null, Notification OtherNotification = null, int Type = 0)

        public static async Task SendIOSPushNotification(List<string> devices, bool isIOS, NotificationModel OtherNotification = null, int Type = 0)
        {
            string serverKey = ServerKeyDriver;
            var result = "-1";
            var notificationid = 0;

            try

            {
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                foreach (var device in devices)
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" + serverKey);
                    httpWebRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));
                    //httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                    httpWebRequest.Method = "POST";
                    IosPush pushModel = new IosPush();




                    pushModel.aps.alert.title = OtherNotification.Title;
                    pushModel.aps.alert.body = OtherNotification.Text;



                    pushModel.notification.Type = Type;

                    pushModel.aps.contentavailable = 1;

                    using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "";
                        if (isIOS)
                        {
                            json = "{\"to\": \"" + devices + "\",\"notification\": {\"body\": \"" + pushModel.aps.alert.body + "\",\"title\": \"" + pushModel.aps.alert.title + "\",\"notificationid\": \"" + pushModel.notification.NotificationId + "\",\"orderid\": \"" + pushModel.notification.EntityId + "\",\"isread\": \"" + true + "\",}}";

                        }
                        else
                        {
                            json = "{\"to\": \"" + devices + "\",\"data\": {\"body\": \"" + pushModel.aps.alert.body + "\",\"title\": \"" + pushModel.aps.alert.title + "\",\"notificationid\": \"" + pushModel.notification.NotificationId + "\",\"orderid\": \"" + pushModel.notification.EntityId + "\",\"isread\": \"" + true + "\",}}";

                        }

                        //json = "{\"to\": \"" + device.AuthToken + "\",\"notification\": {\"body\": \"" + Text + "\",\"title\": \"" + Text + "\",}}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                        if (result.Contains("success") && result.Contains("failure"))
                        {
                            dynamic token = JObject.Parse(result);
                            string success = token.success.ToString();
                            //return success == "1" ? true : false;
                        }
                        else
                        {
                        }
                    }
                }

                // return result;
            }
            catch (Exception ex)
            {
            }

        }

        //public void OldSendIOSPushNotification(List<UserDevice> devices, AdminNotifications AdminNotification = null, Notification OtherNotification = null, int Type = 0)
        //{
        //    try
        //    {
        //        // Configuration (NOTE: .pfx can also be used here)
        //        if (devices.Count() == 0) //it means there is no device no need to run the below code
        //        {
        //            return;
        //        }

        //        if (ApnsConfig != null)
        //        {
        //            IosPush pushModel = new IosPush();

        //            foreach (var device in devices.Where(x => x.IsActive))
        //            {
        //                if (AdminNotification != null)
        //                {
        //                    pushModel.aps.alert.title = AdminNotification.Title;
        //                    pushModel.aps.alert.body = AdminNotification.Text;
        //                    pushModel.aps.contentavailable = 1;

        //                    if (device.User != null)
        //                        pushModel.notification.NotificationId = device.User.Notifications.FirstOrDefault(x => x.AdminNotification_Id == AdminNotification.Id).Id;
        //                    else
        //                        pushModel.notification.NotificationId = device.DeliveryMan.Notifications.FirstOrDefault(x => x.AdminNotification_Id == AdminNotification.Id).Id;

        //                    pushModel.notification.Type = (int)PushNotificationType.Announcement;
        //                    pushModel.notification.EntityId = OtherNotification.Entity_ID.Value;
        //                }
        //                else
        //                {
        //                    pushModel.aps.alert.title = OtherNotification.Title;
        //                    pushModel.aps.alert.body = OtherNotification.Text;
        //                    pushModel.notification.NotificationId = OtherNotification.Id;
        //                    pushModel.notification.DeliveryMan_Id = OtherNotification.DeliveryMan_ID;
        //                    pushModel.notification.Type = Type;
        //                    pushModel.notification.EntityId = OtherNotification.Entity_ID.Value;
        //                    pushModel.aps.contentavailable = 1;
        //                }

        //                ApnsServiceBroker apnsBroker;
        //                if (device.ApplicationType == UserDevice.ApplicationTypes.Enterprise)
        //                {
        //                    if (device.EnvironmentType == UserDevice.ApnsEnvironmentTypes.Production)
        //                    {
        //                        apnsBroker = new ApnsServiceBroker(Enterprise.IOS.ProductionConfig);
        //                        //device.iOSPushConfiguration = Enterprise.IOS.ProductionConfig;
        //                    }
        //                    else // Sandbox/Development
        //                    {
        //                        apnsBroker = new ApnsServiceBroker(Enterprise.IOS.SandboxConfig);
        //                        //device.iOSPushConfiguration = Enterprise.IOS.SandboxConfig;
        //                    }
        //                }
        //                else //PlayStore
        //                {
        //                    if (device.EnvironmentType == UserDevice.ApnsEnvironmentTypes.Production)
        //                    {
        //                        apnsBroker = new ApnsServiceBroker(PlayStore.IOS.ProductionConfig);
        //                        //device.iOSPushConfiguration = PlayStore.IOS.ProductionConfig;
        //                    }
        //                    else // Sandbox/Development
        //                    {
        //                        apnsBroker = new ApnsServiceBroker(device.iOSPushConfiguration = PlayStore.IOS.SandboxConfig);
        //                        //device.iOSPushConfiguration = PlayStore.IOS.SandboxConfig;
        //                    }
        //                }

        //                if (device.ApplicationType == UserDevice.ApplicationTypes.Enterprise)
        //                {
        //                    if (device.EnvironmentType == UserDevice.ApnsEnvironmentTypes.Production)
        //                        apnsBroker = new ApnsServiceBroker(Enterprise.IOS.ProductionConfig);
        //                    else // Sandbox/Development
        //                        apnsBroker = new ApnsServiceBroker(Enterprise.IOS.SandboxConfig);
        //                }
        //                else //PlayStore
        //                {
        //                    if (device.EnvironmentType == UserDevice.ApnsEnvironmentTypes.Production)
        //                        apnsBroker = new ApnsServiceBroker(PlayStore.IOS.ProductionConfig);
        //                    else // Sandbox/Development
        //                        apnsBroker = new ApnsServiceBroker(device.iOSPushConfiguration = PlayStore.IOS.SandboxConfig);
        //                }

        //                apnsBroker.OnNotificationFailed += IOSPushNotificationFailed;
        //                apnsBroker.OnNotificationSucceeded += IOSNotificationSuccess;

        //                // Start the broker
        //                apnsBroker.Start();
        //                apnsBroker.QueueNotification(new ApnsNotification
        //                {
        //                    DeviceToken = device.AuthToken,
        //                    Payload = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(pushModel))
        //                });

        //                apnsBroker.Stop();
        //                apnsBroker.OnNotificationFailed -= IOSPushNotificationFailed;
        //                apnsBroker.OnNotificationSucceeded -= IOSNotificationSuccess;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}





        //public async Task SendAndroidPushNotification(List<UserDevice> devices, AdminNotifications AdminNotification = null, Notification OtherNotification = null, int Type = 0)
        //{
        //    try
        //    {
        //        if (devices.Count() == 0)//it means their is no device no need to run the below code
        //        {
        //            return;
        //        }

        //        NotificationModel msgModel = new NotificationModel();

        //        foreach (var device in devices.Where(x => x.IsActive))
        //        {
        //            GcmServiceBroker gcmBroker;

        //            if (AdminNotification != null)
        //            {
        //                msgModel.Type = (int)PushNotificationType.Announcement;
        //                Notification notification;

        //                if (device.User != null)
        //                    notification = device.User.Notifications.FirstOrDefault(x => x.AdminNotification_Id == AdminNotification.Id);
        //                else
        //                    notification = device.DeliveryMan.Notifications.FirstOrDefault(x => x.AdminNotification_Id == AdminNotification.Id);

        //                msgModel.Message = AdminNotification.Text;
        //                msgModel.Message_Ar = AdminNotification.Text_Ar;
        //                msgModel.NotificationId = notification.Id;
        //                msgModel.Title = notification.Title;
        //                msgModel.Title_Ar = notification.Title_Ar;
        //                msgModel.ImageUrl = notification.ImageUrl;
        //                //msgModel.EntityId = (OtherNotification != null) ? OtherNotification.Entity_ID.Value : 0;
        //            }
        //            else if (OtherNotification != null)
        //            {
        //                msgModel.Type = Type;
        //                msgModel.Message = OtherNotification.Text;
        //                msgModel.Message_Ar = OtherNotification.Text_Ar;
        //                msgModel.NotificationId = OtherNotification.Id;
        //                msgModel.Title = OtherNotification.Title;
        //                msgModel.Title_Ar = OtherNotification.Title_Ar;
        //                msgModel.DeliveryMan_Id = OtherNotification.DeliveryMan_ID;
        //                msgModel.EntityId = OtherNotification.Entity_ID.Value;

        //            }

        //            if (device.ApplicationType == UserDevice.ApplicationTypes.Enterprise)
        //            {
        //                gcmBroker = new GcmServiceBroker(Enterprise.Android.AndroidGCMConfig);
        //            }
        //            else
        //            {
        //                gcmBroker = new GcmServiceBroker(PlayStore.Android.AndroidGCMConfig);
        //            }

        //            gcmBroker.OnNotificationFailed += AndroidNotificationFailed;
        //            gcmBroker.OnNotificationSucceeded += AndroidNotificationSuccess;
        //            gcmBroker.Start();

        //            gcmBroker.QueueNotification(
        //            new GcmNotification
        //            {
        //             Ids = new List<string> { device.AuthToken },
        //                Priority = GcmNotificationPriority.High,
        //                Data = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(msgModel))
        //            });

        //            gcmBroker.Stop();
        //            gcmBroker.OnNotificationFailed -= AndroidNotificationFailed;
        //            gcmBroker.OnNotificationSucceeded -= AndroidNotificationSuccess;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}








        private void DeleteExpiredSubscription(string AuthToken)
        {
            try
            {
                //using (AlghoneimContext ctx = new BLL())
                //{
                //    bll.Delete(AuthToken);
                //}

                OnDeviceRemoved(new PushNotificationEventArgs(AuthToken));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void OnDeviceRemoved(EventArgs e)
        {
            EventHandler handler = DeviceRemoved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }

    public class PushNotificationEventArgs : EventArgs
    {
        public string AuthToken { get; set; }
        public PushNotificationEventArgs(string authToken)
        {
            AuthToken = authToken;
        }
    }
}

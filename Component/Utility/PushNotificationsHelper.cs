using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Component.Utility
{
    public static class PushNotificationsHelper
    {
        private static string ServerKeyDriver = AppSettingsProvider.FCMServerKeyDriverApp;
        private static string ServerKey = AppSettingsProvider.FCMServerKeyUserApp;
        private static string SenderIdUser = AppSettingsProvider.FCMSenderIdUser;
        private static string SenderIdDriver = AppSettingsProvider.FCMSenderIdDriver;
        public  static int SendAndroidPushNotifications(string Message, string Title, List<string> DeviceTokens, int EntityId = 0, UserTypes userType = UserTypes.User, PushNotfType Type = PushNotfType.Announcement)
        {
            try
            {
                string key = String.Empty;
                string senderId = String.Empty;
                if (userType == UserTypes.User)
                {
                    key = ServerKey;
                    senderId = SenderIdUser;
                }
                else
                {
                    key = ServerKey;
                    senderId = SenderIdUser;
                }
                foreach (var deviceToken in DeviceTokens)
                {
                    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "POST";
                    tRequest.Headers.Add(string.Format("Authorization:key={0}", key));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    tRequest.ContentType = "application/json";
                    var payload = new
                    {
                        to = deviceToken,
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            body = Message,
                            title = Title,
                            entityId = EntityId,
                            type = Type
                        },
                    };                  

                    string postbody = JsonConvert.SerializeObject(payload).ToString();
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                    tRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = tRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = tRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                if (dataStreamResponse != null)
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        String sResponseFromServer = tReader.ReadToEnd();
                                        //result.Response = sResponseFromServer;
                                    }
                                }
                            }
                        }
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SendIOSPushNotifications(string Message, string Title, List<string> DeviceTokens, int EntityId = 0, UserTypes userType = UserTypes.User, PushNotfType Type = PushNotfType.Announcement)
        {
            try
            {

                string key = String.Empty;
                string senderId = String.Empty;
                if (userType == UserTypes.User)
                {
                    key = ServerKey;
                    senderId = SenderIdUser;
                }
                else
                {
                    key = ServerKey;
                    senderId = SenderIdUser;
                }
                foreach (var deviceToken in DeviceTokens)
                {
                    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "POST";
                    tRequest.Headers.Add(string.Format("Authorization:key={0}", key));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    tRequest.ContentType = "application/json";

                    var payload = new
                    {
                        to = deviceToken,
                        content_available = true,
                        mutable_content = false,
                        data = new
                        {
                            message = Message,
                            title = Title
                        },
                        notification = new
                        {
                            body = Message,
                            title = Title,
                            entityId = EntityId,
                            type = Type
                        }
                    };

                    string postbody = JsonConvert.SerializeObject(payload).ToString();
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);

                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    WebResponse tResponse = tRequest.GetResponse();
                    Stream dataStreamResponse = tResponse.GetResponseStream();
                    if (dataStreamResponse != null)
                    {
                        StreamReader tReader = new StreamReader(dataStreamResponse);
                        String sResponseFromServer = tReader.ReadToEnd();

                    }
                }
                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

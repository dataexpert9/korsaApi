using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Component.Utility
{
    public class SocialLogins
    {
        public async Task<SocialUserViewModel> GetSocialUserData(string access_token, SocialLoginType socialLoginType)
        {
            try
            {
                HttpClient client = new HttpClient();
                string urlProfile = "";
                string picture = "";
                if (socialLoginType == SocialLoginType.Google)
                    urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;
                else if (socialLoginType == SocialLoginType.Facebook)
                    // urlProfile = "https://graph.facebook.com/me?access_token=" + access_token;
                    urlProfile = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name&access_token=" + access_token;

                else if (socialLoginType == SocialLoginType.Twitter)
                {

                }
                client.CancelPendingRequests();
                HttpResponseMessage output = await client.GetAsync(urlProfile);


                if (output.IsSuccessStatusCode)
                {
                    string outputData = await output.Content.ReadAsStringAsync();
                    SocialUserViewModel socialUser = JsonConvert.DeserializeObject<SocialUserViewModel>(outputData);

                    socialUser.picture = "http://graph.facebook.com/" + socialUser.id + "/picture?type=large";
                    if (!String.IsNullOrEmpty(socialUser.id))
                    {
                        socialUser.username = socialUser.id.ToString();
                    }
                    socialUser.password = socialUser.email;
                    return socialUser;
                    //if (socialUser != null)
                    //{
                    //    // You will get the user information here.
                    //}
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class SocialUserViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string given_name { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
        public string password { get; set; }
    }
}

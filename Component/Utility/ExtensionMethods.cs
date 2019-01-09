using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;

namespace Component.Utility
{
    public static class ExtensionMethods
    {
        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }


        public static bool SendEmail(string Email,string Subject,string Body)
        {

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("wasallee1@gmail.com", "wasallee!23")
                };

                var message = new MailMessage(new MailAddress(AppSettingsProvider.FromMailAddress), new MailAddress(Email))
                {
                    Subject = Subject,
                    Body = Body
                };

                smtp.Send(message);

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



    }
}

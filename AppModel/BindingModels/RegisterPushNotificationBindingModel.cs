using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{

    public class RegisterPushNotificationBindingModel
    {
        public string DeviceName { get; set; }

        [Required]
        public string UDID { get; set; }

        [Required]
        public bool IsAndroidPlatform { get; set; }

        [Required]
        public bool IsPlayStore { get; set; }


        [Required]
        public bool IsProduction { get; set; }

        public string AuthToken { get; set; }

        [Required]
        public UserTypes SignInType { get; set; }
    }
}

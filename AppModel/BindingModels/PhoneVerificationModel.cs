using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class PhoneVerificationModel
    {

        [Required]
        [Display(Name = "Code")]
        public int Code { get; set; }
        public UserTypes UserType { get; set; }
    }
}

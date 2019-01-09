using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class PhoneBindingModel
    {
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public UserTypes UserType { get; set; }
        public bool isResetCase { get; set; } = false;
    }
}

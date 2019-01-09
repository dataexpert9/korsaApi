using Component.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public class VerifyPhoneNumberBindingModel
    {
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [StringLength(maximumLength: 15, MinimumLength = 10, ErrorMessage = "Enter valid Phone Number")]
        public string PhoneNum { get; set; }
    }

    public class ResetPasswordBindingModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string CellNumOrUsername { get; set; }
    }


    public class ChangePasswordBindingModel
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }


    public class UpdatePasswordBindingModel
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }

    public class SetPasswordBindingModel
    {

        [Required(ErrorMessage = "This field is required")]
        public string CellNumOrUsername { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}

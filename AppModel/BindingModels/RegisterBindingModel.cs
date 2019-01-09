using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Component.Utility;
using Microsoft.AspNetCore.Http;
using static Utility.ImageHelper;

namespace AppModel.BindingModels
{
    public class RegisterDriverBindingModel
    {
        public IFormFile ProfilePicture { get; set; }
        
        [Required]
        public string Username { get; set; }
        public string FullName { get; set; }
        
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string LicenseNo { get; set; }

        [Required]
        public DateTime LicenseExpiry { get; set; }

        [Required]
        public List<IFormFile> DrivingLicenseFrontImages { get; set; }

        [Required]
        public List<IFormFile> DrivingLicenseBackImages { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        public int Company_Id { get; set; }
        [Required]
        public int Model_Id { get; set; }
        [Required]
        public int Year_Id { get; set; }
        [Required]
        public int Type_Id { get; set; }
        [Required]
        public int Capacity_Id { get; set; }

        public int RideType_Id { get; set; }
        
        [Required]
        public DateTime RegisterationExpiry { get; set; }
        [Required]
        public List<IFormFile> RegistrationCopyImages { get; set; }
        [Required]
        public List<IFormFile> CarPhotos { get; set; }

        public string BriefIntro { get; set; }
        public string WorkHistory { get; set; }
        public DriverAccountStatus Status { get; set; } = DriverAccountStatus.RequestPending;


        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public int? Branch_Id { get; set; }
    }

    public class RegisterUserBindingModel
    {

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        //[Required(ErrorMessage = "Please Upload Profile Picture")]
        public IFormFile ProfilePicture { get; set; }
        //[Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }

        [RegularExpression(MyRegularExpressions.Email, ErrorMessage = "Enter valid email address.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int City_Id { get; set; }

        public string InvitationCode { get; set; }

        //-------------------------Optionals---------------------------

        [Required]
        [Display(Name = "Current Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must have to agree with our terms and conditons in order to register")]
        public bool TermsAndConditions { get; set; }
        public SignInType SignUpType { get; set; }
    }


    public class EditUserProfileBindingModel
    {

        public IFormFile ProfilePicture { get; set; }
        [Required]
        public string FullName { get; set; }

        //[Required]
        //[Display(Name = "Phone Number")]
        //public string PhoneNo { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        //[Display(Name = "Zip Code")]
        //public string ZipCode { get; set; }



    }

   


    public class EditDriverProfileBindingModel
    {

        public IFormFile ProfilePicture { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string ImagesToRemoveIds { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public List<IFormFile> DrivingLicenseFrontImages { get; set; }

        public List<IFormFile> DrivingLicenseBackImages { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public int? Branch_Id { get; set; }
    }



    public class EditDriverProfileBindingModelAdmin
    {
        public int Id { get; set; }
        public ImageModel ProfilePicture { get; set; }

        public string HomeAddress { get; set; }

        public string PhoneNo { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string ImagesToRemoveIds { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public List<ImageModel> DrivingLicenseFrontImages { get; set; }

        public List<ImageModel> DrivingLicenseBackImages { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public int? Branch_Id { get; set; }
    }
}

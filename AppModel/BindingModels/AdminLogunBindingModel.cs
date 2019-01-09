using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Utility.ImageHelper;

namespace AppModel.BindingModels
{

    public class AdminLoginBindingModel
    {
        //public AdminLoginBindingModel(string _email, string _password)
        //{
        //    Email = _email;
        //    Password = _password;
        //}
        //public AdminLoginBindingModel()
        //{

        //}

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }



    public class AdminBindingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public short Role { get; set; }

        [Required]
        public string Email { get; set; }

        public ImageModel Image { get; set; }
        public string ImageUrl { get; set; }
    
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }





    public class AddUserBindingModel
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        public ImageModel ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        [RegularExpression(MyRegularExpressions.Email, ErrorMessage = "Enter valid email address.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        //public string City { get; set; }
        
        [Display(Name = "Current Address")]
        public string Address { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string InvitationCode { get; set; }
        public int City_Id { get; set; }

        public Gender Gender { get; set; }

    }

    public class AddDriverBindingModel
    {
        public ImageModel ProfilePicture { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public List<ImageModel> DrivingLicenseFrontImages { get; set; }
        public List<ImageModel> DrivingLicenseBackImages { get; set; }
        public string Number { get; set; }
        public string PhoneNo { get; set; }
        public int Company_Id { get; set; }
        public int Model_Id { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int RideType_Id { get; set; }
        public int City_Id { get; set; }

        public DateTime RegisterationExpiry { get; set; }
        public List<ImageModel> RegistrationCopyImages { get; set; }
        public List<ImageModel> CarPhotos { get; set; }

        public string BriefIntro { get; set; }
        public string WorkHistory { get; set; }
        public DriverAccountStatus Status { get; set; } = DriverAccountStatus.RequestPending;

    }




        public class DashboardStats
        {
            public DashboardStats()
            {
                DeviceUsage = new List<DeviceStats>();
            }
            public int TotalUsers { get; set; }
            public int TotalDrivers { get; set; }
            public int TotalRides { get; set; }
            public int TotalVehicles { get; set; }
            public List<DeviceStats> DeviceUsage { get; set; }
        }
        public class DeviceStats
        {
            public int Count { get; set; }
            public int Percentage { get; set; }
        }


        public class ChangeUserStatusListModel
        {
            public ChangeUserStatusListModel()
            {
                Users = new List<ChangeUserStatusModel>();
            }

            public List<ChangeUserStatusModel> Users { get; set; }
        }

        public class ChangeUserStatusModel
        {
            public int UserId { get; set; }
            public bool Status { get; set; }
        }

    }

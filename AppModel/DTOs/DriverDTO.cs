using AppModel.CustomModels;
using Component.Utility;
using System;
using System.Collections.Generic;

namespace AppModel.DTOs
{
    public class DriverDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public int RemainingRide { get; set; }

        public double Wallet { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string HomeAddress { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public List<DriverMediaDTO> Medias { get; set; }
        public int Company_Id { get; set; }
        public int Model_Id { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int City_Id { get; set; }
        public int RideType_Id { get; set; }

        public List<VehicleDTO> Vehicles { get; set; }
        public List<DriverAccountDTO> DriverAccounts { get; set; }
        public List<CashSubscriptionDTO> CashSubscriptions { get; set; }

        public double Rating { get; set; }
        public bool IsNotificationsOn { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int SignInType { get; set; }
        public int RidesCount { get; set; }
        public int TotalMileage { get; set; }

        public string Token { get; set; }
        public string BriefIntro { get; set; }
        public string WorkHistory { get; set; }
        public DriverAccountStatus Status { get; set; }
        public SettingsDTO AppSettings { get; set; }

        //----------Optionals
        public string FullName { get; set; }
        public Locations Location { get; set; }
        public string CarColor { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public Guid UserId { get; set; }
        public string ZipCode { get; set; }
        public string InvitationCode { get; set; }
        public bool TermsAndConditions { get; set; }
        public LoginStatus LoginStatus { get; set; }

    }

    public class DriverViewModel
    {
        public DriverViewModel()
        {
            Driver = new DriverDTO();
        }
        public DriverDTO Driver { get; set; }
    }

    public class DriverDTOList
    {
        public DriverDTOList()
        {
            Drivers = new List<DriverDTO>();
        }
        public List<DriverDTO> Drivers { get; set; }
        public int RideId { get; set; }
    }
    public class DriverMediaDTO
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
    }




}

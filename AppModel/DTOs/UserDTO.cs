using AppModel.CustomModels;
using Component.Utility;
using System.Collections.Generic;

namespace AppModel.DTOs
{
    public class UserDTO : BaseModelDTO
    {
        public UserDTO()
        {
            AppSettings = new SettingsDTO();
            BankTopUps = new List<TopUpDTO>();
            
        }
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public bool UseCreditFirst { get; set; }
        public string ActiveCardId { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string ZipCode { get; set; }

        public string DateofBirth { get; set; }

        public int? SignInType { get; set; }

        public short? Status { get; set; }
        public double Rating { get; set; }
        public bool EmailConfirmed { get; set; }
        public int RidesCount { get; set; }
        public double TotalDistance { get; set; } //In Miles

        public bool PhoneConfirmed { get; set; }
        public Gender DriverPreference { get; set; }
        public bool IsNotificationsOn { get; set; }

        public Locations Location { get; set; }

        public string Token { get; set; }
        public int Gender { get; set; }

        public CityDTO City { get; set; }
        public int? City_Id { get; set; }
        public string State { get; set; }

        public string Address { get; set; }

        public string InvitationCode { get; set; }
        public SettingsDTO AppSettings { get; set; }
        public List<TopUpDTO> BankTopUps { get; set; }
        public List<CreditCardDTO> CreditCards { get; set; }



        public string UserName { get; set; }
        public string Password { get; set; }
        public PaymentMethods PrefferedPaymentMethod { get; set; }
        public float Credit { get; set; }
        public bool TermsAndConditions { get; set; }
        public int AppReferrerUserId { get; set; }
        public double DistanceTravelled { get; set; }
        public int CurrentReferralCount { get; set; }
        public int FreeRides { get; set; }
        public double Wallet { get; set; }

    }



    public class UserViewModel
    {
        public UserViewModel()
        {
            User = new UserDTO();
        }
        public UserDTO User { get; set; }
    }

    public class UserDTOList
    {
        public UserDTOList()
        {
            Users = new List<UserDTO>();
        }
        public List<UserDTO> Users { get; set; }
    }

}

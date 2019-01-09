using AppModel.CustomModels;
using AppModel.DomainModels;
using Component.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class User : BaseModel
    {
        public User()
        {
            UserDevices = new HashSet<UserDevice>();
            BankTopUps = new HashSet<TopUp>();
            UserTrips = new HashSet<UserTrip>();
            //AppRatings = new HashSet<AppRating>();
            ContactUs = new HashSet<ContactUs>();
            //VerifyNumberCodes = new HashSet<VerifyNumberCode>();
            //Trips = new HashSet<Trip>();
            Notifications = new HashSet<Notification>();
            InvitedFriends = new HashSet<InvitedFriend>();
            CreditCards = new HashSet<CreditCard>();
            PaymentHistories = new HashSet<PaymentHistory>();

        }

        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public Gender Gender { get; set; }
        public string ActiveCardId { get; set; }
        public Gender DriverPreference { get; set; }
        public int RatedRidesCount { get; set; } = 0;
        [JsonIgnore]
        public string Password { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public double TotalDistance { get; set; } //In Miles
        public float Rating { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string InvitationCode { get; set; }
        public DateTime DateofBirth { get; set; }
        public int? SignInType { get; set; }
        public short? Status { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsNotificationsOn { get; set; } = true;
        public bool UseCreditFirst { get; set; }
        public PaymentMethods PrefferedPaymentMethod { get; set; } = PaymentMethods.Cash;
        public float Credit { get; set; }
        public int CurrentReferralCount { get; set; }
        public int FreeRides { get; set; }
        public bool TermsAndConditions { get; set; }
        public int AppReferrerUserId { get; set; }
        public int RidesCount { get; set; }
        public double DistanceTravelled { get; set; }
        public double Wallet { get; set; }


        [NotMapped]
        public string Token { get; set; }
        public int? City_Id { get; set; }
        public City City { get; set; }


        public ICollection<PaymentHistory> PaymentHistories { get; set; }

        public virtual ICollection<FavouriteLocation> FavouriteLocations { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<InvitedFriend> InvitedFriends { get; set; }// Invited Friends
        public virtual ICollection<InvitedFriend> Referrer { get; set; }// Invited Friends

        //public virtual InvitedFriend Referrer { get; set; }// Friend who invited you

        public virtual ICollection<TopUp> BankTopUps { get; set; }
        public virtual ICollection<UserDevice> UserDevices { get; set; }
        public virtual ICollection<UserTrip> UserTrips { get; set; }
        public virtual ICollection<AppRating> AppRatings { get; set; }
        public virtual ICollection<ContactUs> ContactUs { get; set; }
        //public virtual ICollection<VerifyNumberCode> VerifyNumberCodes { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCode> UserPromocodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}

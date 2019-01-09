using AppModel.CustomModels;
using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class Driver : BaseModel
    {
        public Driver()
        {
            Notifications = new HashSet<Notification>();
            DriverDevices = new HashSet<UserDevice>();
            Vehicles = new HashSet<Vehicle>();
            Medias = new HashSet<DriversMedia>();
            Trips = new HashSet<Trip>();
            Location = new Location();
            ContactUs = new HashSet<ContactUs>();
            DriverSubscriptions = new HashSet<DriverSubscription>();
            CashSubscriptions = new HashSet<CashSubscription>();
            DriverAccounts = new HashSet<DriverAccount>();
            DriverPayments = new HashSet<DriverPayment>();
            CashSubscriptions = new HashSet<CashSubscription>();
            CreditCards = new HashSet<CreditCard>();
            PaymentHistories = new HashSet<PaymentHistory>();

        }
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string HomeAddress { get; set; }
        public string LicenseNo { get; set; }
        public int RidesCount { get; set; }
        public int RatedRidesCount { get; set; } = 0;
        public double Wallet { get; set; }
        public string Username { get; set; }
        public double TotalMileage { get; set; }
        public string BriefIntro { get; set; }
        public string WorkHistory { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public Location Location { get; set; }
        public bool IsNotificationsOn { get; set; } = true;
        public string CarColor { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public double Rating { get; set; }
        public string Password { get; set; }
        public int SignInType { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public int Gender { get; set; }
        public DriverAccountStatus Status { get; set; }
        public string InvitationCode { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool TermsAndConditions { get; set; }
        public int? City_Id { get; set; }
        public City City { get; set; }

        
        public ICollection<PaymentHistory> PaymentHistories { get; set; }
        public ICollection<DriverAccount> DriverAccounts { get; set; }
        public ICollection<DriverPayment> DriverPayments { get; set; }
        public ICollection<CashSubscription> CashSubscriptions { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<DriverLog> DriverLogs { get; set; }

        public ICollection<Trip> Trips { get; set; }
        public virtual ICollection<ContactUs> ContactUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserDevice> DriverDevices { get; set; }
        public virtual ICollection<DriversMedia> Medias { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<DriverSubscription> DriverSubscriptions { get; set; }

        public LoginStatus LoginStatus { get; set; }
    }
    public class DriverAccount : BaseModel
    {
       
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public Driver Driver { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public bool isActive { get; set; }

        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public Branch Branch { get; set; }

    }

}

using System.ComponentModel.DataAnnotations;

namespace Component.Utility
{
    public enum KorsaEntityTypes
    {
        Admin,
        Settings,
        SubscriptionPackage,
        RideType,
        Bank,
        Branch,
        Account,
        Country,
        City,
        CarCompany,
        CarModel,
        CarYear,
        CarType,
        CarCapacity,
        Role,
        FareCalculation

    }


    public enum LogType
    {
        Login,
        Logout
    }

    public enum AppImprovementTypes
    {
        Other,
        Driving,
        Navigation,
        Comfort,
        [Display(Name ="Car Quality")]
        CarQuality
    }


    public enum TargetAudienceType
    {
        User = 1,
        Driver,
        All
    }

    public enum DurationType
    {
        Days,
        Months,
        Years
    }



    public enum TopUpStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public enum AddUpdateResponse
    {
        Error,
        Added,
        Updated,
        AlreadyExist,
        NotFound
    }



    public enum SearchType
    {
        All,
        LowRated
    }


    public enum RideSearchType
    {
        All,
        AcceptedRides,
        CancelledRides,
        CompletedRides,
        ScheduledRides,
        RideAcceptanceReport
    }



    public enum CultureType
    {

        English = 1,
        Arabic = 2,
        Both = 3
    }

    public enum SocialLoginType
    {
        Google,
        Facebook,
        Instagram,
        Twitter
    }
    public enum MediaType
    {
        LicenseFront = 1,
        LicenseBack = 2,
        CarImage = 3,
        RegistrationCopyImage = 4,
        TopUpReciept=5,
        CashSubscription=6,
    }

    public enum PushNotfType
    {
        Announcement,
        UserBlocked,
        DriverBlocked
    }

    public enum LoginStatus
    {
        Online = 1,
        Offline = 2
    }
    public enum DriverAccountStatus
    {
        RequestPending = 1,
        RequestRejected = 2,
        RequestApproved = 3
    }
    public enum PromocodeType
    {
        DiscountPercentage = 1,
        FixedDiscount = 2
    }


    public enum SignInType
    {
        ThroughApp = 1,
        Facebook = 2
    }

    public enum UserTypes
    {
        User = 1,
        Driver = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum PaymentMethods
    {
        Cash = 1,
        CreditCard = 2,
        Wallet=3,
    }
    

    public enum TripTypes
    {
        Current = 1,
        Scheduled = 2,
    }
    public enum DriverMessages
    {
        [Display(Name = "I am outside")]
        IAmOutside = 1,
        [Display(Name = "Please Call me")]
        PleaseCallMe = 2
    }
    public enum UserMessages
    {
        [Display(Name = "Please Wait")]
        PleaseWait = 1,
        [Display(Name = "I am waiting")]
        IAmWaiting = 2,
        [Display(Name = "Please Call me")]
        PleaseCallMe = 3
    }

    public enum TripStatus
    {
        Requested = 1,
        AssignedToDriver = 2,
        Arrived = 3,
        Started = 4,
        Completed = 5,
        CancelledByDriver = 6,
        CancelledByUser = 7,
        Closed
    }

    public enum RideCancellationReason
    {
        [Display(Name = "I'm getting late")]
        GettingLate = 1,
        [Display(Name = "I don't want to go now")]
        DontWannaGo = 2
    }
    public enum WhatCanBeImproved
    {
        [Display(Name = "Driving")]
        Driving = 1,
        [Display(Name = "Navigation")]
        Navigation = 2,
        [Display(Name = "Comfort")]
        Comfort = 3,
        [Display(Name = "Car Quality")]
        CarQuality = 4
    }

}

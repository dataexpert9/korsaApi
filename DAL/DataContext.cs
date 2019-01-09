using AppModel.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.DomainModels
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VerifyNumberCode> VerifyNumberCodes { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SettingsML> SettingsMLs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<RideType> RideTypes { get; set; }
        public DbSet<RideTypeML> RideTypeMLs { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<AppRating> AppRatings { get; set; }
        public DbSet<AppRatingML> AppRatingMLs { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankML> BankMLs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchML> BranchMLs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountML> AccountMLs { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryML> CountryMLs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityML> CityMLs { get; set; }
        public DbSet<DriverPayment> DriverPayments { get; set; }
        public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
        public DbSet<DriverSubscription> DriverSubscriptions { get; set; }
        public DbSet<DriverAccount> DriverAccounts { get; set; }
        public DbSet<DriverLog> DriverLogs { get; set; }
        public DbSet<AdminTokens> AdminTokens { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<FavouriteLocation> FavouriteLocations { get; set; }
        public virtual DbSet<AdminNotifications> AdminNotifications { get; set; }
        public virtual DbSet<AdminSubAdminNotifications> AdminSubAdminNotifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DriversMedia> DriverMedias { get; set; }
        public DbSet<VehicleMedia> VehicleMedias { get; set; }
        public DbSet<TopUpMedia> TopUpMedias { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<RoleScreen> RoleScreens { get; set; }
        public virtual DbSet<FareCalculation> FareCalculations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Promocode> Promocodes { get; set; }
        public DbSet<UserCode> UserPromocode { get; set; }
        public DbSet<CancellationReason> CancellationReasons { get; set; }
        public DbSet<CancellationReasonML> CancellationReasonMLs { get; set; }

        public DbSet<TopUp> BankTopUps { get; set; }
        public DbSet<SupportConversation> SupportConversations { get; set; }
        public DbSet<CashSubscription> CashSubscriptions { get; set; }
        public DbSet<CashSubscriptionMedia> CashSubscriptionMedias { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<CarCompanyML> CarCompanyMLs { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarTypeML> CarTypeMLs { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarModelML> CarModelMLs { get; set; }
        public DbSet<CarYear> CarYear { get; set; }
        public DbSet<CarYearML> CarYearMLs { get; set; }
        public DbSet<CarCapacity> CarCapacity { get; set; }
        public DbSet<CarCapacityML> CarCapacityMLs { get; set; }
        public DbSet<InvitedFriend> InvitedFriends { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<Mailing> Mailing { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Countries

            modelBuilder.Entity<Country>()
        .HasMany(e => e.Cities)
        .WithOne(e => e.Country)
        .HasForeignKey(e => e.Country_Id)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Country>()
       .HasMany(e => e.CountryMLsList)
       .WithOne(e => e.Country)
       .HasForeignKey(e => e.Country_Id)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<City>()
       .HasMany(e => e.CityMLsList)
       .WithOne(e => e.City)
       .HasForeignKey(e => e.City_Id)
       .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<City>()
     .HasMany(e => e.FareCalculations)
     .WithOne(e => e.City)
     .HasForeignKey(e => e.City_Id)
     .OnDelete(DeleteBehavior.Cascade);

            #endregion


            #region Account
            modelBuilder.Entity<Bank>()
            .HasMany(a => a.BankMLsList)
            .WithOne(e => e.Bank)
            .HasForeignKey(x => x.Bank_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bank>()
           .HasMany(a => a.Branches)
           .WithOne(e => e.Bank)
           .HasForeignKey(x => x.Bank_Id)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
           .HasMany(a => a.BranchMLsList)
           .WithOne(e => e.Branch)
           .HasForeignKey(x => x.Branch_Id)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
           .HasMany(a => a.Accounts)
           .WithOne(e => e.Branch)
           .HasForeignKey(x => x.Branch_Id)
           .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Account>()
             .HasMany(a => a.AccountMLsList)
             .WithOne(e => e.Account)
             .HasForeignKey(x => x.Account_Id)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
           .HasMany(a => a.BankTopUps)
           .WithOne(e => e.Account)
           .HasForeignKey(x => x.Account_Id)
           .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Account>()
            .HasMany(a => a.CashSubscriptions)
            .WithOne(e => e.Account)
            .HasForeignKey(x => x.Account_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubscriptionPackage>()
            .HasMany(a => a.CashSubscriptions)
            .WithOne(e => e.SubscriptionPackage)
            .HasForeignKey(x => x.SubscriptionPackage_Id)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion


            #region User's
            modelBuilder.Entity<FavouriteLocation>().OwnsOne(c => c.Location);



            modelBuilder.Entity<City>()
           .HasMany(e => e.Users)
           .WithOne(e => e.City)
           .HasForeignKey(e => e.City_Id)
           .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<User>()
            .HasMany(e => e.PaymentHistories)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.User_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(e => e.CreditCards)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.User_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(e => e.InvitedFriends)
            .WithOne(e => e.InvitedUser)
            .HasForeignKey(e => e.InvitedUser_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
           .HasMany(e => e.Referrer)
           .WithOne(e => e.Referrer)
           .HasForeignKey(e => e.Referrer_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(a => a.UserDevices)
            .WithOne(e => e.User)
            .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany(a => a.BankTopUps)
            .WithOne(e => e.User)
            .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TopUp>()
            .HasMany(a => a.Receipts)
            .WithOne(e => e.TopUp)
            .HasForeignKey(x => x.TopUp_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(a => a.FavouriteLocations)
            .WithOne(e => e.User)
            .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(a => a.Notifications)
            .WithOne(e => e.User)
            .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
            .HasMany(a => a.UserTrips)
            .WithOne(e => e.User)
            .IsRequired()
           .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
              .HasMany(a => a.AppRatings)
              .WithOne(e => e.User)
              .HasForeignKey(x => x.User_Id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
             .HasMany(a => a.ContactUs)
            .WithOne(e => e.User)
             .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
             .HasMany(a => a.ContactUs)
            .WithOne(e => e.Driver)
             .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
          .HasMany(x => x.UserPromocodes)
          .WithOne(x => x.User)
          .HasForeignKey(x => x.User_Id)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Promocode>()
            .HasMany(x => x.UserPromocodes)
            .WithOne(x => x.Promocode)
            .HasForeignKey(x => x.Promocode_Id)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion


            #region Trip's


            modelBuilder.Entity<Trip>().OwnsOne(c => c.DestinationLocation);
            modelBuilder.Entity<Trip>().OwnsOne(c => c.PickupLocation);

            modelBuilder.Entity<Trip>()
                .HasMany(a => a.UserTrips)
                .WithOne(e => e.Trip)
                .IsRequired()
                 .HasForeignKey(x => x.Trip_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RideType>()
               .HasMany(a => a.Trips)
               .WithOne(b => b.RideType)
               .HasForeignKey(x => x.RideType_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RideType>()
                .HasMany(e => e.FareCalculations)
                .WithOne(e => e.RideType)
                .HasForeignKey(e => e.RideType_Id)
                .OnDelete(DeleteBehavior.SetNull);

            #endregion 


            #region Driver's

            modelBuilder.Entity<Driver>().OwnsOne(c => c.Location);

            modelBuilder.Entity<City>()
           .HasMany(e => e.Drivers)
           .WithOne(e => e.City)
           .HasForeignKey(e => e.City_Id)
           .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Driver>()
            .HasMany(e => e.PaymentHistories)
            .WithOne(e => e.Driver)
            .HasForeignKey(e => e.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
            .HasMany(e => e.CreditCards)
            .WithOne(e => e.Driver)
            .HasForeignKey(e => e.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Driver>()
            .HasMany(a => a.DriverDevices)
            .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SubscriptionPackage>()
          .HasMany(e => e.DriverSubscriptions)
          .WithOne(e => e.SubscriptionPackage)
          .HasForeignKey(e => e.SubscriptionPackage_Id)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
            .HasMany(a => a.CashSubscriptions)
            .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CashSubscription>()
            .HasMany(a => a.Receipts)
            .WithOne(e => e.CashSubscription)
            .HasForeignKey(x => x.CashSubscription_Id)
            .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Driver>()
            .HasMany(a => a.DriverSubscriptions)
            .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
            .HasMany(a => a.DriverAccounts)
            .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
           .HasMany(a => a.DriverPayments)
           .WithOne(e => e.Driver)
           .HasForeignKey(x => x.Driver_Id)
           .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Driver>()
         .HasMany(a => a.Notifications)
         .WithOne(e => e.Driver)
         .HasForeignKey(x => x.Driver_Id)
         .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Driver>()
            .HasMany(a => a.Medias)
            .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
            .HasMany(a => a.Trips)
           .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
         .HasMany(a => a.Vehicles)
        .WithOne(e => e.Driver)
         .HasForeignKey(x => x.Driver_Id)
        .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<City>()
            //.HasMany(a => a.Drivers)
            //.WithOne(e => e.City)
            //.HasForeignKey(x => x.City_Id)
            //.OnDelete(DeleteBehavior.Cascade);
            #endregion


            #region Vehicle

            modelBuilder.Entity<Vehicle>()
       .HasMany(a => a.Medias)
       .WithOne(e => e.Vehicle)
       .HasForeignKey(x => x.Vehicle_Id)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RideType>()
      .HasMany(a => a.Vehicles)
      .WithOne(e => e.RideType)
      .HasForeignKey(x => x.RideType_Id)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarCompany>()
      .HasMany(a => a.CarModels)
      .WithOne(e => e.CarCompany)
      .HasForeignKey(x => x.Company_Id)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarModel>()
     .HasMany(a => a.Vehicles)
     .WithOne(e => e.CarModel)
     .HasForeignKey(x => x.Model_Id)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarCapacity>()
     .HasMany(a => a.Vehicles)
     .WithOne(e => e.CarCapacity)
     .HasForeignKey(x => x.Capacity_Id)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarType>()
     .HasMany(a => a.Vehicles)
     .WithOne(e => e.CarType)
     .HasForeignKey(x => x.Type_Id)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarYear>()
     .HasMany(a => a.Vehicles)
     .WithOne(e => e.CarYear)
     .HasForeignKey(x => x.Year_Id)
     .OnDelete(DeleteBehavior.Cascade);



            #endregion

            #region Admin

            modelBuilder.Entity<AdminNotifications>()
           .HasMany(a => a.Notifications)
           .WithOne(e => e.AdminNotification)
           .HasForeignKey(x => x.AdminNotification_Id)
           .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Role>()
            .HasMany(e => e.RoleScreen)
            .WithOne(e => e.Roles)
            .HasForeignKey(e => e.Role_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Screen>()
                .HasMany(e => e.RoleScreen)
                .WithOne(e => e.Screens)
                .HasForeignKey(e => e.Screen_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
            .HasMany(e => e.Admins)
            .WithOne(e => e.Roles)
            .HasForeignKey(e => e.Role_Id)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
          
        }
    }



}



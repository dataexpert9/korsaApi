using AppModel.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace AppModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VerifyNumberCode> VerifyNumberCodes { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SettingsML> SettingsMLs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<RideType> RideTypes { get; set; }
        public DbSet<RideTypeML> RideTypeMLs { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<AppRating> AppRatings { get; set; }
        public DbSet<AppRatingML> AppRatingMLs { get; set; }
        public DbSet<UserDriverMessaging> UserDriverMessagings { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<MessageML> MessageMLs { get; set; }
        public DbSet<Promocode> Promocodes { get; set; }
        public DbSet<UserCode> UserPromocode { get; set; }
        public DbSet<CancellationReason> CancellationReasons { get; set; }
        public DbSet<CancellationReasonML> CancellationReasonMLs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User's
            modelBuilder.Entity<User>()
           .HasMany(a => a.UserDevices)
           .WithOne(e => e.User)
           .IsRequired()
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

            modelBuilder.Entity<User>()
             .HasMany(a => a.VerifyNumberCodes)
            .WithOne(e => e.User)
             .HasForeignKey(x => x.User_Id)
            .OnDelete(DeleteBehavior.Cascade);

            //Trip's
            modelBuilder.Entity<Trip>()
            .HasMany(a => a.UserTrips)
            .WithOne(e => e.Trip)
            .IsRequired()
             .HasForeignKey(x => x.Trip_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
            .HasMany(a => a.Trips)
           .WithOne(e => e.Driver)
            .HasForeignKey(x => x.Driver_Id)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RideType>()
           .HasMany(a => a.Trips)
           .WithOne(b => b.RideType)
           .HasForeignKey(x => x.RideType_Id)
            .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Trip>()
            // .HasOne(a => a.PrimaryUser)
            //.WithOne(e => e.Trip)
            // .HasForeignKey<Trip>(b => b.PrimaryUser_Id)
            //.OnDelete(DeleteBehavior.SetNull);

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
        }
    }
}

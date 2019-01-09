﻿// <auto-generated />
using System;
using DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181005103938_PromocodeType")]
    partial class PromocodeType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppModel.DomainModels.CancellationReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("CancellationReasons");
                });

            modelBuilder.Entity("AppModel.DomainModels.CancellationReasonML", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CancellationReason_Id");

                    b.Property<int>("Culture");

                    b.Property<string>("Reason");

                    b.HasKey("Id");

                    b.HasIndex("CancellationReason_Id");

                    b.ToTable("CancellationReasonMLs");
                });

            modelBuilder.Entity("DAL.DomainModels.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNo");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<short>("Role");

                    b.Property<short?>("Status");

                    b.Property<int?>("Store_Id");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("DAL.DomainModels.AdminNotifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminId");

                    b.Property<int>("Admin_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("TargetAudienceType");

                    b.Property<string>("Text");

                    b.Property<string>("Text_Ar");

                    b.Property<string>("Title");

                    b.Property<string>("Title_Ar");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("AdminNotifications");
                });

            modelBuilder.Entity("DAL.DomainModels.AdminSubAdminNotifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminId");

                    b.Property<int?>("AdminNotificationId");

                    b.Property<int?>("AdminNotification_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Status");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("AdminNotificationId");

                    b.ToTable("AdminSubAdminNotifications");
                });

            modelBuilder.Entity("DAL.DomainModels.AppRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CanImprove");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<short>("Rating");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("AppRatings");
                });

            modelBuilder.Entity("DAL.DomainModels.AppRatingML", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppRating_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("AppRating_Id");

                    b.ToTable("AppRatingMLs");
                });

            modelBuilder.Entity("DAL.DomainModels.CarCapacity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarCapacity");
                });

            modelBuilder.Entity("DAL.DomainModels.CarCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarCompany");
                });

            modelBuilder.Entity("DAL.DomainModels.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarModel");
                });

            modelBuilder.Entity("DAL.DomainModels.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarType");
                });

            modelBuilder.Entity("DAL.DomainModels.CarYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarYear");
                });

            modelBuilder.Entity("DAL.DomainModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<int>("Country_Id");

                    b.Property<string>("Name");

                    b.Property<int>("State_Id");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("State_Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("DAL.DomainModels.ContactUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Message");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("DAL.DomainModels.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PhoneCode");

                    b.Property<string>("SortName");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("DAL.DomainModels.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BriefIntro");

                    b.Property<string>("CarColor");

                    b.Property<string>("CarName");

                    b.Property<string>("CarNumber");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<int>("Gender");

                    b.Property<string>("HomeAddress");

                    b.Property<string>("InvitationCode");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNotificationsOn");

                    b.Property<DateTime>("LicenseExpiry");

                    b.Property<string>("LicenseNo");

                    b.Property<int>("LoginStatus");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<float>("Rating");

                    b.Property<int>("RidesCount");

                    b.Property<int>("SignInType");

                    b.Property<int>("Status");

                    b.Property<bool>("TermsAndConditions");

                    b.Property<int>("TotalMileage");

                    b.Property<Guid>("UserId");

                    b.Property<string>("Username");

                    b.Property<string>("WorkHistory");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("DAL.DomainModels.DriversMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("Driver_Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("MediaUrl");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Driver_Id");

                    b.ToTable("DriverMedias");
                });

            modelBuilder.Entity("DAL.DomainModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("Driver_Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Text");

                    b.Property<int>("User_Id");

                    b.Property<bool>("isUserSender");

                    b.HasKey("Id");

                    b.HasIndex("Driver_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DAL.DomainModels.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminNotificationId");

                    b.Property<int?>("AdminNotification_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("DeliveryMan_ID");

                    b.Property<int?>("DriverId");

                    b.Property<int?>("Entity_ID");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("Status");

                    b.Property<string>("Text");

                    b.Property<string>("Text_Ar");

                    b.Property<string>("Title");

                    b.Property<string>("Title_Ar");

                    b.Property<int?>("UserId");

                    b.Property<int?>("User_ID");

                    b.HasKey("Id");

                    b.HasIndex("AdminNotificationId");

                    b.HasIndex("DriverId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DAL.DomainModels.Promocode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("Discount");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("DAL.DomainModels.RideType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("BasicCharges");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<string>("DefaultImageUrl");

                    b.Property<float>("FarePerKm");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<float>("PeakFactor");

                    b.Property<int>("PersonsCapacity");

                    b.Property<string>("SelectedImageUrl");

                    b.HasKey("Id");

                    b.ToTable("RideTypes");
                });

            modelBuilder.Entity("DAL.DomainModels.RideTypeML", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutRideType");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("RideType_Id");

                    b.HasKey("Id");

                    b.HasIndex("RideType_Id");

                    b.ToTable("RideTypeMLs");
                });

            modelBuilder.Entity("DAL.DomainModels.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<string>("CurrencySymbol");

                    b.Property<float>("InvitationBonus");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("MaximumRequestRadius");

                    b.Property<double>("MinimumRequestRadius");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("DAL.DomainModels.SettingsML", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutUs");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<string>("Currency");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("PrivacyPolicy");

                    b.Property<int>("Settings_Id");

                    b.Property<string>("TermsOfUse");

                    b.HasKey("Id");

                    b.HasIndex("Settings_Id");

                    b.ToTable("SettingsMLs");
                });

            modelBuilder.Entity("DAL.DomainModels.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Country_Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Country_Id");

                    b.ToTable("Sates");
                });

            modelBuilder.Entity("DAL.DomainModels.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CancellationReason_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DestinationLocationName");

                    b.Property<float>("Discount");

                    b.Property<short>("DriverRating");

                    b.Property<int?>("Driver_Id");

                    b.Property<DateTime>("EndTime");

                    b.Property<float>("EstimatedFare");

                    b.Property<double>("Fare");

                    b.Property<float>("FarePerKm");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<float>("PeakFactor");

                    b.Property<DateTime>("PickupDateTime");

                    b.Property<string>("PickupLocationName");

                    b.Property<int?>("PrimaryUser_Id");

                    b.Property<int?>("Promocode_Id");

                    b.Property<DateTime>("RequestTime");

                    b.Property<int>("RideType_Id");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<short>("UserRating");

                    b.Property<bool>("isScheduled");

                    b.HasKey("Id");

                    b.HasIndex("CancellationReason_Id");

                    b.HasIndex("Driver_Id");

                    b.HasIndex("PrimaryUser_Id");

                    b.HasIndex("Promocode_Id");

                    b.HasIndex("RideType_Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("DAL.DomainModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("AppReferrerUserId");

                    b.Property<string>("City");

                    b.Property<int?>("CityId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Credit");

                    b.Property<DateTime>("DateofBirth");

                    b.Property<double>("DistanceTravelled");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<int>("Gender");

                    b.Property<string>("InvitationCode");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNotificationsOn");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<bool>("PhoneConfirmed");

                    b.Property<string>("PhoneNo");

                    b.Property<int>("PrefferedPaymentMethod");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<float>("Rating");

                    b.Property<int>("RidesCount");

                    b.Property<int?>("SignInType");

                    b.Property<string>("State");

                    b.Property<short?>("Status");

                    b.Property<bool>("TermsAndConditions");

                    b.Property<double>("TotalDistance");

                    b.Property<string>("UserName");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.DomainModels.UserCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Promocode_Id");

                    b.Property<DateTime>("UsageDate");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Promocode_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserPromocode");
                });

            modelBuilder.Entity("DAL.DomainModels.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationType");

                    b.Property<string>("AuthToken");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DeviceName");

                    b.Property<int?>("Driver_Id");

                    b.Property<int>("EnvironmentType");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool>("Platform");

                    b.Property<string>("UDID");

                    b.Property<int?>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Driver_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserDevices");
                });

            modelBuilder.Entity("DAL.DomainModels.UserTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<short>("Rating");

                    b.Property<DateTime>("RequestTime");

                    b.Property<int>("Trip_Id");

                    b.Property<int>("User_Id");

                    b.Property<bool>("isScheduled");

                    b.HasKey("Id");

                    b.HasIndex("Trip_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserTrips");
                });

            modelBuilder.Entity("DAL.DomainModels.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity_Id");

                    b.Property<int>("Company_Id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Driver_Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Model_Id");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Number");

                    b.Property<DateTime>("RegistrationExpiry");

                    b.Property<int>("Type_Id");

                    b.Property<int>("Year_Id");

                    b.HasKey("Id");

                    b.HasIndex("Capacity_Id");

                    b.HasIndex("Company_Id");

                    b.HasIndex("Driver_Id");

                    b.HasIndex("Model_Id");

                    b.HasIndex("Type_Id");

                    b.HasIndex("Year_Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("DAL.DomainModels.VehicleMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("MediaUrl");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Type");

                    b.Property<int?>("Vehicle_Id");

                    b.HasKey("Id");

                    b.HasIndex("Vehicle_Id");

                    b.ToTable("VehicleMedias");
                });

            modelBuilder.Entity("DAL.DomainModels.VerifyNumberCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Phone");

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.ToTable("VerifyNumberCodes");
                });

            modelBuilder.Entity("AppModel.DomainModels.CancellationReasonML", b =>
                {
                    b.HasOne("AppModel.DomainModels.CancellationReason", "CancellationReason")
                        .WithMany("CancellationReasonMLsList")
                        .HasForeignKey("CancellationReason_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.AdminNotifications", b =>
                {
                    b.HasOne("DAL.DomainModels.Admin", "Admin")
                        .WithMany("SentNotifications")
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("DAL.DomainModels.AdminSubAdminNotifications", b =>
                {
                    b.HasOne("DAL.DomainModels.Admin", "Admin")
                        .WithMany("ReceivedNotifications")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.AdminNotifications", "AdminNotification")
                        .WithMany("AdminSubAdminNotifications")
                        .HasForeignKey("AdminNotificationId");
                });

            modelBuilder.Entity("DAL.DomainModels.AppRating", b =>
                {
                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("AppRatings")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.AppRatingML", b =>
                {
                    b.HasOne("DAL.DomainModels.AppRating", "AppRating")
                        .WithMany("AppRatingMLsList")
                        .HasForeignKey("AppRating_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.City", b =>
                {
                    b.HasOne("DAL.DomainModels.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId");

                    b.HasOne("DAL.DomainModels.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("State_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.ContactUs", b =>
                {
                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("ContactUs")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.Driver", b =>
                {
                    b.OwnsOne("DAL.DomainModels.Location", "Location", b1 =>
                        {
                            b1.Property<int>("DriverId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.ToTable("Drivers");

                            b1.HasOne("DAL.DomainModels.Driver")
                                .WithOne("Location")
                                .HasForeignKey("DAL.DomainModels.Location", "DriverId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("DAL.DomainModels.DriversMedia", b =>
                {
                    b.HasOne("DAL.DomainModels.Driver", "Driver")
                        .WithMany("Medias")
                        .HasForeignKey("Driver_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.Message", b =>
                {
                    b.HasOne("DAL.DomainModels.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("Driver_Id");

                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.Notification", b =>
                {
                    b.HasOne("DAL.DomainModels.AdminNotifications", "AdminNotification")
                        .WithMany("Notifications")
                        .HasForeignKey("AdminNotificationId");

                    b.HasOne("DAL.DomainModels.Driver")
                        .WithMany("Notifications")
                        .HasForeignKey("DriverId");

                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DAL.DomainModels.Promocode", b =>
                {
                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DAL.DomainModels.RideTypeML", b =>
                {
                    b.HasOne("DAL.DomainModels.RideType", "RideType")
                        .WithMany("RideTypeMLsList")
                        .HasForeignKey("RideType_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.SettingsML", b =>
                {
                    b.HasOne("DAL.DomainModels.Settings", "Settings")
                        .WithMany("SettingsMLsList")
                        .HasForeignKey("Settings_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.State", b =>
                {
                    b.HasOne("DAL.DomainModels.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("Country_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.Trip", b =>
                {
                    b.HasOne("AppModel.DomainModels.CancellationReason", "CancellationReason")
                        .WithMany()
                        .HasForeignKey("CancellationReason_Id");

                    b.HasOne("DAL.DomainModels.Driver", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("Driver_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.User", "PrimaryUser")
                        .WithMany()
                        .HasForeignKey("PrimaryUser_Id");

                    b.HasOne("DAL.DomainModels.Promocode", "Promocode")
                        .WithMany()
                        .HasForeignKey("Promocode_Id");

                    b.HasOne("DAL.DomainModels.RideType", "RideType")
                        .WithMany("Trips")
                        .HasForeignKey("RideType_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("DAL.DomainModels.Location", "DestinationLocation", b1 =>
                        {
                            b1.Property<int>("TripId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.ToTable("Trips");

                            b1.HasOne("DAL.DomainModels.Trip")
                                .WithOne("DestinationLocation")
                                .HasForeignKey("DAL.DomainModels.Location", "TripId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DAL.DomainModels.Location", "PickupLocation", b1 =>
                        {
                            b1.Property<int>("TripId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.ToTable("Trips");

                            b1.HasOne("DAL.DomainModels.Trip")
                                .WithOne("PickupLocation")
                                .HasForeignKey("DAL.DomainModels.Location", "TripId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("DAL.DomainModels.User", b =>
                {
                    b.HasOne("DAL.DomainModels.City")
                        .WithMany("Users")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("DAL.DomainModels.UserCode", b =>
                {
                    b.HasOne("DAL.DomainModels.Promocode", "Promocode")
                        .WithMany("UserPromocodes")
                        .HasForeignKey("Promocode_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("UserPromocodes")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.UserDevice", b =>
                {
                    b.HasOne("DAL.DomainModels.Driver", "Driver")
                        .WithMany("DriverDevices")
                        .HasForeignKey("Driver_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("UserDevices")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.UserTrip", b =>
                {
                    b.HasOne("DAL.DomainModels.Trip", "Trip")
                        .WithMany("UserTrips")
                        .HasForeignKey("Trip_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.User", "User")
                        .WithMany("UserTrips")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.Vehicle", b =>
                {
                    b.HasOne("DAL.DomainModels.CarCapacity", "CarCapacity")
                        .WithMany("Vehicles")
                        .HasForeignKey("Capacity_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.CarCompany", "CarCompany")
                        .WithMany("Vehicles")
                        .HasForeignKey("Company_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.Driver", "Driver")
                        .WithMany("Vehicles")
                        .HasForeignKey("Driver_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.CarModel", "CarModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("Model_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.CarType", "CarType")
                        .WithMany("Vehicles")
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.DomainModels.CarYear", "CarYear")
                        .WithMany("Vehicles")
                        .HasForeignKey("Year_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.DomainModels.VehicleMedia", b =>
                {
                    b.HasOne("DAL.DomainModels.Vehicle", "Vehicle")
                        .WithMany("Medias")
                        .HasForeignKey("Vehicle_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

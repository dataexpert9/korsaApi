﻿// <auto-generated />
using AppModel;
using Component.Utility;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180804054424_AlphaSevicesBaseModel")]
    partial class AlphaSevicesBaseModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppModel.AppRating", b =>
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

            modelBuilder.Entity("AppModel.AppRatingML", b =>
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

            modelBuilder.Entity("AppModel.ContactUs", b =>
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

            modelBuilder.Entity("AppModel.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<int>("Gender");

                    b.Property<string>("HomeAddress");

                    b.Property<string>("InvitationCode");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNotificationsOn");

                    b.Property<double>("Latitude");

                    b.Property<string>("LicenseNo");

                    b.Property<double>("Longitude");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<int>("SignInType");

                    b.Property<bool>("TermsAndConditions");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("AppModel.RideType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("BasicCharges");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<float>("FarePerKm");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("PersonsCapacity");

                    b.HasKey("Id");

                    b.ToTable("RideTypes");
                });

            modelBuilder.Entity("AppModel.RideTypeML", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("AppModel.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Culture");

                    b.Property<string>("CurrencySymbol");

                    b.Property<float>("InvitationBonus");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("AppModel.SettingsML", b =>
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

            modelBuilder.Entity("AppModel.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Discount");

                    b.Property<short>("DriverRating");

                    b.Property<int>("Driver_Id");

                    b.Property<DateTime>("EndTime");

                    b.Property<float>("Fare");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<DateTime>("PickupDateTime");

                    b.Property<int?>("PrimaryUser_Id");

                    b.Property<string>("Promocode");

                    b.Property<DateTime>("RequestTime");

                    b.Property<int>("RideType_Id");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<bool>("isScheduled");

                    b.HasKey("Id");

                    b.HasIndex("Driver_Id");

                    b.HasIndex("PrimaryUser_Id")
                        .IsUnique()
                        .HasFilter("[PrimaryUser_Id] IS NOT NULL");

                    b.HasIndex("RideType_Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("AppModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("AppReferrerUserId");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Credit");

                    b.Property<DateTime>("DateofBirth");

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

                    b.Property<short>("Ratings");

                    b.Property<int?>("SignInType");

                    b.Property<string>("State");

                    b.Property<short?>("Status");

                    b.Property<bool>("TermsAndConditions");

                    b.Property<double>("TotalDistance");

                    b.Property<int>("TotalRides");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppModel.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthToken");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DeviceName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool>("Platform");

                    b.Property<string>("UDID");

                    b.Property<int?>("User_Id")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserDevices");
                });

            modelBuilder.Entity("AppModel.UserTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<short>("Rating");

                    b.Property<int>("Trip_Id");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Trip_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserTrips");
                });

            modelBuilder.Entity("AppModel.VerifyNumberCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("VerifyNumberCodes");
                });

            modelBuilder.Entity("AppModel.AppRating", b =>
                {
                    b.HasOne("AppModel.User", "User")
                        .WithMany("AppRatings")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.AppRatingML", b =>
                {
                    b.HasOne("AppModel.AppRating", "AppRating")
                        .WithMany("AppRatingMLsList")
                        .HasForeignKey("AppRating_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.ContactUs", b =>
                {
                    b.HasOne("AppModel.User", "User")
                        .WithMany("ContactUs")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.RideTypeML", b =>
                {
                    b.HasOne("AppModel.RideType", "RideType")
                        .WithMany("RideTypeMLsList")
                        .HasForeignKey("RideType_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.SettingsML", b =>
                {
                    b.HasOne("AppModel.Settings", "Settings")
                        .WithMany("SettingsMLsList")
                        .HasForeignKey("Settings_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.Trip", b =>
                {
                    b.HasOne("AppModel.Driver", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("Driver_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppModel.User", "PrimaryUser")
                        .WithOne("Trip")
                        .HasForeignKey("AppModel.Trip", "PrimaryUser_Id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppModel.RideType", "RideType")
                        .WithMany("Trips")
                        .HasForeignKey("RideType_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.UserDevice", b =>
                {
                    b.HasOne("AppModel.User", "User")
                        .WithMany("UserDevices")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.UserTrip", b =>
                {
                    b.HasOne("AppModel.Trip", "Trip")
                        .WithMany("UserTrips")
                        .HasForeignKey("Trip_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppModel.User", "User")
                        .WithMany("UserTrips")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppModel.VerifyNumberCode", b =>
                {
                    b.HasOne("AppModel.User", "User")
                        .WithMany("VerifyNumberCodes")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

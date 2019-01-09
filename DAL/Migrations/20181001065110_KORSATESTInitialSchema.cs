using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class KORSATESTInitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Role = table.Column<short>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    Store_Id = table.Column<int>(nullable: true),
                    Status = table.Column<short>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancellationReasons",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCapacity",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCapacity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCompany",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarYear",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SortName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    LicenseNo = table.Column<string>(nullable: true),
                    RidesCount = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    TotalMileage = table.Column<int>(nullable: false),
                    BriefIntro = table.Column<string>(nullable: true),
                    WorkHistory = table.Column<string>(nullable: true),
                    LicenseExpiry = table.Column<DateTime>(nullable: false),
                    Location_Longitude = table.Column<double>(nullable: false),
                    Location_Latitude = table.Column<double>(nullable: false),
                    IsNotificationsOn = table.Column<bool>(nullable: false),
                    CarColor = table.Column<string>(nullable: true),
                    CarName = table.Column<string>(nullable: true),
                    CarNumber = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    SignInType = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    InvitationCode = table.Column<string>(nullable: true),
                    ProfilePictureUrl = table.Column<string>(nullable: true),
                    TermsAndConditions = table.Column<bool>(nullable: false),
                    LoginStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RideTypes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonsCapacity = table.Column<int>(nullable: false),
                    FarePerKm = table.Column<float>(nullable: false),
                    DefaultImageUrl = table.Column<string>(nullable: true),
                    SelectedImageUrl = table.Column<string>(nullable: true),
                    PeakFactor = table.Column<float>(nullable: false),
                    BasicCharges = table.Column<float>(nullable: false),
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    InvitationBonus = table.Column<float>(nullable: false),
                    MinimumRequestRadius = table.Column<double>(nullable: false),
                    MaximumRequestRadius = table.Column<double>(nullable: false),
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyNumberCodes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    isVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyNumberCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin_Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Title_Ar = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Text_Ar = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    TargetAudienceType = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AdminId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminNotifications_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CancellationReasonMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CancellationReason_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasonMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReason_Id",
                        column: x => x.CancellationReason_Id,
                        principalTable: "CancellationReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Country_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sates_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverMedias",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: true),
                    Driver_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverMedias_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    RegistrationExpiry = table.Column<DateTime>(nullable: false),
                    Company_Id = table.Column<int>(nullable: false),
                    Model_Id = table.Column<int>(nullable: false),
                    Year_Id = table.Column<int>(nullable: false),
                    Type_Id = table.Column<int>(nullable: false),
                    Capacity_Id = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarCapacity_Capacity_Id",
                        column: x => x.Capacity_Id,
                        principalTable: "CarCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "CarCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarModel_Model_Id",
                        column: x => x.Model_Id,
                        principalTable: "CarModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarType_Type_Id",
                        column: x => x.Type_Id,
                        principalTable: "CarType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarYear_Year_Id",
                        column: x => x.Year_Id,
                        principalTable: "CarYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideTypeMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    RideType_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideTypeMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideTypeMLs_RideTypes_RideType_Id",
                        column: x => x.RideType_Id,
                        principalTable: "RideTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingsMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutUs = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    TermsOfUse = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    Settings_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsMLs_Settings_Settings_Id",
                        column: x => x.Settings_Id,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminSubAdminNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AdminNotification_Id = table.Column<int>(nullable: true),
                    AdminNotificationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSubAdminNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminSubAdminNotifications_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminSubAdminNotifications_AdminNotifications_AdminNotificationId",
                        column: x => x.AdminNotificationId,
                        principalTable: "AdminNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    State_Id = table.Column<int>(nullable: false),
                    Country_Id = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cities_Sates_State_Id",
                        column: x => x.State_Id,
                        principalTable: "Sates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMedias",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: true),
                    Vehicle_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleMedias_Vehicles_Vehicle_Id",
                        column: x => x.Vehicle_Id,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TotalDistance = table.Column<double>(nullable: false),
                    Rating = table.Column<float>(nullable: false),
                    ProfilePictureUrl = table.Column<string>(nullable: true),
                    InvitationCode = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    SignInType = table.Column<int>(nullable: true),
                    Status = table.Column<short>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PhoneConfirmed = table.Column<bool>(nullable: false),
                    IsNotificationsOn = table.Column<bool>(nullable: false),
                    PrefferedPaymentMethod = table.Column<int>(nullable: false),
                    Credit = table.Column<float>(nullable: false),
                    TermsAndConditions = table.Column<bool>(nullable: false),
                    AppReferrerUserId = table.Column<int>(nullable: false),
                    RidesCount = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppRatings",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<short>(nullable: false),
                    CanImprove = table.Column<int>(nullable: false),
                    Culture = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRatings_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Driver_Id = table.Column<int>(nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    isUserSender = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Text_Ar = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Title_Ar = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: true),
                    Entity_ID = table.Column<int>(nullable: true),
                    DeliveryMan_ID = table.Column<int>(nullable: true),
                    AdminNotification_Id = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    AdminNotificationId = table.Column<int>(nullable: true),
                    DriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AdminNotifications_AdminNotificationId",
                        column: x => x.AdminNotificationId,
                        principalTable: "AdminNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Promocodes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Discount = table.Column<double>(nullable: false),
                    isValid = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceName = table.Column<string>(nullable: true),
                    UDID = table.Column<string>(nullable: true),
                    Platform = table.Column<bool>(nullable: false),
                    User_Id = table.Column<int>(nullable: true),
                    AuthToken = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: true),
                    EnvironmentType = table.Column<int>(nullable: false),
                    ApplicationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevices_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDevices_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRatingMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    AppRating_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRatingMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRatingMLs_AppRatings_AppRating_Id",
                        column: x => x.AppRating_Id,
                        principalTable: "AppRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PickupLocation_Longitude = table.Column<double>(nullable: false),
                    PickupLocation_Latitude = table.Column<double>(nullable: false),
                    DestinationLocation_Longitude = table.Column<double>(nullable: false),
                    DestinationLocation_Latitude = table.Column<double>(nullable: false),
                    Fare = table.Column<double>(nullable: false),
                    PickupLocationName = table.Column<string>(nullable: true),
                    DestinationLocationName = table.Column<string>(nullable: true),
                    EstimatedFare = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    PickupDateTime = table.Column<DateTime>(nullable: false),
                    DriverRating = table.Column<short>(nullable: false),
                    UserRating = table.Column<short>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    isScheduled = table.Column<bool>(nullable: false),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Promocode_Id = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CancellationReason_Id = table.Column<int>(nullable: true),
                    FarePerKm = table.Column<float>(nullable: false),
                    PeakFactor = table.Column<float>(nullable: false),
                    PrimaryUser_Id = table.Column<int>(nullable: true),
                    RideType_Id = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_CancellationReasons_CancellationReason_Id",
                        column: x => x.CancellationReason_Id,
                        principalTable: "CancellationReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Users_PrimaryUser_Id",
                        column: x => x.PrimaryUser_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Promocodes_Promocode_Id",
                        column: x => x.Promocode_Id,
                        principalTable: "Promocodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_RideTypes_RideType_Id",
                        column: x => x.RideType_Id,
                        principalTable: "RideTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPromocode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Id = table.Column<int>(nullable: false),
                    Promocode_Id = table.Column<int>(nullable: false),
                    UsageDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromocode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPromocode_Promocodes_Promocode_Id",
                        column: x => x.Promocode_Id,
                        principalTable: "Promocodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPromocode_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrips",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<short>(nullable: false),
                    User_Id = table.Column<int>(nullable: false),
                    Trip_Id = table.Column<int>(nullable: false),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    isScheduled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrips_Trips_Trip_Id",
                        column: x => x.Trip_Id,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrips_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminNotifications_AdminId",
                table: "AdminNotifications",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminSubAdminNotifications_AdminId",
                table: "AdminSubAdminNotifications",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminSubAdminNotifications_AdminNotificationId",
                table: "AdminSubAdminNotifications",
                column: "AdminNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRatingMLs_AppRating_Id",
                table: "AppRatingMLs",
                column: "AppRating_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppRatings_User_Id",
                table: "AppRatings",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReason_Id",
                table: "CancellationReasonMLs",
                column: "CancellationReason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_State_Id",
                table: "Cities",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_User_Id",
                table: "ContactUs",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverMedias_Driver_Id",
                table: "DriverMedias",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Driver_Id",
                table: "Messages",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User_Id",
                table: "Messages",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdminNotificationId",
                table: "Notifications",
                column: "AdminNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DriverId",
                table: "Notifications",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_UserId",
                table: "Promocodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RideTypeMLs_RideType_Id",
                table: "RideTypeMLs",
                column: "RideType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sates_Country_Id",
                table: "Sates",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsMLs_Settings_Id",
                table: "SettingsMLs",
                column: "Settings_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CancellationReason_Id",
                table: "Trips",
                column: "CancellationReason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Driver_Id",
                table: "Trips",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips",
                column: "PrimaryUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Promocode_Id",
                table: "Trips",
                column: "Promocode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RideType_Id",
                table: "Trips",
                column: "RideType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_Driver_Id",
                table: "UserDevices",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_User_Id",
                table: "UserDevices",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocode_Promocode_Id",
                table: "UserPromocode",
                column: "Promocode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocode_User_Id",
                table: "UserPromocode",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrips_Trip_Id",
                table: "UserTrips",
                column: "Trip_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrips_User_Id",
                table: "UserTrips",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMedias_Vehicle_Id",
                table: "VehicleMedias",
                column: "Vehicle_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Capacity_Id",
                table: "Vehicles",
                column: "Capacity_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Company_Id",
                table: "Vehicles",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Driver_Id",
                table: "Vehicles",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Model_Id",
                table: "Vehicles",
                column: "Model_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Type_Id",
                table: "Vehicles",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Year_Id",
                table: "Vehicles",
                column: "Year_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminSubAdminNotifications");

            migrationBuilder.DropTable(
                name: "AppRatingMLs");

            migrationBuilder.DropTable(
                name: "CancellationReasonMLs");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "DriverMedias");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RideTypeMLs");

            migrationBuilder.DropTable(
                name: "SettingsMLs");

            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.DropTable(
                name: "UserPromocode");

            migrationBuilder.DropTable(
                name: "UserTrips");

            migrationBuilder.DropTable(
                name: "VehicleMedias");

            migrationBuilder.DropTable(
                name: "VerifyNumberCodes");

            migrationBuilder.DropTable(
                name: "AppRatings");

            migrationBuilder.DropTable(
                name: "AdminNotifications");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CancellationReasons");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropTable(
                name: "RideTypes");

            migrationBuilder.DropTable(
                name: "CarCapacity");

            migrationBuilder.DropTable(
                name: "CarCompany");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarType");

            migrationBuilder.DropTable(
                name: "CarYear");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Sates");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

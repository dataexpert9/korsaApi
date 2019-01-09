using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Referrals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
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
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
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
                    table.PrimaryKey("PK_Banks", x => x.Id);
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
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCapacity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanies",
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
                    table.PrimaryKey("PK_CarCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
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
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
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
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
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
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
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
                    UniqueId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    LicenseNo = table.Column<string>(nullable: true),
                    RidesCount = table.Column<int>(nullable: false),
                    RatedRidesCount = table.Column<int>(nullable: false),
                    Wallet = table.Column<double>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    TotalMileage = table.Column<double>(nullable: false),
                    BriefIntro = table.Column<string>(nullable: true),
                    WorkHistory = table.Column<string>(nullable: true),
                    LicenseExpiry = table.Column<DateTime>(nullable: false),
                    Location_Longitude = table.Column<double>(nullable: false),
                    Location_Latitude = table.Column<double>(nullable: false),
                    IsNotificationsOn = table.Column<bool>(nullable: false),
                    CarColor = table.Column<string>(nullable: true),
                    CarName = table.Column<string>(nullable: true),
                    CarNumber = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    SignInType = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
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
                    RideTax = table.Column<double>(nullable: false),
                    Culture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPackages",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NumOfRides = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    DurationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPackages", x => x.Id);
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
                    UniqueId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DriverPreference = table.Column<int>(nullable: false),
                    RatedRidesCount = table.Column<int>(nullable: false),
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
                    UseCreditFirst = table.Column<bool>(nullable: false),
                    PrefferedPaymentMethod = table.Column<int>(nullable: false),
                    Credit = table.Column<float>(nullable: false),
                    CurrentReferralCount = table.Column<int>(nullable: false),
                    FreeRides = table.Column<int>(nullable: false),
                    TermsAndConditions = table.Column<bool>(nullable: false),
                    RidesCount = table.Column<int>(nullable: false),
                    DistanceTravelled = table.Column<double>(nullable: false),
                    Wallet = table.Column<double>(nullable: false),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    UserType = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
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
                        name: "FK_AdminNotifications_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    Admin_Id = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminTokens_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Bank_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankMLs_Banks_Bank_Id",
                        column: x => x.Bank_Id,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bank_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Banks_Bank_Id",
                        column: x => x.Bank_Id,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "CarCapacityMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarCapacity_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCapacityMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarCapacityMLs_CarCapacity_CarCapacity_Id",
                        column: x => x.CarCapacity_Id,
                        principalTable: "CarCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanyMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarCompany_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanyMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarCompanyMLs_CarCompanies_CarCompany_Id",
                        column: x => x.CarCompany_Id,
                        principalTable: "CarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarModelMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarModel_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModelMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModelMLs_CarModels_CarModel_Id",
                        column: x => x.CarModel_Id,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTypeMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarType_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypeMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarTypeMLs_CarTypes_CarType_Id",
                        column: x => x.CarType_Id,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarYearMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarYear_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarYearMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarYearMLs_CarYear_CarYear_Id",
                        column: x => x.CarYear_Id,
                        principalTable: "CarYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country_Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Country_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryMLs_Countries_Country_Id",
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
                name: "DriverPayments",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Driver_Id = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    InvoiceUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverPayments_Drivers_Driver_Id",
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
                    Driver_Id = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Capacity = table.Column<string>(nullable: true)
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
                        name: "FK_Vehicles_CarCompanies_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "CarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarModels_Model_Id",
                        column: x => x.Model_Id,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_CarTypes_Type_Id",
                        column: x => x.Type_Id,
                        principalTable: "CarTypes",
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
                    AboutRideType = table.Column<string>(nullable: true),
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
                name: "DriverSubscriptions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Driver_Id = table.Column<int>(nullable: false),
                    SubscriptionPackage_Id = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    RemainingRides = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverSubscriptions_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverSubscriptions_SubscriptionPackages_SubscriptionPackage_Id",
                        column: x => x.SubscriptionPackage_Id,
                        principalTable: "SubscriptionPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    User_Id = table.Column<int>(nullable: true),
                    Driver_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactUs_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteLocations",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Location_Longitude = table.Column<double>(nullable: false),
                    Location_Latitude = table.Column<double>(nullable: false),
                    PlaceId = table.Column<string>(nullable: true),
                    FormattedAddress = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteLocations_Users_User_Id",
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
                    ExpiryHours = table.Column<int>(nullable: false),
                    CodeType = table.Column<int>(nullable: false),
                    CouponType = table.Column<int>(nullable: false),
                    CouponAmount = table.Column<int>(nullable: false),
                    IsExpired = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocodes_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAppReferral",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvitedUser_Id = table.Column<int>(nullable: false),
                    Referrer_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAppReferral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAppReferral_Users_InvitedUser_Id",
                        column: x => x.InvitedUser_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAppReferral_Users_Referrer_Id",
                        column: x => x.Referrer_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_AdminSubAdminNotifications_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
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
                name: "Notifications",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: true),
                    Entity_Id = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: true),
                    AdminNotification_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AdminNotifications_AdminNotification_Id",
                        column: x => x.AdminNotification_Id,
                        principalTable: "AdminNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Branch_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Branches_Branch_Id",
                        column: x => x.Branch_Id,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    Branch_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchMLs_Branches_Branch_Id",
                        column: x => x.Branch_Id,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverAccounts",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Driver_Id = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountHolderName = table.Column<string>(nullable: true),
                    Branch_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverAccounts_Branches_Branch_Id",
                        column: x => x.Branch_Id,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAccounts_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    City_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityMLs_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
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
                    DriverRating = table.Column<double>(nullable: false),
                    UserRating = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    isScheduled = table.Column<bool>(nullable: false),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Promocode_Id = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CancellationReason_Id = table.Column<int>(nullable: true),
                    ReasonToCancel = table.Column<string>(nullable: true),
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
                name: "AccountMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IBN = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    Account_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountMLs_Accounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTopUps",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false),
                    Account_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTopUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTopUps_Accounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTopUps_Users_User_Id",
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

            migrationBuilder.CreateTable(
                name: "TopUpMedias",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: true),
                    TopUp_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUpMedias_BankTopUps_TopUp_Id",
                        column: x => x.TopUp_Id,
                        principalTable: "BankTopUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMLs_Account_Id",
                table: "AccountMLs",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Branch_Id",
                table: "Accounts",
                column: "Branch_Id");

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
                name: "IX_AdminTokens_AdminId",
                table: "AdminTokens",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRatingMLs_AppRating_Id",
                table: "AppRatingMLs",
                column: "AppRating_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppRatings_User_Id",
                table: "AppRatings",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankMLs_Bank_Id",
                table: "BankMLs",
                column: "Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankTopUps_Account_Id",
                table: "BankTopUps",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankTopUps_User_Id",
                table: "BankTopUps",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Bank_Id",
                table: "Branches",
                column: "Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BranchMLs_Branch_Id",
                table: "BranchMLs",
                column: "Branch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReason_Id",
                table: "CancellationReasonMLs",
                column: "CancellationReason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarCapacityMLs_CarCapacity_Id",
                table: "CarCapacityMLs",
                column: "CarCapacity_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarCompanyMLs_CarCompany_Id",
                table: "CarCompanyMLs",
                column: "CarCompany_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarModelMLs_CarModel_Id",
                table: "CarModelMLs",
                column: "CarModel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypeMLs_CarType_Id",
                table: "CarTypeMLs",
                column: "CarType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarYearMLs_CarYear_Id",
                table: "CarYearMLs",
                column: "CarYear_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country_Id",
                table: "Cities",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CityMLs_City_Id",
                table: "CityMLs",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_Driver_Id",
                table: "ContactUs",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_User_Id",
                table: "ContactUs",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMLs_Country_Id",
                table: "CountryMLs",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAccounts_Branch_Id",
                table: "DriverAccounts",
                column: "Branch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAccounts_Driver_Id",
                table: "DriverAccounts",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverMedias_Driver_Id",
                table: "DriverMedias",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverPayments_Driver_Id",
                table: "DriverPayments",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSubscriptions_Driver_Id",
                table: "DriverSubscriptions",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSubscriptions_SubscriptionPackage_Id",
                table: "DriverSubscriptions",
                column: "SubscriptionPackage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLocations_User_Id",
                table: "FavouriteLocations",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Driver_Id",
                table: "Messages",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User_Id",
                table: "Messages",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdminNotification_Id",
                table: "Notifications",
                column: "AdminNotification_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Driver_Id",
                table: "Notifications",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_User_Id",
                table: "Notifications",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_User_Id",
                table: "Promocodes",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RideTypeMLs_RideType_Id",
                table: "RideTypeMLs",
                column: "RideType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsMLs_Settings_Id",
                table: "SettingsMLs",
                column: "Settings_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TopUpMedias_TopUp_Id",
                table: "TopUpMedias",
                column: "TopUp_Id");

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
                name: "IX_UserAppReferral_InvitedUser_Id",
                table: "UserAppReferral",
                column: "InvitedUser_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAppReferral_Referrer_Id",
                table: "UserAppReferral",
                column: "Referrer_Id");

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
                name: "AccountMLs");

            migrationBuilder.DropTable(
                name: "AdminSubAdminNotifications");

            migrationBuilder.DropTable(
                name: "AdminTokens");

            migrationBuilder.DropTable(
                name: "AppRatingMLs");

            migrationBuilder.DropTable(
                name: "BankMLs");

            migrationBuilder.DropTable(
                name: "BranchMLs");

            migrationBuilder.DropTable(
                name: "CancellationReasonMLs");

            migrationBuilder.DropTable(
                name: "CarCapacityMLs");

            migrationBuilder.DropTable(
                name: "CarCompanyMLs");

            migrationBuilder.DropTable(
                name: "CarModelMLs");

            migrationBuilder.DropTable(
                name: "CarTypeMLs");

            migrationBuilder.DropTable(
                name: "CarYearMLs");

            migrationBuilder.DropTable(
                name: "CityMLs");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "CountryMLs");

            migrationBuilder.DropTable(
                name: "DriverAccounts");

            migrationBuilder.DropTable(
                name: "DriverMedias");

            migrationBuilder.DropTable(
                name: "DriverPayments");

            migrationBuilder.DropTable(
                name: "DriverSubscriptions");

            migrationBuilder.DropTable(
                name: "FavouriteLocations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RideTypeMLs");

            migrationBuilder.DropTable(
                name: "SettingsMLs");

            migrationBuilder.DropTable(
                name: "TopUpMedias");

            migrationBuilder.DropTable(
                name: "UserAppReferral");

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
                name: "Cities");

            migrationBuilder.DropTable(
                name: "SubscriptionPackages");

            migrationBuilder.DropTable(
                name: "AdminNotifications");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "BankTopUps");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CancellationReasons");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropTable(
                name: "RideTypes");

            migrationBuilder.DropTable(
                name: "CarCapacity");

            migrationBuilder.DropTable(
                name: "CarCompanies");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "CarYear");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}

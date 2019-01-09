using AppModel;
using AppModel.BindingModels;
using AppModel.CustomModels;
using AppModel.DomainModels;
using AppModel.DTOs;
using AutoMapper;
using DAL;
using DAL.DomainModels;

namespace Korsa
{
    public static class AutoMapperConfig
    {
        public static void CreateConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Settings, SettingsDTO>().ReverseMap();
                cfg.CreateMap<InvitedFriend, InvitedFriendDTO>().ReverseMap(); 
                cfg.CreateMap<RoleScreen, RoleScreenDTO>().ReverseMap(); 
                cfg.CreateMap<TopUpMedia, TopUpMediaDTO>().ReverseMap();
                cfg.CreateMap<CashSubscription, CashSubscriptionDTO>().ReverseMap();
                cfg.CreateMap<SupportConversation, SupportConversationDTO>().ReverseMap();
                cfg.CreateMap<CreditCard, CreditCardDTO>().ReverseMap();
                cfg.CreateMap<FavouriteLocationBindingModel, FavouriteLocation>().ReverseMap();
                cfg.CreateMap<AdminBindingModel, Admin>().ReverseMap();
                cfg.CreateMap<FareCalculationBindingModel, FareCalculation>().ReverseMap();
                cfg.CreateMap<FareCalculationDTO, FareCalculation>().ReverseMap();
                cfg.CreateMap<SubscriptionBindingModel, SubscriptionPackage>().ReverseMap();
                cfg.CreateMap<SupportConversationBindingModel, SupportConversation>().ReverseMap();
                cfg.CreateMap<AccountBindingModel, Account>().ReverseMap();
                cfg.CreateMap<CityBindingModel, City>().ReverseMap();
                cfg.CreateMap<CountryBindingModel, Country>().ReverseMap();
                cfg.CreateMap<AccountDTO, Account>().ReverseMap();
                cfg.CreateMap<Promocode, PromocodeDTO>().ReverseMap();
                cfg.CreateMap<DriverAccountDTO, DriverAccount>().ReverseMap();
                cfg.CreateMap<RoleDTO, Role>().ReverseMap();
                cfg.CreateMap<Notification, AdminNotificationBindingModel>().ReverseMap();

                cfg.CreateMap<BankBindingModel, Bank>().ReverseMap();
                cfg.CreateMap<CarCapacityBindingModel, CarCapacity>().ReverseMap();
                cfg.CreateMap<CarCompanyBindingModel, CarCompany>().ReverseMap();
                cfg.CreateMap<CarYearBindingModel, CarYear>().ReverseMap();
                cfg.CreateMap<CarTypeBindingModel, CarType>().ReverseMap();
                cfg.CreateMap<CarModelBindingModel, CarModel>().ReverseMap();
                cfg.CreateMap<CarCompanyBindingModel, CarCompanyML>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<CarYearBindingModel, CarYearML>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<CarTypeBindingModel, CarTypeML>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<CarCapacityBindingModel, CarCapacityML>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<CarModelBindingModel, CarModelML>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<BankBranchBindingModel, Branch>().ReverseMap();
                cfg.CreateMap<EditDriverProfileBindingModel, Branch>().ReverseMap();
                cfg.CreateMap<MailingDTO, Mailing>().ReverseMap();
                cfg.CreateMap<BankBindingModel, BankML>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<BankBranchBindingModel, BranchML>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<AccountBindingModel, AccountML>()
               .ForMember(x => x.Id, opt => opt.Ignore())
               .ForMember(x => x.CreatedDate, opt => opt.Ignore())
               .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<CountryBindingModel, CountryML>()
             .ForMember(x => x.Id, opt => opt.Ignore())
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<CityBindingModel, CityML>()
             .ForMember(x => x.Id, opt => opt.Ignore())
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<BranchDTO, Branch>().ReverseMap();
                cfg.CreateMap<BranchML, BranchMLsDTO>();
                cfg.CreateMap<AccountML, AccountMLsDTO>();

                cfg.CreateMap<TopUpDTO, TopUp>().ReverseMap();
                //.ForMember(x => x.Account, opt => opt.Ignore())
                //.ForMember(x => x.User, opt => opt.Ignore());


                cfg.CreateMap<BankDTO, Bank>().ReverseMap();
                cfg.CreateMap<CountryDTO, Country>().ReverseMap();
                cfg.CreateMap<CityDTO, CityML>().ReverseMap();
                cfg.CreateMap<CityDTO, City>().ReverseMap();

                cfg.CreateMap<CountryML, CountryMLsDTO>();
                cfg.CreateMap<CityML, CityMLsDTO>();

                cfg.CreateMap<BankMLsDTO, BankML>().ReverseMap();

                cfg.CreateMap<RideTypeBindingModel, RideType>().ReverseMap();
                cfg.CreateMap<RideTypeBindingModel, RideTypeML>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());

                cfg.CreateMap<SubscriptionPackageDTO, SubscriptionPackage>().ReverseMap();
                cfg.CreateMap<Vehicle, VehicleDTO>().ReverseMap();
                cfg.CreateMap<SettingsML, SettingsMLsDTO>();
                cfg.CreateMap<Driver, DriverDTO>();
                cfg.CreateMap<Notification, NotificationDTO>().ReverseMap();
                cfg.CreateMap<EditUserProfileBindingModel, User>().DisableCtorValidation();
                cfg.CreateMap<RegisterUserBindingModel, User>();
                cfg.CreateMap<AddUserBindingModel, User>().ReverseMap();
                cfg.CreateMap<RegisterDriverBindingModel, Driver>();
                cfg.CreateMap<RideNowBindingModel, Trip>();
                cfg.CreateMap<TripDTO, Trip>().ReverseMap();
                cfg.CreateMap<DriverPayment, DriverPaymentDTO>().ReverseMap();
                cfg.CreateMap<MessageBindingModel, Message>().ReverseMap();
                cfg.CreateMap<EditDriverProfileBindingModel, Driver>().ReverseMap();
                cfg.CreateMap<EditDriverProfileBindingModelAdmin, Driver>().ReverseMap();
                cfg.CreateMap<VehicleBindingModel, Vehicle>().ReverseMap();
                cfg.CreateMap<RideLaterBindingModel, Trip>();
                cfg.CreateMap<Locations, Location>();
                cfg.CreateMap<RideType, RideTypeDTO>();
                cfg.CreateMap<CarCompany, CarCompanyDTO>();
                cfg.CreateMap<CarModel, CarModelDTO>();
                cfg.CreateMap<CarType, CarTypeDTO>();
                cfg.CreateMap<CarCapacity, CarCapacityDTO>();
                cfg.CreateMap<CarYear, CarYearDTO>();
                cfg.CreateMap<UserDevice, UserDeviceDTO>();
                cfg.CreateMap<CancellationReason, CancellationReasonDTO>();
                cfg.CreateMap<Message, MessageDTO>();
                cfg.CreateMap<Locations, Loc>().ReverseMap();
                cfg.CreateMap<DashboardStats, DashboardStatsDTO>().ReverseMap();
                cfg.CreateMap<BaseModel, BaseModelDTO>().ReverseMap();
                cfg.CreateMap<RideTypeML, RideTypeDTO>()
                .ForMember(
                     dest => dest.Id,
                     opts => opts.MapFrom(src => src.RideType_Id)
                 )
                 .ReverseMap();
                cfg.CreateMap<CancellationReasonML, CancellationReasonDTO>()
                .ForMember(
                 dest => dest.Id,
                 opts => opts.MapFrom(src => src.CancellationReason_Id)
                )
                .ReverseMap();
                cfg.CreateMap<CarCompanyML, CarCompanyDTO>()
                .ForMember(
                 dest => dest.Id,
                 opts => opts.MapFrom(src => src.CarCompany_Id)
                )
                .ReverseMap();

                cfg.CreateMap<CarModelML, CarModelDTO>()
               .ForMember(
                dest => dest.Id,
                opts => opts.MapFrom(src => src.CarModel_Id)
               )
               .ReverseMap();
                cfg.CreateMap<CarYearML, CarYearDTO>()
               .ForMember(
                dest => dest.Id,
                opts => opts.MapFrom(src => src.CarYear_Id)
               )
               .ReverseMap();

                cfg.CreateMap<CarTypeML, CarTypeDTO>()
               .ForMember(
                dest => dest.Id,
                opts => opts.MapFrom(src => src.CarType_Id)
               )
               .ReverseMap();

                cfg.CreateMap<CarCapacityML, CarCapacityDTO>()
               .ForMember(
                dest => dest.Id,
                opts => opts.MapFrom(src => src.CarCapacity_Id)
               )
               .ReverseMap();
            });

        }
    }
}

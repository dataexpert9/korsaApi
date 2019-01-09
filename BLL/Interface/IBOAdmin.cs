using AppModel.BindingModels;
using AppModel.DTOs;
using Component.Utility;
using DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOAdmin
    {
        Admin WebPanelLogin(string username, string password);
        List<CashSubscriptionDTO> GetSubscriptionPackagesRequests();
        DashboardStats GetAdminDashboardStats();
        List<User> GetAllUsers(string startDate, string endDate, SearchType userSearchBy, string userName, string userId);
        UserDTO GetUserById(int User_Id);
        DriverDTO GetDriverById(int Driver_Id);
        Admin GetAdminById(int Id);
        List<Driver> GetAllDrivers(bool isBecomDriverRequests = false, string startDate="", string endDate="", SearchType userSearchBy=SearchType.All, string userName="", string userId ="");
        bool ChangeUserStatuses(ChangeUserStatusListModel model);
        bool ChangeDriverStatuses(ChangeUserStatusListModel model);
        bool SaveDriverRequestStatuses(ChangeUserStatusListModel model);

        int ChangePassword(int Id, string OldPassword, string NewPassword);
        List<Trip> GetAllRides(string startDate, string endDate, RideSearchType filterType, int rideId);
        Settings SetSettings(SettingsDTO settings);
        List<TripDTO> GetRidesById(int id, UserTypes userType);
        AddUpdateResponse AddEditSubscriptionPackage(SubscriptionBindingModel model);
        bool DeleteSubscriptionPackage(int id);
        List<SubscriptionPackage> GetAllPackages();
        SubscriptionPackage GetSubscriptionPackageById(int? id);
        bool DeleteRideType(int id);
        RideTypeDTO GetRideTypeById(int? id, CultureType culture = CultureType.English);

        bool DeleteBank(int id);
        AddUpdateResponse AddEditBankBranch(BankBranchBindingModel model);
        AddUpdateResponse AddEditAccount(AccountBindingModel model);
        AddUpdateResponse AddEditRideType(RideTypeBindingModel model, string defImagePath, string selectedImagePath);
        AddUpdateResponse AddEditBank(BankBindingModel model);
        Admin AddUpdateAdmin(AdminBindingModel model);

        bool DeleteBranch(int id);
        bool DeleteAccount(int id);
        List<TopUpDTO> GetTopUpRequests();
        BankDTO GetBankById(int? id, CultureType culture);
        BranchDTO GetBranchById(int? id, CultureType culture);
        AccountDTO GetAccountById(int? id, CultureType culture);
        bool AcceptTopUpRequest(TopUpRequestBindingModel model, int userId);
        bool AcceptCashSubscriptionRequest(CashSubscriptionRequestBindingModel model, int driverId);
        AddUpdateResponse AddUpdateCountry(CountryBindingModel model);
        AddUpdateResponse AddUpdateCity(CityBindingModel model);
        CountryDTO GetCountryById(int? id, CultureType culture);
        CityDTO GetCityById(int? id, CultureType culture);
        bool DeleteCountry(int id);
        bool DeleteCity(int id);
        AddUpdateResponse AddEditCarCompany(CarCompanyBindingModel model);
        AddUpdateResponse AddEditCarModels(CarModelBindingModel model);
        AddUpdateResponse AddEditCarYear(CarYearBindingModel model);
        AddUpdateResponse AddEditCarCapacity(CarCapacityBindingModel model);
        AddUpdateResponse AddEditCarType(CarTypeBindingModel model);
        bool DeleteCarYear(int id);
        bool DeleteCarModel(int id);
        bool DeleteCarCapacity(int id);
        bool DeleteCarType(int id);
        bool DeleteAdmin(int id);
        PromocodeDTOList AddMapCode(AddPromocodeBindingModel model);
        bool DeleteCarCompany(int id);
        CarCompanyDTO GetCarCompanyById(int value, CultureType culture);
        CarModelDTO GetCarModelById(int value, CultureType culture);
        CarTypeDTO GetCarTypeById(int value, CultureType culture);
        CarCapacityDTO GetCarCapacityById(int value, CultureType culture);
        CarYearDTO GetCarYearById(int value, CultureType culture);
        bool PayOutstandingBalance(DriverPaymentBindingModelList model);
        List<DriverPaymentDTO> GetDriverPaymentHistory(int driverId);
        DriverPaymentDTO GetPaymentDetailsById(int id);
        PromocodeDTOList GetCodes();
        bool DeletePromocode(int id);
        InvitedFriendDTOList GetUserReferralsByUserId(int user_Id);
        List<SupportConversationDTO> GetSupportConversationsList(string search);
        List<AdminDTO> GetAdmins();
        AddUpdateResponse AddUpdateRole(RoleScreenModel model);
        SearchRoleListDTO SearchRoles();
        bool DeleteEntity(KorsaEntityTypes type, int id);
        RoleDTO GetRoleById(int value, CultureType culture);
        AddUpdateResponse AddUpdateFareCalculation(FareCalculationBindingModel model);
        List<FareCalculationDTO> GetAllFareCalculation(int cityId, PaymentMethods paymentType);
        FareCalculationDTO GetFareCalculationDetailsById(int id, CultureType culture);
        List<InvitedFriendDTO> GetAppReferrers(string username="");
        List<TopUpDTO> UserWalletReport(string uniqueId);
        List<DriverPaymentDTO> PaymentReport(string uniqueId, string StartDate = "", string EndDate = "");
        EarningReportDTO TotalEarningsReport(string startDate = "", string endDate = "");
        DriverLogDTOList DriverLogReport(string startDate, string endDate);
    }
}

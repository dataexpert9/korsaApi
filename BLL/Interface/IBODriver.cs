using System.Collections.Generic;
using AppModel;
using AppModel.BindingModels;
using AppModel.DTOs;
using Component.Utility;
using DAL.DomainModels;

namespace BLL.Interface
{
    public interface IBODriver
    {
        DriverDTO InsertDriver(Driver driver);
        bool Exists(string username, string phoneNum);
        DriverDTO AuthenticateCredentials(string username, string password, CultureType culture = CultureType.English);
        Driver GetDriverById(int id);
        VehicleDTO GetVehicleById(int id, CultureType culture);
        List<VehicleDTO> GetVehiclesByDriverId(int id, CultureType culture);
        VehicleDTO AddEditVehicle(int driverId, VehicleBindingModel model, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English);
        bool UpdateDriver(Driver user);
        List<SubscriptionPackageDTO> GetAllSubscriptionPackages();
        SettingsDTO GetSettings();
        Driver ResetForgotPassword(string email, string code);
        bool VerifyUserName(string username, UserTypes userType);
        DriverDTO AddDriver(RegisterDriverBindingModel model, string imagePath, List<string> licenseFrontUrls, List<string> licenseBackUrls, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English);
        DriverDTO AddDriverAdmin(AddDriverBindingModel model, string imagePath, List<string> licenseFrontUrls, List<string> licenseBackUrls, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English);
        List<CarCompanyDTO> GetAllCarMakers(CultureType culture);
        List<CarModelDTO> GetAllCarModels(CultureType culture);
        List<CarYearDTO> GetAllCarYears(CultureType culture);
        List<CarTypeDTO> GetAllCarTypes(CultureType culture);
        List<CarCapacityDTO> GetAllCarCapacities(CultureType culture);
        Driver GetDriver(string usernameOrCellNum);
        DriverDTO EditDriverProfile(int driverId, string imagePath, EditDriverProfileBindingModel model, List<string> licenseFrontUrls, List<string> licenseBackUrls);
        DriverDTO EditDriverProfileAdmin(int driverId, string imagePath, EditDriverProfileBindingModelAdmin model, List<string> licenseFrontUrls, List<string> licenseBackUrls);
        List<BankDTO> GetAllBanks();
        List<BranchDTO> GetAllBranches();
        List<BranchDTO> GetAllBranchesByBankId(int id);
        List<AccountDTO> GetAllAccounts();
        List<CountryDTO> GetAllCountries(bool isAll);
        List<CityDTO> GetAllCities(bool isAll);
        bool SetActiveVehicle(int driverId,int vehicleId);
        bool AddBankCashSubscription(int id, List<string> topUpReceiptUrls, AddBankCashSbuscriptionBindingModel model);
        DriverDTO GetDriverFromToken(int id);
        List<CityDTO> GetAllCitiesByCountryId(int id,bool isAll);
        List<Driver> GetDriversbyIds(string driverIds, string searchString = "");
        bool LogDriver(int driverId, bool isOnline);
    }
}

using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Implementation
{
    public class BODriver : IBODriver
    {
        public DataContext _dbContext { get; set; }

        public BODriver(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string username, string phoneNum)
        {
            try
            {
                return _dbContext.Drivers.Any(x => x.PhoneNo == phoneNum || x.Username == username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DriverDTO InsertDriver(Driver driver)
        {
            try
            {
                if (driver.Location == null)
                {
                    driver.Location = new Location();
                }

                _dbContext.Drivers.Add(driver);
                _dbContext.SaveChanges();
                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                return driverDTO;
            }

            catch (Exception ex)
            {
                throw;
            }
        }


        public DriverDTO AddDriver(RegisterDriverBindingModel model, string imagePath, List<string> licenseFrontUrls, List<string> licenseBackUrls, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English)
        {
            try
            {
                int code = new Random().Next(11111, 999999);
                generateId: string id = code.ToString();
                if (_dbContext.Drivers.Any(x => x.UniqueId == id && !x.IsDeleted))
                {
                    goto generateId;
                }

                Driver driver = new Driver { UniqueId = id, FullName = model.FullName, ProfilePictureUrl = imagePath, PhoneNo = model.PhoneNo, Username = model.Username, Email = model.Email, Password = model.Password, Gender = (int)model.Gender, DateOfBirth = model.DateOfBirth, HomeAddress = model.HomeAddress, LicenseNo = model.LicenseNo, LicenseExpiry = model.LicenseExpiry, SignInType = (int)UserTypes.Driver, BriefIntro = model.BriefIntro, WorkHistory = model.WorkHistory, Rating = 0, IsNotificationsOn = true, Status = DriverAccountStatus.RequestPending, LoginStatus=LoginStatus.Offline };

                Vehicle vehicle = new Vehicle { isActive = true, Number = model.Number, RegistrationExpiry = model.RegisterationExpiry, Company_Id = model.Company_Id, Model_Id = model.Model_Id, Year_Id = model.Year_Id, Type_Id = model.Type_Id, Capacity_Id = model.Capacity_Id, Driver_Id = driver.Id, RideType_Id = model.RideType_Id };

                foreach (var img in licenseFrontUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseFront, Driver_Id = driver.Id });
                }

                foreach (var img in licenseBackUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseBack, Driver_Id = driver.Id });
                }

                foreach (var img in registrationCopyUrls)
                {
                    vehicle.Medias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.RegistrationCopyImage, Vehicle_Id = vehicle.Id });
                }

                foreach (var img in carImagesUrls)
                {
                    vehicle.Medias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.CarImage, Vehicle_Id = vehicle.Id });
                }

                driver.Vehicles.Add(vehicle);


                if (model.Branch_Id != null && model.Branch_Id.Value > 0)
                {
                    DriverAccount driverAccount = new DriverAccount { Driver_Id = driver.Id, AccountNumber = model.AccountNumber, AccountHolderName = model.AccountHolderName, Branch_Id = model.Branch_Id.Value, isActive = true };
                    driver.DriverAccounts.Add(driverAccount);
                }
                _dbContext.Drivers.Add(driver);
                _dbContext.SaveChanges();

                //setting strings of vehicle model
                vehicle = _dbContext.Vehicles.Include(a => a.Medias)
                  .Include(b => b.RideType).ThenInclude(c => c.RideTypeMLsList)
                  .Include(b => b.CarCompany).ThenInclude(c => c.CarCompanyMLsList)
                  .Include(b => b.CarModel).ThenInclude(c => c.CarModelMLsList)
                  .Include(b => b.CarType).ThenInclude(c => c.CarTypeMLsList)
                  .Include(b => b.CarYear).ThenInclude(c => c.CarYearMLsList)
                  .Include(b => b.CarCapacity).ThenInclude(c => c.CarCapacityMLsList)
                  .FirstOrDefault(x => x.Id == driver.Vehicles.First().Id && !x.IsDeleted);
                vehicle.Medias = vehicle.Medias.Where(x => x.IsDeleted == false).ToList();
                vehicle.Company = vehicle.CarCompany.CarCompanyMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Model = vehicle.CarModel.CarModelMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Type = vehicle.CarType.CarTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Year = vehicle.CarYear.CarYearMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Capacity = vehicle.CarCapacity.CarCapacityMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Classification = vehicle.RideType.RideTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                _dbContext.SaveChanges();

                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                return driverDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DriverDTO AddDriverAdmin(AddDriverBindingModel model, string imagePath, List<string> licenseFrontUrls, List<string> licenseBackUrls, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English)
        {
            try
            {
                int code = new Random().Next(11111, 999999);
                generateId: string id = code.ToString();
                if (_dbContext.Drivers.Any(x => x.UniqueId == id && !x.IsDeleted))
                {
                    goto generateId;
                }

                Driver driver = new Driver { City_Id = model.City_Id, UniqueId = id, FullName = model.FullName, ProfilePictureUrl = imagePath, PhoneNo = model.PhoneNo, Username = model.Username, Email = model.Email, Password = model.Password, Gender = (int)model.Gender, DateOfBirth = model.DateOfBirth, HomeAddress = model.HomeAddress, LicenseNo = model.LicenseNo, LicenseExpiry = model.LicenseExpiry, SignInType = (int)UserTypes.Driver, BriefIntro = model.BriefIntro, WorkHistory = model.WorkHistory, Rating = 0, IsNotificationsOn = true, Status = DriverAccountStatus.RequestApproved, LoginStatus=LoginStatus.Offline };
                _dbContext.Drivers.Add(driver);

                Vehicle vehicle = new Vehicle { RideType_Id = model.RideType_Id, Number = model.Number, RegistrationExpiry = model.RegisterationExpiry, Company_Id = model.Company_Id, Model_Id = model.Model_Id, Year_Id = model.Year_Id, Type_Id = model.Type_Id, Capacity_Id = model.Capacity_Id, Driver_Id = driver.Id };
                _dbContext.Vehicles.Add(vehicle);

                foreach (var img in licenseFrontUrls)
                {
                    _dbContext.DriverMedias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseFront, Driver_Id = driver.Id });
                }

                foreach (var img in licenseBackUrls)
                {
                    _dbContext.DriverMedias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseBack, Driver_Id = driver.Id });
                }

                foreach (var img in registrationCopyUrls)
                {
                    _dbContext.VehicleMedias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.RegistrationCopyImage, Vehicle_Id = vehicle.Id });
                }

                foreach (var img in carImagesUrls)
                {
                    _dbContext.VehicleMedias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.CarImage, Vehicle_Id = vehicle.Id });
                }

                _dbContext.SaveChanges();

                //setting strings of vehicle model
                vehicle = _dbContext.Vehicles.Include(a => a.Medias)
                  .Include(b => b.RideType).ThenInclude(c => c.RideTypeMLsList)
                  .Include(b => b.CarCompany).ThenInclude(c => c.CarCompanyMLsList)
                  .Include(b => b.CarModel).ThenInclude(c => c.CarModelMLsList)
                  .Include(b => b.CarType).ThenInclude(c => c.CarTypeMLsList)
                  .Include(b => b.CarYear).ThenInclude(c => c.CarYearMLsList)
                  .Include(b => b.CarCapacity).ThenInclude(c => c.CarCapacityMLsList)
                  .FirstOrDefault(x => x.Id == driver.Vehicles.First().Id && !x.IsDeleted);
                vehicle.Medias = vehicle.Medias.Where(x => x.IsDeleted == false).ToList();
                vehicle.Company = vehicle.CarCompany.CarCompanyMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Model = vehicle.CarModel.CarModelMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Type = vehicle.CarType.CarTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Year = vehicle.CarYear.CarYearMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Capacity = vehicle.CarCapacity.CarCapacityMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Classification = vehicle.RideType.RideTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                _dbContext.SaveChanges();

                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                return driverDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public VehicleDTO AddEditVehicle(int driverId, VehicleBindingModel model, List<string> carImagesUrls, List<string> registrationCopyUrls, CultureType culture = CultureType.English)
        {
            try
            {
                Vehicle vehicle = new Vehicle();

                if (model.Id == 0)
                {
                    vehicle = new Vehicle { Number = model.Number, RegistrationExpiry = model.RegistrationExpiry, Company_Id = model.Company_Id, Model_Id = model.Model_Id, Year_Id = model.Year_Id, Type_Id = model.Type_Id, Capacity_Id = model.Capacity_Id, Driver_Id = driverId, RideType_Id = model.RideType_Id.Value };
                    _dbContext.Vehicles.Add(vehicle);
                }
                else
                {
                    vehicle = _dbContext.Vehicles.Include(a => a.Medias).FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                    Mapper.Map(model, vehicle);
                    if (!String.IsNullOrEmpty(model.ImagesToRemoveIds))
                    {
                        List<int> imagesIds = model.ImagesToRemoveIds.Split(',').Select(int.Parse).ToList();
                        foreach (VehicleMedia item in vehicle.Medias)
                        {
                            if (imagesIds.Contains(item.Id))
                            {
                                item.IsDeleted = true;
                            }
                        }
                    }
                }

                foreach (var img in registrationCopyUrls)
                {
                    vehicle.Medias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.RegistrationCopyImage });
                }

                foreach (var img in carImagesUrls)
                {
                    vehicle.Medias.Add(new VehicleMedia { MediaUrl = img, Type = MediaType.CarImage });
                }

                _dbContext.SaveChanges();


                //setting strings of vehicle model
                vehicle = _dbContext.Vehicles.Include(a => a.Medias)
                  .Include(b => b.RideType).ThenInclude(c => c.RideTypeMLsList)
                  .Include(b => b.CarCompany).ThenInclude(c => c.CarCompanyMLsList)
                  .Include(b => b.CarModel).ThenInclude(c => c.CarModelMLsList)
                  .Include(b => b.CarType).ThenInclude(c => c.CarTypeMLsList)
                  .Include(b => b.CarYear).ThenInclude(c => c.CarYearMLsList)
                  .Include(b => b.CarCapacity).ThenInclude(c => c.CarCapacityMLsList)
                  .FirstOrDefault(x => x.Id == vehicle.Id && !x.IsDeleted);
                vehicle.Medias = vehicle.Medias.Where(x => x.IsDeleted == false).ToList();
                vehicle.Company = vehicle.CarCompany.CarCompanyMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Model = vehicle.CarModel.CarModelMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Type = vehicle.CarType.CarTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Year = vehicle.CarYear.CarYearMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Capacity = vehicle.CarCapacity.CarCapacityMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                vehicle.Classification = vehicle.RideType.RideTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;

                _dbContext.SaveChanges();
                VehicleDTO vehicleDTO = Mapper.Map<Vehicle, VehicleDTO>(vehicle);
                return vehicleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DriverDTO EditDriverProfile(int driverId, string imagePath, EditDriverProfileBindingModel model, List<string> licenseFrontUrls, List<string> licenseBackUrls)
        {
            try
            {
                Driver driver = new Driver();
                driver = _dbContext.Drivers.Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                      .Include(x => x.Medias)
                      .Include(x => x.DriverAccounts)

                      .FirstOrDefault(x => x.Id == driverId && !x.IsDeleted);
                model.PhoneNo = driver.PhoneNo;
                Mapper.Map(model, driver);
                if (!String.IsNullOrEmpty(imagePath))
                {
                    driver.ProfilePictureUrl = imagePath;
                }

                if (model.Branch_Id.HasValue)
                {
                    DriverAccount driverAccount = (_dbContext.DriverAccounts.Any(x => x.AccountNumber == model.AccountNumber && x.Branch_Id == model.Branch_Id && x.Driver_Id == driverId)) ? _dbContext.DriverAccounts.FirstOrDefault(x => x.AccountNumber == model.AccountNumber) : new DriverAccount { Driver_Id = driverId, AccountNumber = model.AccountNumber, AccountHolderName = model.AccountHolderName, Branch_Id = model.Branch_Id.Value, isActive = true };
                    foreach (var item in driver.DriverAccounts)
                    {
                        item.isActive = false;
                    }

                    if (driverAccount.Id == 0)
                    {
                        _dbContext.DriverAccounts.Add(driverAccount);
                    }
                    else
                    {
                        driverAccount.isActive = true;
                        driverAccount.AccountHolderName = model.AccountHolderName;
                        driverAccount.AccountNumber = model.AccountNumber;
                        driverAccount.Branch_Id = model.Branch_Id.Value;
                    }
                }



                //RemoveExistingRemovedImages
                if (!String.IsNullOrEmpty(model.ImagesToRemoveIds))
                {
                    List<int> imagesIds = model.ImagesToRemoveIds.Split(',').Select(int.Parse).ToList();
                    foreach (DriversMedia item in driver.Medias)
                    {
                        if (imagesIds.Contains(item.Id))
                        {
                            item.IsDeleted = true;
                        }
                    }
                }

                foreach (var img in licenseFrontUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseFront, Driver_Id = driver.Id });
                }

                foreach (var img in licenseBackUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseBack, Driver_Id = driver.Id });
                }

                //if (licenseBackUrls.Count>0|| licenseFrontUrls.Count>0)
                //{
                //    driver.Status = DriverAccountStatus.RequestPending;//Verify the documents of driver again Hence his status is set to pending again
                //}
                _dbContext.SaveChanges();

                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                return driverDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DriverDTO EditDriverProfileAdmin(int driverId, string imagePath, EditDriverProfileBindingModelAdmin model, List<string> licenseFrontUrls, List<string> licenseBackUrls)
        {
            try
            {
                Driver driver = new Driver();
                driver = _dbContext.Drivers.Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                      .Include(x => x.Medias)
                      .Include(x => x.DriverAccounts)
                      .FirstOrDefault(x => x.Id == driverId && !x.IsDeleted);
                model.PhoneNo = driver.PhoneNo;
                Mapper.Map(model, driver);
                if (!String.IsNullOrEmpty(imagePath))
                {
                    driver.ProfilePictureUrl = imagePath;
                }

                if (model.Branch_Id.HasValue)
                {
                    DriverAccount driverAccount = (_dbContext.DriverAccounts.Any(x => x.AccountNumber == model.AccountNumber && x.Branch_Id == model.Branch_Id && x.Driver_Id == driverId)) ? _dbContext.DriverAccounts.FirstOrDefault(x => x.AccountNumber == model.AccountNumber) : new DriverAccount { Driver_Id = driverId, AccountNumber = model.AccountNumber, AccountHolderName = model.AccountHolderName, Branch_Id = model.Branch_Id.Value, isActive = true };
                    foreach (var item in driver.DriverAccounts)
                    {
                        item.isActive = false;
                    }

                    if (driverAccount.Id == 0)
                    {
                        _dbContext.DriverAccounts.Add(driverAccount);
                    }
                    else
                    {
                        driverAccount.isActive = true;
                        driverAccount.AccountHolderName = model.AccountHolderName;
                        driverAccount.AccountNumber = model.AccountNumber;
                        driverAccount.Branch_Id = model.Branch_Id.Value;
                    }
                }



                //RemoveExistingRemovedImages
                if (!String.IsNullOrEmpty(model.ImagesToRemoveIds))
                {
                    List<int> imagesIds = model.ImagesToRemoveIds.Split(',').Select(int.Parse).ToList();
                    foreach (DriversMedia item in driver.Medias)
                    {
                        if (imagesIds.Contains(item.Id))
                        {
                            item.IsDeleted = true;
                        }
                    }
                }

                foreach (var img in licenseFrontUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseFront, Driver_Id = driver.Id });
                }

                foreach (var img in licenseBackUrls)
                {
                    driver.Medias.Add(new DriversMedia { MediaUrl = img, Type = MediaType.LicenseBack, Driver_Id = driver.Id });
                }

                //As admin is uploading himself so it's not needed to send driver to pending state
                //if (licenseBackUrls.Count > 0 || licenseFrontUrls.Count > 0)
                //{
                //    driver.Status = DriverAccountStatus.RequestPending;//Verify the documents of driver again Hence his status is set to pending again
                //}

                _dbContext.SaveChanges();


                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                return driverDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DriverDTO AuthenticateCredentials(string username, string password, CultureType culture = CultureType.English)
        {
            try
            {
                var driver = _dbContext.Drivers
                      .Include(x => x.Medias)
                      .Include(x => x.DriverAccounts).ThenInclude(x => x.Branch).ThenInclude(a => a.BranchMLsList)
                      .Include(x => x.DriverAccounts).ThenInclude(x => x.Branch).ThenInclude(a => a.Bank).ThenInclude(a => a.BankMLsList)
                      .Include(x => x.CreditCards)
                      .Include(x => x.CashSubscriptions).ThenInclude(x => x.SubscriptionPackage)
                      .Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                     .FirstOrDefault(x => (x.Username == username || x.PhoneNo == username) && x.Password == password && x.IsDeleted == false && x.Status == DriverAccountStatus.RequestApproved);

                if (driver != null)
                {
                    driver.Medias = driver.Medias.Where(x => x.IsDeleted == false).ToList();
                    driver.Vehicles = driver.Vehicles.Where(x => x.isActive == true).ToList();
                    driver.CashSubscriptions = driver.CashSubscriptions.Where(x => x.isActive && x.Status == TopUpStatus.Accepted).ToList();

                    DriverDTO driverDTO = new DriverDTO();
                    Mapper.Map(driver, driverDTO);
                    foreach (var item in driver.DriverAccounts)// Will always execute only once
                    {
                        BranchML branchML = item.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                        BankML bankML = item.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                        Mapper.Map(branchML, driverDTO.DriverAccounts[0].Branch.English);
                        Mapper.Map(bankML, driverDTO.DriverAccounts[0].Branch.Bank.English);
                        driverDTO.DriverAccounts[0].Branch.Arabic = null;
                        driverDTO.DriverAccounts[0].Branch.Bank.Arabic = null;
                    }
                    if (driverDTO.CashSubscriptions.Any(x => x.isActive == true))
                    {
                        driverDTO.CashSubscriptions.First(x => x.isActive == true).Driver = null;
                    }
                    return driverDTO;

                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Driver ResetForgotPassword(string email, string code)
        {

            try
            {
                Driver driver = _dbContext.Drivers.FirstOrDefault(x => x.Email == email);
                if (driver == null)
                {
                    return null;
                }

                driver.Password = CryptoHelper.Hash(code.ToString());
                _dbContext.SaveChanges();
                return driver;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public Driver GetDriverById(int Id)
        {
            try
            {

                Driver response = _dbContext.Drivers.Include(x => x.Medias)
                      .Include(x => x.DriverAccounts)
                      .Include(x => x.CreditCards)
                      .Include(x => x.CashSubscriptions).ThenInclude(x => x.SubscriptionPackage)
                      .Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                      .FirstOrDefault(x => x.Id == Id);

                response.Medias = response.Medias.Where(x => x.IsDeleted == false).ToList();
                response.Vehicles = response.Vehicles.Where(x => x.isActive).ToList();
                response.CashSubscriptions = response.CashSubscriptions.Where(x => x.isActive && x.Status == TopUpStatus.Accepted).ToList();
                response.DriverAccounts = response.DriverAccounts.Where(x => x.isActive).ToList();
                foreach (var item in response.Vehicles)
                {
                    item.Medias = item.Medias.Where(x => x.IsDeleted == false).ToList();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DriverDTO GetDriverFromToken(int Id)
        {
            try
            {
                Driver driver =
                    //_dbContext.Drivers.Include(x => x.Medias)
                    //  .Include(x => x.DriverAccounts)
                    //  .Include(x => x.CreditCards)
                    //  .Include(x => x.CashSubscriptions).ThenInclude(x => x.SubscriptionPackage)
                    //  .Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                    _dbContext.Drivers
                      .Include(x => x.Medias)
                      .Include(x => x.DriverAccounts).ThenInclude(x => x.Branch).ThenInclude(a => a.BranchMLsList)
                      .Include(x => x.DriverAccounts).ThenInclude(x => x.Branch).ThenInclude(a => a.Bank).ThenInclude(a => a.BankMLsList)
                      .Include(x => x.CreditCards)
                      .Include(x => x.CashSubscriptions).ThenInclude(x => x.SubscriptionPackage)
                      .Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                      .FirstOrDefault(x => x.Id == Id);

                if (driver != null)
                {
                    driver.Medias = driver.Medias.Where(x => x.IsDeleted == false).ToList();
                    driver.Vehicles = driver.Vehicles.Where(x => x.isActive).ToList();
                    driver.CashSubscriptions = driver.CashSubscriptions.Where(x => x.isActive && x.Status == TopUpStatus.Accepted).ToList();
                    driver.DriverAccounts = driver.DriverAccounts.Where(x => x.isActive).ToList();
                    foreach (var item in driver.Vehicles)
                    {
                        item.Medias = item.Medias.Where(x => x.IsDeleted == false).ToList();
                    }

                    DriverDTO driverDTO = new DriverDTO();
                    Mapper.Map(driver, driverDTO);
                    foreach (var item in driver.DriverAccounts)// Will always execute only once
                    {
                        BranchML branchML = item.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                        BankML bankML = item.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                        Mapper.Map(branchML, driverDTO.DriverAccounts[0].Branch.English);
                        Mapper.Map(bankML, driverDTO.DriverAccounts[0].Branch.Bank.English);
                        driverDTO.DriverAccounts[0].Branch.Arabic = null;
                        driverDTO.DriverAccounts[0].Branch.Bank.Arabic = null;
                    }

                    if (driverDTO.CashSubscriptions.Any(x => x.isActive == true))
                    {
                        driverDTO.CashSubscriptions.First(x => x.isActive == true).Driver = null;
                    }
                    return driverDTO;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public bool SetActiveVehicle(int driverId, int vehicleId)
        {
            try
            {
                Driver response = _dbContext.Drivers.Include(x => x.Vehicles)
                              .FirstOrDefault(x => x.Id == driverId);
                foreach (var item in response.Vehicles)
                {
                    item.isActive = false;
                }

                response.Vehicles.FirstOrDefault(x => x.Id == vehicleId).isActive = true;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VehicleDTO GetVehicleById(int Id, CultureType culture)
        {
            try
            {
                Vehicle vehicle = _dbContext.Vehicles.Include(a => a.Medias)
                    .FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
                vehicle.Medias = vehicle.Medias.Where(x => x.IsDeleted == false).ToList();

                VehicleDTO vehicleDTO = Mapper.Map<Vehicle, VehicleDTO>(vehicle);
                return vehicleDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VehicleDTO> GetVehiclesByDriverId(int id, CultureType culture)
        {
            try
            {
                var vehicles = _dbContext.Vehicles.Include(a => a.Medias)
                    .Where(x => x.Driver_Id == id && !x.IsDeleted).ToList();
                foreach (var vehicle in vehicles)
                {
                    vehicle.Medias = vehicle.Medias.Where(x => x.IsDeleted == false).ToList();
                }

                List<VehicleDTO> response = new List<VehicleDTO>();
                Mapper.Map(vehicles, response);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateDriver(Driver driver)
        {
            try
            {
                _dbContext.Drivers.Update(driver);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }
        public bool VerifyUserName(string username, UserTypes userType)
        {
            try
            {
                if (userType == UserTypes.User)
                {
                    if (_dbContext.Users.Any(x => x.UserName == username))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (_dbContext.Drivers.Any(x => x.Username == username))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public List<CarCompanyDTO> GetAllCarMakers(CultureType culture)
        {

            try
            {
                List<CarCompany> CarCompaniesList = _dbContext.CarCompanies.Include(x => x.CarCompanyMLsList).Where(x => (x.Culture == CultureType.Both || x.Culture == culture) && x.IsDeleted == false).ToList();
                List<CarCompanyDTO> CarCompaniesResponseList = Mapper.Map<List<CarCompany>, List<CarCompanyDTO>>(CarCompaniesList);
                CarCompanyDTO cancellationReasonsDTO = new CarCompanyDTO();
                for (int i = 0; i < CarCompaniesList.Count; i++)
                {
                    CarCompanyML CarCompanyML = CarCompaniesList[i].CarCompanyMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(CarCompanyML, CarCompaniesResponseList[i]);
                }
                return CarCompaniesResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CarModelDTO> GetAllCarModels(CultureType culture)
        {

            try
            {
                List<CarModel> CarModelsList = _dbContext.CarModels.Include(x => x.CarModelMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture && x.IsDeleted == false).ToList();
                List<CarModelDTO> CarModelsResponseList = Mapper.Map<List<CarModel>, List<CarModelDTO>>(CarModelsList);
                CarYearDTO CarModelDTO = new CarYearDTO();
                for (int i = 0; i < CarModelsList.Count; i++)
                {
                    CarModelML CarModelML = CarModelsList[i].CarModelMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(CarModelML, CarModelsResponseList[i]);
                }
                return CarModelsResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CarYearDTO> GetAllCarYears(CultureType culture)
        {

            try
            {
                List<CarYear> carYearsList = _dbContext.CarYear.Include(x => x.CarYearMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture && x.IsDeleted == false).ToList();
                List<CarYearDTO> carYearsResponseList = Mapper.Map<List<CarYear>, List<CarYearDTO>>(carYearsList);
                CarYearDTO CarYearDTO = new CarYearDTO();
                for (int i = 0; i < carYearsList.Count; i++)
                {
                    CarYearML CarYearML = carYearsList[i].CarYearMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(CarYearML, carYearsResponseList[i]);
                }
                return carYearsResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CarTypeDTO> GetAllCarTypes(CultureType culture)
        {

            try
            {
                List<CarType> carTypesList = _dbContext.CarTypes.Include(x => x.CarTypeMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture && x.IsDeleted == false).ToList();
                List<CarTypeDTO> carTypesResponseList = Mapper.Map<List<CarType>, List<CarTypeDTO>>(carTypesList);
                CarTypeDTO CarTypeDTO = new CarTypeDTO();
                for (int i = 0; i < carTypesList.Count; i++)
                {
                    CarTypeML CarTypeML = carTypesList[i].CarTypeMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(CarTypeML, carTypesResponseList[i]);
                }
                return carTypesResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CarCapacityDTO> GetAllCarCapacities(CultureType culture)
        {

            try
            {
                List<CarCapacity> CarCapacityList = _dbContext.CarCapacity.Include(x => x.CarCapacityMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture && x.IsDeleted == false).ToList();
                List<CarCapacityDTO> carCapacityResponseList = Mapper.Map<List<CarCapacity>, List<CarCapacityDTO>>(CarCapacityList);
                CarCapacityDTO CarCapacityDTO = new CarCapacityDTO();
                for (int i = 0; i < CarCapacityList.Count; i++)
                {
                    CarCapacityML CarCapacityML = CarCapacityList[i].CarCapacityMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(CarCapacityML, carCapacityResponseList[i]);
                }
                return carCapacityResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public Driver GetDriver(string usernameOrCellNum)
        {
            try
            {
                var driver = _dbContext.Drivers.FirstOrDefault(x => (x.PhoneNo == usernameOrCellNum || x.Username == usernameOrCellNum) && !x.IsDeleted);
                return driver;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public List<SubscriptionPackageDTO> GetAllSubscriptionPackages()
        {

            try
            {
                List<SubscriptionPackageDTO> packagesDTOs = new List<SubscriptionPackageDTO>();
                List<SubscriptionPackage> packages = new List<SubscriptionPackage>();

                packages = _dbContext.SubscriptionPackages.Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(packages, packagesDTOs);
                return packagesDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }


        public List<BankDTO> GetAllBanks()
        {

            try
            {
                List<BankDTO> bankDTOs = new List<BankDTO>();
                List<Bank> banks = new List<Bank>();

                banks = _dbContext.Banks.Include(z => z.BankMLsList).Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(banks, bankDTOs);
                for (int i = 0; i < banks.Count; i++)
                {

                    BankML englishML = banks[i].BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, bankDTOs[i].English);
                }
                return bankDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }


        public List<BranchDTO> GetAllBranches()
        {

            try
            {
                List<BranchDTO> branchDTOs = new List<BranchDTO>();
                List<Branch> branches = new List<Branch>();
                branches = _dbContext.Branches.Include(z => z.BranchMLsList).Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(branches, branchDTOs);
                for (int i = 0; i < branches.Count; i++)
                {
                    BranchML englishML = branches[i].BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, branchDTOs[i].English);
                }
                return branchDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }


        public List<AccountDTO> GetAllAccounts()
        {

            try
            {
                List<AccountDTO> accountDTOs = new List<AccountDTO>();
                List<Account> accounts = new List<Account>();

                accounts = _dbContext.Accounts.Include(z => z.AccountMLsList)
                    .Include(a => a.Branch).ThenInclude(a => a.BranchMLsList)
                    .Include(a => a.Branch).ThenInclude(a => a.Bank)
                    .Include(a => a.Branch).ThenInclude(a => a.Bank).ThenInclude(a => a.BankMLsList)
                    .Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(accounts, accountDTOs);
                for (int i = 0; i < accounts.Count; i++)
                {

                    AccountML englishML = accounts[i].AccountMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, accountDTOs[i].English);
                    BranchML branchML = accounts[i].Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    BankML bankML = accounts[i].Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(branchML, accountDTOs[i].Branch.English);
                    Mapper.Map(bankML, accountDTOs[i].Branch.Bank.English);
                    accountDTOs[i].Arabic = null;
                    accountDTOs[i].Branch.Arabic = null;
                    accountDTOs[i].Branch.Bank.Arabic = null;

                }
                return accountDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CountryDTO> GetAllCountries(bool isAll)
        {

            try
            {
                List<CountryDTO> countryDTOs = new List<CountryDTO>();
                List<Country> countries = new List<Country>();
                if (isAll)
                {
                    countries = _dbContext.Countries.Include(z => z.CountryMLsList).Where(x => x.IsDeleted == false).ToList();
                }
                else
                {
                    countries = _dbContext.Countries.Include(z => z.CountryMLsList).Where(x => x.IsDeleted == false && x.IsActive == true).ToList();
                }

                Mapper.Map(countries, countryDTOs);
                for (int i = 0; i < countries.Count; i++)
                {

                    CountryML englishML = countries[i].CountryMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, countryDTOs[i].English);
                }
                return countryDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SettingsDTO GetSettings()
        {
            try
            {
                Settings settings = _dbContext.Settings.Include(x => x.SettingsMLsList).FirstOrDefault();
                SettingsDTO appsettingsResponse = Mapper.Map<Settings, SettingsDTO>(settings);
                SettingsML englishML = settings.SettingsMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                SettingsML arabicML = settings.SettingsMLsList.FirstOrDefault(x => x.Culture == CultureType.Arabic);
                Mapper.Map(englishML, appsettingsResponse.English);
                Mapper.Map(arabicML, appsettingsResponse.Arabic);
                return appsettingsResponse;

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<CityDTO> GetAllCities(bool isAll)
        {

            try
            {
                List<CityDTO> cityDTOs = new List<CityDTO>();
                List<City> cities = new List<City>();
                if (isAll)
                {
                    cities = _dbContext.Cities.Include(z => z.CityMLsList)
                    .Include(a => a.Country).ThenInclude(a => a.CountryMLsList)
                    .Where(x => x.IsDeleted == false).ToList();
                }
                else
                {
                    cities = _dbContext.Cities.Include(z => z.CityMLsList)
                    .Include(a => a.Country).ThenInclude(a => a.CountryMLsList)
                    .Where(x => x.IsDeleted == false && x.IsActive == true).ToList();
                }

                Mapper.Map(cities, cityDTOs);
                for (int i = 0; i < cities.Count; i++)
                {
                    CityML englishML = cities[i].CityMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, cityDTOs[i].English);
                    CountryML countryML = cities[i].Country.CountryMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(countryML, cityDTOs[i].Country.English);
                    cityDTOs[i].Arabic = null;
                    cityDTOs[i].Country.Arabic = null;

                }
                return cityDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public List<CityDTO> GetAllCitiesByCountryId(int id, bool isAll)
        {

            try
            {
                List<CityDTO> cityDTOs = new List<CityDTO>();
                List<City> cities = new List<City>();
                if (isAll)
                {
                    cities = _dbContext.Cities.Include(z => z.CityMLsList)
                    .Include(a => a.Country).ThenInclude(a => a.CountryMLsList)
                    .Where(x => x.IsDeleted == false && x.Country_Id == id).ToList();
                }
                else
                {
                    cities = _dbContext.Cities.Include(z => z.CityMLsList)
                .Include(a => a.Country).ThenInclude(a => a.CountryMLsList)
                .Where(x => x.IsDeleted == false && x.Country_Id == id && x.IsActive == true).ToList();
                }

                Mapper.Map(cities, cityDTOs);
                for (int i = 0; i < cities.Count; i++)
                {
                    CityML englishML = cities[i].CityMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, cityDTOs[i].English);
                    CountryML countryML = cities[i].Country.CountryMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(countryML, cityDTOs[i].Country.English);
                    cityDTOs[i].Arabic = null;
                    cityDTOs[i].Country.Arabic = null;

                }
                return cityDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<BranchDTO> GetAllBranchesByBankId(int id)
        {

            try
            {
                List<BranchDTO> branchDTOs = new List<BranchDTO>();
                List<Branch> branches = new List<Branch>();

                branches = _dbContext.Branches.Include(z => z.BranchMLsList).Where(x => x.IsDeleted == false && x.Bank_Id == id).ToList();
                Mapper.Map(branches, branchDTOs);
                for (int i = 0; i < branches.Count; i++)
                {

                    BranchML englishML = branches[i].BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, branchDTOs[i].English);
                }
                return branchDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public bool AddBankCashSubscription(int id, List<string> topUpReceiptUrls, AddBankCashSbuscriptionBindingModel model)
        {
            try
            {
                var package = _dbContext.SubscriptionPackages.FirstOrDefault(x => x.Id == model.SubscriptionPackage_Id);
                CashSubscription cashSubscription = new CashSubscription { Driver_Id = id, RemainingRides = package.NumOfRides, Amount = model.Amount, Account_Id = model.Account_Id, SubscriptionPackage_Id = model.SubscriptionPackage_Id, Status = TopUpStatus.Pending, isActive = true, PaymentType = PaymentMethods.CreditCard };
                var driver = _dbContext.Drivers.Include(x => x.CashSubscriptions).FirstOrDefault(x => x.Id == id);
                if (driver.CashSubscriptions.Count > 0)
                {
                    var pkg = driver.CashSubscriptions.Last();
                    if (pkg.isActive)
                    {
                        cashSubscription.RemainingRides += pkg.RemainingRides;
                        cashSubscription.PreviousPackageRides = pkg.RemainingRides;
                    }
                    pkg.isActive = false;
                }

                foreach (var img in topUpReceiptUrls)
                {
                    CashSubscriptionMedia media = new CashSubscriptionMedia { MediaUrl = img };
                    cashSubscription.Receipts.Add(media);
                }

                #region Expiry
                switch (package.DurationType)
                {
                    case DurationType.Days:
                        cashSubscription.ExpiryDate = DateTime.UtcNow.AddDays(package.Duration);
                        break;
                    case DurationType.Months:
                        cashSubscription.ExpiryDate = DateTime.UtcNow.AddMonths(package.Duration);
                        break;

                    case DurationType.Years:
                        cashSubscription.ExpiryDate = DateTime.UtcNow.AddYears(package.Duration);
                        break;

                    default:
                        cashSubscription.ExpiryDate = DateTime.UtcNow.AddMonths(package.Duration);
                        break;
                };
                #endregion
                driver.CashSubscriptions.Add(cashSubscription);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Driver> GetDriversbyIds(string driverIds, string searchString = "")
        {
            List<Driver> responseList = new List<Driver>();
                List<int> IdsList = driverIds.Split(',').Select(int.Parse).ToList();
                responseList = _dbContext.Drivers.Where(x => IdsList.Contains(x.Id) && x.LoginStatus == LoginStatus.Online).ToList();
                if(!String.IsNullOrEmpty(searchString))
                    responseList = responseList.Where(x => x.Username.ToLower().Contains(searchString.ToLower())).ToList();
            if (responseList.Count == 0)
            {
                return null;
            }
            return responseList;
        }

        public bool LogDriver(int driverId, bool isOnline)
        {
            if(isOnline)
            {
                DriverLog _log = new DriverLog { Driver_Id = driverId, Type = LogType.Login };
                _dbContext.DriverLogs.Add(_log);
                _dbContext.SaveChanges();
            }
            else
            {
                DriverLog driverLog = _dbContext.DriverLogs.FirstOrDefault(x => x.Type == LogType.Login);
                if (driverLog != null)
                    driverLog.Type = LogType.Logout;
            }
            return true;
        }

    }
}

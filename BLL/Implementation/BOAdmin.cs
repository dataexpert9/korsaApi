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
using System.Globalization;
using System.Linq;

namespace BLL.Implementation
{

    public class BOAdmin : IBOAdmin
    {
        public DataContext _dbContext { get; set; }

        public BOAdmin(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Admin WebPanelLogin(string username, string password)
        {
            var admin = _dbContext.Admins.FirstOrDefault(x => x.Email == username && x.Password == password && x.IsDeleted == false);
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public DashboardStats GetAdminDashboardStats()
        {
            try
            {
                DashboardStats returnModel = new DashboardStats();
                returnModel.TotalUsers = _dbContext.Users.Count();
                returnModel.TotalDrivers = _dbContext.Drivers.Count(x => x.Status == DriverAccountStatus.RequestApproved);
                returnModel.TotalRides = _dbContext.Trips.Count();
                returnModel.TotalVehicles = _dbContext.Vehicles.Count();
                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<User> GetAllUsers(string startDate, string endDate, SearchType userSearchBy, string userName, string userId)
        {

            List<User> responseList = _dbContext.Users.ToList();
            if (responseList.Count == 0)
            {
                return null;
            }

            if (!String.IsNullOrEmpty(userName))
            {
                responseList = responseList.Where(x => x.UserName.ToLower().Contains(userName.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(userId))
            {
                responseList = responseList.Where(x => x.UniqueId == userId).ToList();
            }

            if (userSearchBy == SearchType.LowRated)
            {
                responseList = responseList.Where(x => x.Rating < 3).ToList();
            }

            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                DateTime startDateTime;
                DateTime endDateTime;
                startDateTime = DateTime.ParseExact(startDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                endDateTime = DateTime.ParseExact(endDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                responseList = responseList.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();
            }

            return responseList;

        }

        public UserDTO GetUserById(int User_Id)
        {
            try
            {
                UserDTO userDTO = new UserDTO();
                var result = _dbContext.Users
                    .Include(x => x.Referrer)
                    .Include(x => x.BankTopUps)
                    .ThenInclude(x => x.Account)
                    .ThenInclude(x => x.AccountMLsList)
                    .Include(x=>x.City).ThenInclude(x=>x.CityMLsList)
                    .ThenInclude(x=>x.City).ThenInclude(x => x.Country).ThenInclude(x => x.CountryMLsList)
                    .FirstOrDefault(x => x.Id == User_Id);
                Mapper.Map(result, userDTO);
                int i = 0;
                foreach (var topUp in result.BankTopUps)
                {
                    if (topUp.Account_Id!=null || topUp.PaymentType==PaymentMethods.Cash)
                    {
                        AccountML englishML = topUp.Account.AccountMLsList.FirstOrDefault(x=>x.Culture==CultureType.English);
                        Mapper.Map(englishML, userDTO.BankTopUps[i].Account.English);
                    }
                    i++;
                }
                CityML cityML = result.City.CityMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(cityML, userDTO.City.English);
                CountryML countryML = result.City.Country.CountryMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(countryML, userDTO.City.Country.English);
                return userDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public InvitedFriendDTOList GetUserReferralsByUserId(int user_Id)
        {

            var result = _dbContext.InvitedFriends
                .Include(x => x.Referrer)
                .Include(x => x.InvitedUser)
                .Where(x => x.Referrer_Id == user_Id).ToList();
            InvitedFriendDTOList response = new InvitedFriendDTOList();
            Mapper.Map(result, response.InvitedFriends);
            return response;

        }

        public Admin GetAdminById(int Id)
        {
            return _dbContext.Admins.FirstOrDefault(x => x.Id == Id);
        }


        public DriverDTO GetDriverById(int Driver_Id)
        {
            var driver = _dbContext.Drivers
                         .Include(x => x.Medias)
                         .Include(x => x.CashSubscriptions).ThenInclude(x=>x.SubscriptionPackage)
                         .Include(x => x.Vehicles).ThenInclude(y => y.Medias)
                        .Include(a => a.DriverAccounts).ThenInclude(a => a.Branch).ThenInclude(a => a.BranchMLsList)
                        .ThenInclude(a => a.Branch).ThenInclude(a => a.Bank).ThenInclude(a => a.BankMLsList)
                        .FirstOrDefault(x => x.Id == Driver_Id);

            driver.Medias = driver.Medias.Where(x => x.IsDeleted == false).ToList();
            driver.CashSubscriptions = driver.CashSubscriptions.Where(x =>/* x.isActive &&*/ x.Status == TopUpStatus.Accepted).OrderBy(x => x.CreatedDate).ToList();
            foreach (var item in driver.Vehicles)
            {
                item.Medias = item.Medias.Where(x => x.IsDeleted == false).ToList();
            }


            DriverDTO response = new DriverDTO();
            Mapper.Map(driver, response);

            int i = 0;
            foreach (var item in driver.DriverAccounts)
            {
                var branchML = item.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(branchML, response.DriverAccounts[i].Branch.English);
                var bankML = item.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(bankML, response.DriverAccounts[i].Branch.Bank.English);
                i++;
            }
            foreach (var item in response.CashSubscriptions)
            {
                foreach (var pkg in item.Driver.CashSubscriptions)
                {
                    pkg.Driver = null;
                }
            }
            return response;
        }


        public List<Driver> GetAllDrivers(bool isBecomeDriverRequests, string startDate, string endDate, SearchType userSearchBy, string userName, string userId)
        {
            List<Driver> responseList = _dbContext.Drivers.Include(x => x.Vehicles).OrderBy(x => x.Status).ToList();
            if (responseList.Count == 0)
            {
                return null;
            }

            if (isBecomeDriverRequests)
            {
                responseList = responseList.Where(x => x.Status == DriverAccountStatus.RequestPending).ToList();
                return responseList;
            }
            else
            {
                responseList = responseList.Where(x => x.Status == DriverAccountStatus.RequestApproved).ToList();

                if (responseList.Count == 0)
                {
                    return null;
                }

                if (!String.IsNullOrEmpty(userName))
                {
                    responseList = responseList.Where(x => x.Username.ToLower().Contains(userName.ToLower())).ToList();
                }

                if (!String.IsNullOrEmpty(userId))
                {
                    responseList = responseList.Where(x => x.UniqueId == userId).ToList();
                }

                if (userSearchBy == SearchType.LowRated)
                {
                    responseList = responseList.Where(x => x.Rating < 3).ToList();
                }

                if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                {
                    DateTime startDateTime;
                    DateTime endDateTime;
                    startDateTime = DateTime.ParseExact(startDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    endDateTime = DateTime.ParseExact(endDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    responseList = responseList.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();

                }

                return responseList;
            }
        }


        public Settings SetSettings(SettingsDTO settings)
        {
            try
            {
                Settings obj = _dbContext.Settings.Include(x => x.SettingsMLsList).FirstOrDefault();
                obj.InvitationBonus = settings.InvitationBonus;
                SettingsML english = obj.SettingsMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                SettingsML arabic = obj.SettingsMLsList.FirstOrDefault(x => x.Culture == CultureType.Arabic);
                arabic.AboutUs = settings.Arabic.AboutUs;
                arabic.TermsOfUse = settings.Arabic.TermsOfUse;
                arabic.PrivacyPolicy = settings.Arabic.PrivacyPolicy;
                arabic.Currency = settings.Arabic.Currency;
                english.AboutUs = settings.English.AboutUs;
                english.TermsOfUse = settings.English.TermsOfUse;
                english.PrivacyPolicy = settings.English.PrivacyPolicy;
                english.Currency = settings.English.Currency;

                _dbContext.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public bool ChangeUserStatuses(ChangeUserStatusListModel model)
        {
            foreach (var user in model.Users)
            {
                _dbContext.Users.FirstOrDefault(x => x.Id == user.UserId).IsDeleted = user.Status; if (user.Status)
                {
                    Notification notf = new Notification { Title = "Korsa", Text = "Your account has been blocked by our administration team.", Type = (int)PushNotfType.UserBlocked, User_Id = user.UserId };
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id == user.UserId).ToList();
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendIOSPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }
                else
                {
                    Notification notf = new Notification { Title = "Korsa", Text = "Your account has been activated by our administration team.", Type = (int)PushNotfType.UserBlocked, User_Id = user.UserId };
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id == user.UserId).ToList();
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendIOSPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }
            }
            _dbContext.SaveChanges();
            return true;

        }
        public bool ChangeDriverStatuses(ChangeUserStatusListModel model)
        {

            try
            {
                foreach (var user in model.Users)
                {
                    _dbContext.Drivers.FirstOrDefault(x => x.Id == user.UserId).IsDeleted = user.Status;
                    if (user.Status)
                    {
                        Notification notf = new Notification { Title = "Korsa", Text = "Your account has been blocked by our administration team.", Type = (int)PushNotfType.DriverBlocked, Driver_Id = user.UserId };
                        var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.Driver_Id == user.UserId).ToList();
                        PushNotificationsHelper.SendAndroidPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList(), userType: UserTypes.Driver);
                        PushNotificationsHelper.SendIOSPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList(), userType: UserTypes.Driver);
                        _dbContext.Notifications.Add(notf);

                    }
                    else
                    {
                        Notification notf = new Notification { Title = "Korsa", Text = "Your account has been activated by our administration team.", Type = (int)PushNotfType.DriverBlocked, Driver_Id = user.UserId };
                        var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.Driver_Id == user.UserId).ToList();
                        PushNotificationsHelper.SendAndroidPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList(), userType: UserTypes.Driver);
                        PushNotificationsHelper.SendIOSPushNotifications(Message: notf.Text, Title: notf.Title, DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList(), userType: UserTypes.Driver);
                        _dbContext.Notifications.Add(notf);
                    }

                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SaveDriverRequestStatuses(ChangeUserStatusListModel model)
        {
            try
            {
                foreach (var user in model.Users)
                {
                    if (user.Status)
                    {
                        _dbContext.Drivers.FirstOrDefault(x => x.Id == user.UserId).Status = DriverAccountStatus.RequestApproved;
                    }
                    else
                    {
                        _dbContext.Drivers.FirstOrDefault(x => x.Id == user.UserId).Status = DriverAccountStatus.RequestRejected;
                    }
                }
                //Notification won't be sent as driver has no Userdevice yet
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<Trip> GetAllRides(string startDate, string endDate, RideSearchType filterType, int rideId)
        {


            List<Trip> responseList = _dbContext.Trips.Include(x => x.Driver).Include(x => x.PrimaryUser).ToList();
            if (responseList.Count == 0)
            {
                return null;
            }

            if (rideId > 0)
            {
                responseList = responseList.Where(x => x.Id == rideId).ToList();
            }

            if (filterType == RideSearchType.CancelledRides)
            {
                responseList = responseList.Where(x => x.Status == TripStatus.CancelledByDriver || x.Status == TripStatus.CancelledByUser).ToList();
            }

            else if (filterType == RideSearchType.CompletedRides)
            {
                responseList = responseList.Where(x => x.Status == TripStatus.Completed || x.Status == TripStatus.Closed).ToList();
            }

            else if (filterType == RideSearchType.AcceptedRides)
            {
                responseList = responseList.Where(x => x.Status == TripStatus.Started || x.Status == TripStatus.AssignedToDriver).ToList();
            }

            else if (filterType == RideSearchType.ScheduledRides)
            {
                responseList = responseList.Where(x => x.isScheduled == true).ToList();
            }
            else if (filterType == RideSearchType.RideAcceptanceReport)
            {
                responseList = responseList.Where(x => x.Status == TripStatus.AssignedToDriver || x.Status == TripStatus.Started || x.Status == TripStatus.Completed || x.Status == TripStatus.Closed).ToList();
            }

            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                DateTime startDateTime;
                DateTime endDateTime;
                startDateTime = DateTime.ParseExact(startDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                endDateTime = DateTime.ParseExact(endDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                responseList = responseList.Where(x => x.CreatedDate >= startDateTime && x.CreatedDate <= endDateTime).ToList();

            }

            return responseList;
        }

        public int ChangePassword(int Id, string OldPassword, string NewPassword)
        {
            try
            {
                if (_dbContext.Admins.Any(x => x.Id == Id && x.Password != OldPassword))
                {
                    return 0;
                }
                else if (_dbContext.Admins.Any(x => x.Id == Id && x.Password == NewPassword))
                {
                    return 2;
                }
                else
                {
                    var admin = _dbContext.Admins.FirstOrDefault(x => x.Id == Id);
                    admin.Password = NewPassword;
                    _dbContext.SaveChanges();
                    return 1;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public Admin AddUpdateAdmin(AdminBindingModel model)
        {
            try
            {
                Admin admin = new Admin();
                if (model.Id > 0)
                {
                    admin = _dbContext.Admins.FirstOrDefault(x => x.Id == model.Id && x.IsDeleted == false);
                    admin.FirstName = model.FirstName;
                    admin.LastName = model.LastName;
                    admin.Phone = model.Phone;
                    admin.Role = model.Role;
                    admin.Password = (String.IsNullOrEmpty(model.Password)) ? admin.Password : CryptoHelper.Hash(model.Password);
                    if (!String.IsNullOrEmpty(model.ImageUrl))
                    {
                        admin.ImageUrl = model.ImageUrl;
                    }

                    _dbContext.SaveChanges();
                }
                else
                {
                    Mapper.Map(model, admin);
                    admin.Password = CryptoHelper.Hash(model.Password);
                    _dbContext.Admins.Add(admin);
                    _dbContext.SaveChanges();
                }
                return admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TripDTO> GetRidesById(int id, UserTypes userType)
        {
            try
            {
                List<Trip> trips = (userType == UserTypes.User)
                    ?
                    _dbContext.UserTrips
                    .Where(x => x.User_Id == id).Select(c => c.Trip)
                    .Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList).Include(x => x.Driver).Include(x => x.PrimaryUser).ToList()
                    :
                    _dbContext.Trips.Include(x => x.PrimaryUser).Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList)
                    .Where(x => x.Driver_Id.Value == id).ToList()
                    ;
                List<TripDTO> tripsResponse = Mapper.Map<List<Trip>, List<TripDTO>>(trips);
                for (int i = 0; i < trips.Count; i++)
                {
                    tripsResponse[i].RideTypeName = trips[i].RideType.RideTypeMLsList.FirstOrDefault().Name;
                }
                return tripsResponse;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }



        public List<DriverPaymentDTO> GetDriverPaymentHistory(int driverId)
        {
            try
            {
                List<DriverPayment> trips = _dbContext.DriverPayments.Where(x => x.Driver_Id == driverId).ToList();
                List<DriverPaymentDTO> response = new List<DriverPaymentDTO>();
                Mapper.Map(trips, response);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupportConversationDTO> GetSupportConversationsList(string search)
        {
            try
            {
                List<SupportConversation> conversations = (String.IsNullOrEmpty(search)) ? _dbContext.SupportConversations.OrderByDescending(x => x.LastConversationDate).ToList() : _dbContext.SupportConversations.Where(x => x.UserName.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.LastConversationDate).ToList();
                List<SupportConversationDTO> response = new List<SupportConversationDTO>();
                Mapper.Map(conversations, response);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DriverPaymentDTO GetPaymentDetailsById(int id)
        {
            try
            {
                DriverPayment payment = _dbContext.DriverPayments.Include(a => a.Driver)
                    .ThenInclude(a => a.DriverAccounts).ThenInclude(a => a.Branch).ThenInclude(a => a.BranchMLsList)
                    .ThenInclude(a => a.Branch).ThenInclude(a => a.Bank).ThenInclude(a => a.BankMLsList)
                    .FirstOrDefault(x => x.Id == id);


                foreach (var item in payment.Driver.DriverAccounts)
                {
                    item.Driver = null;
                }

                DriverPaymentDTO response = new DriverPaymentDTO();
                Mapper.Map(payment, response);
                foreach (var item in payment.Driver.DriverAccounts)
                {
                    int i = 0;
                    var branchML = item.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(branchML, response.Driver.DriverAccounts[i].Branch.English);
                    var bankML = item.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(bankML, response.Driver.DriverAccounts[i].Branch.Bank.English);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddEditSubscriptionPackage(SubscriptionBindingModel model)
        {
            try
            {
                SubscriptionPackage package = (model.Id > 0) ? _dbContext.SubscriptionPackages.FirstOrDefault(x => x.Id == model.Id) : new SubscriptionPackage();
                if (package == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, package);
                if (_dbContext.SubscriptionPackages.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.Id != model.Id && x.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    _dbContext.SubscriptionPackages.Update(package);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    _dbContext.SubscriptionPackages.Add(package);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddUpdateFareCalculation(FareCalculationBindingModel model)
        {
            try
            {
                FareCalculation fare = (model.Id > 0) ? _dbContext.FareCalculations.FirstOrDefault(x => x.Id == model.Id) : new FareCalculation();
                if (fare == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                if (_dbContext.FareCalculations.Any(x => x.PaymentMethod == model.PaymentMethod && x.Id != model.Id && x.IsDeleted == false && x.City_Id == model.City_Id && ((x.StartTime <= model.EndTime && x.StartTime >= model.StartTime) || (x.EndTime >= model.StartTime && x.EndTime <= model.StartTime) || (x.StartTime >= model.StartTime && x.EndTime <= model.EndTime)) && x.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }
                Mapper.Map(model, fare);

                if (model.Id > 0)
                {
                    _dbContext.FareCalculations.Update(fare);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    _dbContext.FareCalculations.Add(fare);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddEditRideType(RideTypeBindingModel model, string defImagePath, string selectedImagePath)
        {
            try
            {
                RideType rideType = (model.Id > 0) ? _dbContext.RideTypes.Include(x => x.RideTypeMLsList).FirstOrDefault(x => x.Id == model.Id) : new RideType();
                if (rideType == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, rideType);
                if (_dbContext.RideTypeMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.RideType.Id != model.Id && x.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (!String.IsNullOrEmpty(defImagePath))
                {
                    rideType.DefaultImageUrl = defImagePath;
                }

                if (!String.IsNullOrEmpty(selectedImagePath))
                {
                    rideType.SelectedImageUrl = selectedImagePath;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, rideType.RideTypeMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.RideTypes.Update(rideType);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        rideType.RideTypeMLsList.Add(new RideTypeML { AboutRideType = model.AboutRideType, Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.RideTypes.Add(rideType);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddEditBank(BankBindingModel model)
        {
            try
            {
                Bank bank = (model.Id > 0) ? _dbContext.Banks.Include(x => x.BankMLsList).FirstOrDefault(x => x.Id == model.Id) : new Bank();
                if (bank == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, bank);
                if (_dbContext.BankMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.Bank_Id != model.Id && x.Bank.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, bank.BankMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.Banks.Update(bank);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        bank.BankMLsList.Add(new BankML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.Banks.Add(bank);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AddUpdateResponse AddEditCarCompany(CarCompanyBindingModel model)
        {
            try
            {
                CarCompany carCompany = (model.Id > 0) ? _dbContext.CarCompanies.Include(x => x.CarCompanyMLsList).FirstOrDefault(x => x.Id == model.Id) : new CarCompany();
                if (carCompany == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, carCompany);
                if (_dbContext.CarCompanyMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.CarCompany_Id != model.Id && x.CarCompany.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, carCompany.CarCompanyMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.CarCompanies.Update(carCompany);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        carCompany.CarCompanyMLsList.Add(new CarCompanyML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.CarCompanies.Add(carCompany);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public AddUpdateResponse AddEditCarModels(CarModelBindingModel model)
        {
            try
            {
                CarModel carModel = (model.Id > 0) ? _dbContext.CarModels.Include(x => x.CarModelMLsList).FirstOrDefault(x => x.Id == model.Id) : new CarModel();
                if (carModel == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, carModel);
                if (_dbContext.CarModelMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.CarModel_Id != model.Id && x.CarModel.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, carModel.CarModelMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.CarModels.Update(carModel);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        carModel.CarModelMLsList.Add(new CarModelML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.CarModels.Add(carModel);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public AddUpdateResponse AddEditCarYear(CarYearBindingModel model)
        {
            try
            {
                CarYear carYear = (model.Id > 0) ? _dbContext.CarYear.Include(x => x.CarYearMLsList).FirstOrDefault(x => x.Id == model.Id) : new CarYear();
                if (carYear == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, carYear);
                if (_dbContext.CarYearMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.CarYear_Id != model.Id && x.CarYear.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, carYear.CarYearMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.CarYear.Update(carYear);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        carYear.CarYearMLsList.Add(new CarYearML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.CarYear.Add(carYear);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AddUpdateResponse AddEditCarCapacity(CarCapacityBindingModel model)
        {
            try
            {
                CarCapacity carCapacity = (model.Id > 0) ? _dbContext.CarCapacity.Include(x => x.CarCapacityMLsList).FirstOrDefault(x => x.Id == model.Id) : new CarCapacity();
                if (carCapacity == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, carCapacity);
                if (_dbContext.CarCapacityMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.CarCapacity_Id != model.Id && x.CarCapacity.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, carCapacity.CarCapacityMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.CarCapacity.Update(carCapacity);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        carCapacity.CarCapacityMLsList.Add(new CarCapacityML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.CarCapacity.Add(carCapacity);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public AddUpdateResponse AddEditCarType(CarTypeBindingModel model)
        {
            try
            {
                CarType carType = (model.Id > 0) ? _dbContext.CarTypes.Include(x => x.CarTypeMLsList).FirstOrDefault(x => x.Id == model.Id) : new CarType();
                if (carType == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, carType);
                if (_dbContext.CarTypeMLs.Any(x => x.Name.Trim().ToLowerInvariant().Equals(model.Name.Trim().ToLowerInvariant()) && x.CarType_Id != model.Id && x.CarType.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, carType.CarTypeMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.CarTypes.Update(carType);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        carType.CarTypeMLsList.Add(new CarTypeML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.CarTypes.Add(carType);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public AddUpdateResponse AddUpdateCountry(CountryBindingModel model)
        {
            try
            {
                Country country = (model.Id > 0) ? _dbContext.Countries.Include(x => x.CountryMLsList).FirstOrDefault(x => x.Id == model.Id) : new Country();
                if (country == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, country);
                if (_dbContext.CountryMLs.Any(x => x.Name.ToLowerInvariant().Trim().Equals(model.Name.ToLowerInvariant().Trim()) && x.Country.Id != model.Id && x.Country.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }


                if (model.Id > 0)
                {
                    if (country.IsActive == false)
                    {
                        var cities = _dbContext.Cities.Where(x => x.Country_Id == model.Id).ToList();
                        foreach (var item in cities)
                        {
                            item.IsActive = false;
                        }
                    }


                    Mapper.Map(model, country.CountryMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.Countries.Update(country);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        country.CountryMLsList.Add(new CountryML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.Countries.Add(country);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AddUpdateResponse AddUpdateRole(RoleScreenModel model)
        {
            try
            {
                Role Role = new Role();
                Role UpdatedRole = new Role();
                Role.Name = model.RoleName;
                if (model.Id != 0)
                {
                    RoleScreen RoleScreen = new RoleScreen();
                    List<RoleScreen> RoleScreenResult = new List<RoleScreen>();
                    UpdatedRole = _dbContext.Roles.Include(x => x.RoleScreen).FirstOrDefault(x => x.Id == model.Id);
                    UpdatedRole.Name = model.RoleName;
                    _dbContext.RoleScreens.RemoveRange(UpdatedRole.RoleScreen);

                    foreach (var role in model.Roles)
                    {
                        RoleScreenResult.Add(new RoleScreen
                        {
                            AllowScreen = role.AllowScreen,
                            Fullaccess = role.Fullaccess,
                            Role_Id = UpdatedRole.Id,
                            Screen_Id = role.ScreenId
                        });
                    }

                    _dbContext.RoleScreens.AddRange(RoleScreenResult);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    _dbContext.Roles.Add(Role);
                    RoleScreen RoleScreen = new RoleScreen();
                    foreach (var role in model.Roles)
                    {
                        RoleScreen = new RoleScreen
                        {
                            Role_Id = Role.Id,
                            Screen_Id = role.ScreenId,
                            AllowScreen = role.AllowScreen,
                            Fullaccess = role.Fullaccess
                        };
                        _dbContext.RoleScreens.Add(RoleScreen);
                    }
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddEditBankBranch(BankBranchBindingModel model)
        {
            try
            {
                Branch branch = (model.Id > 0) ? _dbContext.Branches.Include(x => x.BranchMLsList).FirstOrDefault(x => x.Id == model.Id) : new Branch();
                if (branch == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, branch);
                if (_dbContext.BranchMLs.Any(x => (x.Name.Trim().ToLowerInvariant().Equals(model.Name.Trim().ToLowerInvariant().Trim())) && x.Branch_Id != model.Id && x.Branch.Bank_Id == model.Bank_Id && x.Branch.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, branch.BranchMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.Branches.Update(branch);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        branch.BranchMLsList.Add(new BranchML { Name = model.Name, Code = model.Code, Address = model.Address, Culture = model.Culture });
                    }

                    _dbContext.Branches.Add(branch);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddEditAccount(AccountBindingModel model)
        {
            try
            {
                Account account = (model.Id > 0) ? _dbContext.Accounts.Include(x => x.AccountMLsList).FirstOrDefault(x => x.Id == model.Id) : new Account();
                if (account == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, account);
                if (_dbContext.AccountMLs.Any(x => (x.Name.Trim().ToLowerInvariant().Equals(model.Name.ToLowerInvariant().Trim()) || x.IBN.ToLowerInvariant().Trim().Contains(model.IBN.ToLowerInvariant().Trim())) && x.Account.Id != model.Id && x.Account.Branch_Id == model.Branch_Id && x.Account.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, account.AccountMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.Accounts.Update(account);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        account.AccountMLsList.Add(new AccountML { Name = model.Name, Code = model.Code, IBN = model.IBN, Culture = model.Culture });
                    }

                    _dbContext.Accounts.Add(account);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddUpdateResponse AddUpdateCity(CityBindingModel model)
        {
            try
            {
                City city = (model.Id > 0) ? _dbContext.Cities.Include(x => x.CityMLsList).FirstOrDefault(x => x.Id == model.Id) : new City();
                if (city == null)
                {
                    return AddUpdateResponse.NotFound;
                }

                Mapper.Map(model, city);
                if (_dbContext.CityMLs.Any(x => x.Name.Trim().ToLowerInvariant().Equals(model.Name.ToLowerInvariant().Trim()) && x.City_Id != model.Id && x.City.Country_Id == model.Country_Id && x.City.IsDeleted == false))
                {
                    return AddUpdateResponse.AlreadyExist;
                }

                if (model.Id > 0)
                {
                    Mapper.Map(model, city.CityMLsList.FirstOrDefault(x => x.Culture == model.Culture));
                    _dbContext.Cities.Update(city);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Updated;
                }
                else
                {
                    if (model.Culture == CultureType.English)
                    {
                        city.CityMLsList.Add(new CityML { Name = model.Name, Culture = model.Culture });
                    }

                    _dbContext.Cities.Add(city);
                    _dbContext.SaveChanges();
                    return AddUpdateResponse.Added;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSubscriptionPackage(int id)
        {
            try
            {
                SubscriptionPackage pkg = _dbContext.SubscriptionPackages.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (pkg != null)
                {
                    pkg.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }


        public bool DeleteRideType(int id)
        {
            try
            {
                RideType type = _dbContext.RideTypes.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (type != null)
                {
                    type.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }


        public bool DeleteBank(int id)
        {
            try
            {

                List<Account> accounts = _dbContext.Accounts.Include(x => x.Branch).ThenInclude(x => x.Accounts)
               .Include(x => x.Branch).ThenInclude(x => x.Bank)
               .Where(x => x.IsDeleted == false && x.Branch.Bank_Id == id).ToList();

                if (accounts.Count == 0)
                {
                    Bank bank = _dbContext.Banks.Include(x => x.Branches)
                    .FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                    if (bank == null)
                    {
                        return false;
                    }

                    bank.IsDeleted = true;
                    foreach (var branch in bank.Branches)
                    {
                        branch.IsDeleted = true;
                    }

                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    foreach (var account in accounts)
                    {
                        account.IsDeleted = true;
                        account.Branch.IsDeleted = true;
                        account.Branch.Bank.IsDeleted = true;
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteCountry(int id)
        {
            try
            {
                Country country = _dbContext.Countries.Include(x => x.CountryMLsList).FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (country == null)
                {
                    return false;
                }

                country.IsDeleted = true;
                foreach (var branch in country.Cities)
                {
                    branch.IsDeleted = true;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }


        public bool DeleteCity(int id)
        {
            try
            {
                City city = _dbContext.Cities.Include(x => x.CityMLsList).FirstOrDefault(x => x.IsDeleted == false && x.Id == id);

                if (city != null)
                {
                    city.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public bool DeleteAdmin(int id)
        {
            try
            {
                Admin admin = _dbContext.Admins.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (admin != null)
                {
                    admin.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public List<AdminDTO> GetAdmins()
        {
            try
            {
                List<AdminDTO> response = new List<AdminDTO>();
                List<Admin> admin = _dbContext.Admins.Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(admin, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CarCompanyDTO GetCarCompanyById(int id, CultureType culture)
        {
            try
            {
                CarCompanyDTO response = new CarCompanyDTO();
                CarCompany carCompany = _dbContext.CarCompanies.Include(X => X.CarCompanyMLsList).FirstOrDefault(x => x.Id == id);
                CarCompanyML ml = carCompany.CarCompanyMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(carCompany, response);
                Mapper.Map(ml, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RoleDTO GetRoleById(int value, CultureType culture)
        {
            try
            {
                RoleDTO response = new RoleDTO();
                Role role = _dbContext.Roles.Include(X => X.RoleScreen).FirstOrDefault(x => x.Id == value);
                Mapper.Map(role, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public FareCalculationDTO GetFareCalculationDetailsById(int id, CultureType culture)
        {
            try
            {
                FareCalculationDTO response = new FareCalculationDTO();
                FareCalculation fareCalculation = _dbContext.FareCalculations.Include(X => X.City).ThenInclude(x => x.CityMLsList).FirstOrDefault(x => x.Id == id);
                Mapper.Map(fareCalculation, response);
                CityML englishML = fareCalculation.City.CityMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(englishML, response.City.English);
                response.City.Arabic = null;
                response.City.Country = null;
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public CarModelDTO GetCarModelById(int id, CultureType culture)
        {
            try
            {
                CarModelDTO response = new CarModelDTO();
                CarModel carModel = _dbContext.CarModels.Include(X => X.CarModelMLsList).FirstOrDefault(x => x.Id == id);
                CarModelML ml = carModel.CarModelMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(carModel, response);
                Mapper.Map(ml, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CarTypeDTO GetCarTypeById(int id, CultureType culture)
        {
            try
            {
                CarTypeDTO response = new CarTypeDTO();
                CarType bank = _dbContext.CarTypes.Include(X => X.CarTypeMLsList).FirstOrDefault(x => x.Id == id);
                CarTypeML ml = bank.CarTypeMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(bank, response);
                Mapper.Map(ml, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CarYearDTO GetCarYearById(int id, CultureType culture)
        {
            try
            {
                CarYearDTO response = new CarYearDTO();
                CarYear carYear = _dbContext.CarYear.Include(X => X.CarYearMLsList).FirstOrDefault(x => x.Id == id);
                CarYearML ml = carYear.CarYearMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(carYear, response);
                Mapper.Map(ml, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CarCapacityDTO GetCarCapacityById(int id, CultureType culture)
        {
            try
            {
                CarCapacityDTO response = new CarCapacityDTO();
                CarCapacity carCapacity = _dbContext.CarCapacity.Include(X => X.CarCapacityMLsList).FirstOrDefault(x => x.Id == id);
                CarCapacityML ml = carCapacity.CarCapacityMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(carCapacity, response);
                Mapper.Map(ml, response);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBranch(int id)
        {
            try
            {
                Branch branch = _dbContext.Branches.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (branch != null)
                {
                    branch.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }


        public bool DeleteCarCompany(int id)
        {
            try
            {
                CarCompany company = _dbContext.CarCompanies.Where(x => x.IsDeleted == false && x.Id == id).FirstOrDefault();
                if (company != null)
                {
                    company.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCarYear(int id)
        {
            try
            {
                CarYear carYear = _dbContext.CarYear.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (carYear != null)
                {
                    carYear.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCarCapacity(int id)
        {
            try
            {
                CarCapacity carCapacity = _dbContext.CarCapacity.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (carCapacity != null)
                {
                    carCapacity.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteCarType(int id)
        {
            try
            {
                CarType carType = _dbContext.CarTypes.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (carType != null)
                {
                    carType.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCarModel(int id)
        {
            try
            {
                CarModel carModel = _dbContext.CarModels.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (carModel != null)
                {
                    carModel.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool DeleteEntity(KorsaEntityTypes type, int id)
        {
            try
            {
                switch (type)
                {
                    case KorsaEntityTypes.Role:
                        Role role = _dbContext.Roles.Include(x => x.Admins).FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                        if (role != null)
                        {
                            role.IsDeleted = true;
                            foreach (var admin in role.Admins)
                            {
                                admin.IsDeleted = true;
                            }
                        }
                        else
                        {
                            return false;
                        }

                        _dbContext.SaveChanges();
                        return true;

                    case KorsaEntityTypes.FareCalculation:
                        FareCalculation calculation = _dbContext.FareCalculations.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                        if (calculation != null)
                        {
                            calculation.IsDeleted = true;
                        }

                        _dbContext.SaveChanges();
                        return true;

                    default:
                        return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePromocode(int id)
        {
            try
            {
                Promocode promocode = _dbContext.Promocodes.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (promocode != null)
                {
                    promocode.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteAccount(int id)
        {
            try
            {
                Account account = _dbContext.Accounts.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (account != null)
                {
                    account.IsDeleted = true;
                }
                else
                {
                    return false;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public List<SubscriptionPackage> GetAllPackages()
        {
            return _dbContext.SubscriptionPackages.ToList();
        }


        public PromocodeDTOList GetCodes()
        {
            PromocodeDTOList promocodeDTOList = new PromocodeDTOList();
            var contents = _dbContext.Promocodes.Where(x => x.IsDeleted == false).ToList();
            if (contents == null)
            {
                return null;
            }

            Mapper.Map(contents, promocodeDTOList.Codes);
            return promocodeDTOList;
        }


        public SubscriptionPackage GetSubscriptionPackageById(int? id)
        {
            return _dbContext.SubscriptionPackages.FirstOrDefault(x => x.Id == id.Value);
        }

        public RideTypeDTO GetRideTypeById(int? id, CultureType culture = CultureType.English)
        {

            try
            {
                RideTypeDTO response = new RideTypeDTO();
                RideType type = _dbContext.RideTypes.Include(X => X.RideTypeMLsList).FirstOrDefault(x => x.Id == id.Value);
                RideTypeML ml = type.RideTypeMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(type, response);
                response.AboutRideType = ml.AboutRideType;
                response.Name = ml.Name;
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public BankDTO GetBankById(int? id, CultureType culture)
        {

            try
            {
                BankDTO response = new BankDTO();
                Bank bank = _dbContext.Banks.Include(X => X.BankMLsList).FirstOrDefault(x => x.Id == id.Value);
                BankML ml = bank.BankMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(bank, response);
                Mapper.Map(ml, response.English);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }



        public CountryDTO GetCountryById(int? id, CultureType culture)
        {

            try
            {
                CountryDTO response = new CountryDTO();
                Country country = _dbContext.Countries.Include(X => X.CountryMLsList).FirstOrDefault(x => x.Id == id.Value);
                CountryML ml = country.CountryMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(country, response);
                Mapper.Map(ml, response.English);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public BranchDTO GetBranchById(int? id, CultureType culture)
        {

            try
            {
                BranchDTO response = new BranchDTO();
                Branch branch = _dbContext.Branches.Include(X => X.BranchMLsList).FirstOrDefault(x => x.Id == id.Value);
                BranchML ml = branch.BranchMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(branch, response);
                Mapper.Map(ml, response.English);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public AccountDTO GetAccountById(int? id, CultureType culture)
        {

            try
            {
                AccountDTO response = new AccountDTO();
                Account account = _dbContext.Accounts.Include(X => X.AccountMLsList)
                     .Include(b => b.Branch).ThenInclude(x => x.BranchMLsList)
                    .Include(b => b.Branch).ThenInclude(x => x.Bank).ThenInclude(x => x.BankMLsList)
                    .FirstOrDefault(x => x.Id == id.Value);
                AccountML ml = account.AccountMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(account, response);
                Mapper.Map(ml, response.English);

                BranchML _branchML = account.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(_branchML, response.Branch.English);
                BankML _bankMl = account.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                Mapper.Map(_bankMl, response.Branch.Bank.English);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<CashSubscriptionDTO> GetSubscriptionPackagesRequests()
        {
            try
            {
                List<CashSubscriptionDTO> responseDTO = new List<CashSubscriptionDTO>();
                List<CashSubscription> subscriptions = new List<CashSubscription>();
                subscriptions = _dbContext.CashSubscriptions
                    .Include(a => a.Driver)
                    .Include(a => a.SubscriptionPackage)
                    .Include(a => a.Receipts)
                    .Include(b => b.Account).ThenInclude(b => b.AccountMLsList)
                    .Include(b => b.Account).ThenInclude(b => b.Branch).ThenInclude(x => x.BranchMLsList)
                    .Include(b => b.Account).ThenInclude(b => b.Branch).ThenInclude(x => x.Bank).ThenInclude(x => x.BankMLsList)
                    .Where(x => x.IsDeleted == false && x.Status == TopUpStatus.Pending).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(subscriptions, responseDTO);
                for (int i = 0; i < subscriptions.Count; i++)
                {
                    AccountML ml = subscriptions[i].Account.AccountMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(ml, responseDTO[i].Account.English);

                    BranchML _branchML = subscriptions[i].Account.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(_branchML, responseDTO[i].Account.Branch.English);
                    BankML _bankMl = subscriptions[i].Account.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(_bankMl, responseDTO[i].Account.Branch.Bank.English);


                }
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public CityDTO GetCityById(int? id, CultureType culture)
        {

            try
            {
                CityDTO response = new CityDTO();
                City city = _dbContext.Cities.Include(X => X.CityMLsList).FirstOrDefault(x => x.Id == id.Value);
                CityML ml = city.CityMLsList.FirstOrDefault(x => x.Culture == culture);
                Mapper.Map(city, response);
                Mapper.Map(ml, response.English);
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TopUpDTO> GetTopUpRequests()
        {
            try
            {
                List<TopUpDTO> topUpDTOs = new List<TopUpDTO>();
                List<TopUp> topUps = new List<TopUp>();
                topUps = _dbContext.BankTopUps.
                    Include(a => a.User)
                    .Include(a => a.Receipts)
                    .Include(b => b.Account).ThenInclude(b => b.AccountMLsList)
                    .Include(b => b.Account).ThenInclude(b => b.Branch).ThenInclude(x => x.BranchMLsList)
                    .Include(b => b.Account).ThenInclude(b => b.Branch).ThenInclude(x => x.Bank).ThenInclude(x => x.BankMLsList)
                    .Where(x => x.IsDeleted == false && x.Status == TopUpStatus.Pending).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(topUps, topUpDTOs);
                for (int i = 0; i < topUps.Count; i++)
                {
                    AccountML ml = topUps[i].Account.AccountMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(ml, topUpDTOs[i].Account.English);

                    BranchML _branchML = topUps[i].Account.Branch.BranchMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(_branchML, topUpDTOs[i].Account.Branch.English);
                    BankML _bankMl = topUps[i].Account.Branch.Bank.BankMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(_bankMl, topUpDTOs[i].Account.Branch.Bank.English);

                    topUpDTOs[i].User.AppSettings = null;

                }
                return topUpDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SearchRoleListDTO SearchRoles()
        {
            try
            {
                List<Role> existingRole = new List<Role>();
                SearchRoleListDTO response = new SearchRoleListDTO();
                existingRole = _dbContext.Roles.Include(x => x.RoleScreen).Where(x => x.IsDeleted == false).ToList();
                Mapper.Map(existingRole, response.Roles);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool AcceptTopUpRequest(TopUpRequestBindingModel model, int userId)
        {
            try
            {
                TopUp request = _dbContext.BankTopUps.FirstOrDefault(x => x.Id == model.Id);
                if (model.Status == TopUpStatus.Rejected)
                {
                    request.Status = TopUpStatus.Rejected;
                    Notification notf = new Notification();
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id == userId).ToList();
                    PushNotificationsHelper.SendIOSPushNotifications(Message: "Your topup request has been rejected.Kindly contact support team for details of request rejection.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: "Your topup request has been rejected.Kindly contact support team for details of request rejection.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }
                else if (model.Status == TopUpStatus.Accepted)
                {
                    request.Amount = model.Amount;
                    User user = _dbContext.Users.FirstOrDefault(x => x.Id == request.User_Id);
                    user.Wallet += model.Amount;
                    request.Status = TopUpStatus.Accepted;
                    Notification notf = new Notification();
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id == userId).ToList();
                    PushNotificationsHelper.SendIOSPushNotifications(Message: "Your topup request has been accepted and amount is added to your wallet.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: "Your topup request has been accepted and amount is added to your wallet.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AcceptCashSubscriptionRequest(CashSubscriptionRequestBindingModel model, int driverId)
        {
            try
            {
                CashSubscription cashSubscription = _dbContext.CashSubscriptions.FirstOrDefault(x => x.Id == model.Id);
                if (model.Status == TopUpStatus.Rejected)
                {
                    cashSubscription.Status = TopUpStatus.Rejected;
                    Notification notf = new Notification();
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.Driver_Id == driverId).ToList();
                    PushNotificationsHelper.SendIOSPushNotifications(Message: "Your subscription request has been rejected.Kindly contact support team for details of request rejection.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: "Your subscription request has been rejected.Kindly contact support team for details of request rejection.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }
                else if (model.Status == TopUpStatus.Accepted)
                {
                    cashSubscription.Amount = model.Amount;
                    Driver driver = _dbContext.Drivers.Include(x => x.CashSubscriptions).FirstOrDefault(x => x.Id == cashSubscription.Driver_Id);

                    if (driver.CashSubscriptions.Count > 0)
                    {
                        var pkg = driver.CashSubscriptions.LastOrDefault(x => x.isActive == true && x.Status == TopUpStatus.Accepted);
                        if (pkg != null && pkg.isActive)
                        {
                            cashSubscription.RemainingRides += pkg.RemainingRides;
                            cashSubscription.PreviousPackageRides = pkg.RemainingRides;
                            pkg.isActive = false;
                            Notification notf = new Notification();
                            var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.Driver_Id == driverId).ToList();
                            PushNotificationsHelper.SendIOSPushNotifications(Message: "Your subscription request has been accepted and amount is added to your wallet.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                            PushNotificationsHelper.SendAndroidPushNotifications(Message: "Your subscription request has been accepted and amount is added to your wallet.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                            _dbContext.Notifications.Add(notf);
                        }
                    }

                    cashSubscription.Status = TopUpStatus.Accepted;
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool PayOutstandingBalance(DriverPaymentBindingModelList model)
        {
            try
            {
                foreach (var item in model.Drivers)
                {
                    Driver driver = _dbContext.Drivers.FirstOrDefault(x => x.Id == item.DriverId);
                    if (driver == null)
                    {
                        return false;
                    }

                    DriverPayment request = new DriverPayment { Driver_Id = item.DriverId, Amount = item.Amount };
                    driver.Wallet-= item.Amount;
                    _dbContext.DriverPayments.Add(request);

                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public PromocodeDTOList AddMapCode(AddPromocodeBindingModel model)
        {
            try
            {
                var code = _dbContext.Promocodes.FirstOrDefault(x => x.Code == model.Code);
                if (code != null)
                {
                    return null;
                }
                else
                {
                    List<string> Code = model.Code.Split(',').ToList();
                    PromocodeDTOList response = new PromocodeDTOList();
                    List<Promocode> CodeList = new List<Promocode>();
                    List<string> ErrorCodes = new List<string>();
                    var user = _dbContext.Users.FirstOrDefault(x => x.PhoneNo == model.PhoneNumber || x.Email == model.Email || x.UserName == model.FullName);
                    foreach (var item in Code)
                    {
                        if (_dbContext.Promocodes.Any(x => x.Code == item))
                        {
                            ErrorCodes.Add(item);
                        }
                        else
                        {
                            CodeList.Add(new Promocode
                            {
                                Code = item,
                                CodeType = model.CodeType,
                                CouponType = model.CouponType,
                                CouponAmount = Convert.ToInt32(model.CouponAmount),
                                Discount = model.CouponAmount,
                                Type = (PromocodeType)model.CouponType,
                                CreatedDate = DateTime.UtcNow,
                                Details = model.Details,
                                IsDeleted = false,
                                IsExpired = false,
                                FullName = model.FullName,
                                Email = model.Email,
                                PhoneNumber = model.PhoneNumber,
                                ActivationDate = model.ActivationDate,
                                ExpiryDate = model.ExpiryDate,
                                LimitOfUsage = model.LimitOfUsage
                            });

                            //if (user != null)
                            //{
                            //    CodeList.Last().User_Id = user.Id;
                            //}
                        }
                    }

                    _dbContext.Promocodes.AddRange(CodeList);
                    _dbContext.SaveChanges();
                    Mapper.Map(CodeList, response.Codes);

                    if (ErrorCodes.Count > 0)
                    {
                        response.ErrorMessage = String.Join(",", ErrorCodes);
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FareCalculationDTO> GetAllFareCalculation(int cityId, PaymentMethods paymentType)
        {

            try
            {
                List<FareCalculationDTO> fareCalculationDTOs = new List<FareCalculationDTO>();
                List<FareCalculation> fares = new List<FareCalculation>();
                fares = _dbContext.FareCalculations.Include(x => x.City).ThenInclude(x => x.CityMLsList).Where(x => x.IsDeleted == false).ToList();
                if (cityId > 0)
                {
                    fares = fares.Where(x => x.City_Id == cityId).ToList();
                }

                if (paymentType > 0)
                {
                    fares = fares.Where(x => x.PaymentMethod == paymentType).ToList();
                }

                Mapper.Map(fares, fareCalculationDTOs);
                for (int i = 0; i < fares.Count; i++)
                {
                    CityML englishML = fares[i].City.CityMLsList.FirstOrDefault(x => x.Culture == CultureType.English);
                    Mapper.Map(englishML, fareCalculationDTOs[i].City.English);
                    fareCalculationDTOs[i].City.Arabic = null;
                    fareCalculationDTOs[i].City.Country = null;
                }

                return fareCalculationDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public List<InvitedFriendDTO> GetAppReferrers(string username = "")
        {

            try
            {
                List<InvitedFriendDTO> referrersDTOs = new List<InvitedFriendDTO>();
                List<InvitedFriend> referrers = new List<InvitedFriend>();

                if (!String.IsNullOrEmpty(username))
                {
                    int inviterId = _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(username.ToLower())).Id;
                    if (inviterId > 0)
                    {
                        referrers = _dbContext.InvitedFriends.Include(x => x.Referrer).Include(x => x.InvitedUser).Where(x => x.Referrer_Id == inviterId).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    referrers = _dbContext.InvitedFriends.Include(x => x.Referrer).Include(x => x.InvitedUser).ToList();
                }

                Mapper.Map(referrers, referrersDTOs);
                return referrersDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TopUpDTO> UserWalletReport(string uniqueId)
        {

            try
            {
                List<TopUpDTO> topupDTOs = new List<TopUpDTO>();
                List<TopUp> topUps = new List<TopUp>();

                if (!String.IsNullOrEmpty(uniqueId))
                {
                    int userId = _dbContext.Users.FirstOrDefault(x => x.UniqueId.Equals(uniqueId)).Id;
                    if (userId > 0)
                    {
                        topUps = _dbContext.BankTopUps.Include(x => x.User).Include(x => x.Receipts).Where(x => x.User_Id == userId).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    topUps = _dbContext.BankTopUps.Include(x => x.User).Include(x => x.Receipts).ToList();
                }
                Mapper.Map(topUps, topupDTOs);
                return topupDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DriverPaymentDTO> PaymentReport(string uniqueId, string StartDate = "", string EndDate = "")
        {

            try
            {
                List<DriverPaymentDTO> paymentDTOs = new List<DriverPaymentDTO>();
                List<DriverPayment> payments = new List<DriverPayment>();

                if (!String.IsNullOrEmpty(uniqueId))
                {
                    int userId = _dbContext.Drivers.FirstOrDefault(x => x.UniqueId.Equals(uniqueId)).Id;
                    if (userId > 0)
                    {
                        payments = _dbContext.DriverPayments.Include(x => x.Driver).Where(x => x.Driver_Id == userId).ToList();

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {

                    payments = _dbContext.DriverPayments.Include(x => x.Driver).ToList();
                }
                if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                {
                    DateTime startDateTime;
                    DateTime endDateTime;
                    startDateTime = DateTime.ParseExact(StartDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    endDateTime = DateTime.ParseExact(EndDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    payments = payments.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();
                }
                Mapper.Map(payments, paymentDTOs);
                return paymentDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public EarningReportDTO TotalEarningsReport(string startDate, string endDate)
        {
            try
            {
                EarningReportDTO response = new EarningReportDTO();
                var tripsFares = _dbContext.Trips.Include(x => x.PrimaryUser).Include(x => x.Driver).Where(X => X.IsDeleted == false).ToList();
                var driverDeposits = _dbContext.CashSubscriptions.Include(x => x.Driver).Include(x => x.SubscriptionPackage).Where(X => X.IsDeleted == false).ToList();
                var payments = _dbContext.DriverPayments.Include(x => x.Driver).ToList();

                if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                {
                    DateTime startDateTime;
                    DateTime endDateTime;
                    startDateTime = DateTime.ParseExact(startDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    endDateTime = DateTime.ParseExact(endDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    payments = payments.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();
                    driverDeposits = driverDeposits.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();
                    tripsFares = tripsFares.Where(x => x.CreatedDate.Date >= startDateTime.Date && x.CreatedDate.Date <= endDateTime.Date).ToList();
                }

                Mapper.Map(tripsFares, response.UserDeposits);
                Mapper.Map(driverDeposits, response.DriverSubscriptions);
                Mapper.Map(payments, response.OutFlows);
                foreach (var item in response.DriverSubscriptions)
                {
                    item.Driver.CashSubscriptions = null;
                }

                foreach (var item in response.OutFlows)
                {
                    item.Driver.CashSubscriptions = null;
                }

                foreach (var item in response.UserDeposits)
                {
                    item.Driver = null;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DriverLogDTOList DriverLogReport(string startDate, string endDate)
        {
            try
            {
               var logs= _dbContext.DriverLogs.Include(x=>x.Driver).ThenInclude(x=>x.Trips).ToList();
                DriverLogDTOList driverLogDTOs = new DriverLogDTOList();
                Mapper.Map(logs,driverLogDTOs.Logs);
                for (int i = 0; i < logs.Count; i++)
                {
                    driverLogDTOs.Logs[i].TotalRides = logs[i].Driver.Trips.Count(x => x.EndTime < logs[i].OfflineTime && x.EndTime > logs[i].OnlineTime);
                }
                return driverLogDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

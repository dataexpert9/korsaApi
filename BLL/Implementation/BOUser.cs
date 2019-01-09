using AppModel.BindingModels;
using AppModel.DomainModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Services;
using static Services.Global;

namespace BLL.Implementation
{
    public class BOUser : IBOUser
    {
        public DataContext _dbContext { get; set; }

        public BOUser(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string phone)
        {
            try
            {
                return _dbContext.Users.Any(x => x.PhoneNo == phone);
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }
        public string GetPhoneNo(string username, UserTypes userType)
        {
            try
            {
                return (userType == UserTypes.User) ? _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(username.ToLower()) || x.PhoneNo.Equals(username)).PhoneNo : _dbContext.Drivers.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()) || x.PhoneNo.Equals(username)).PhoneNo;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }


        public bool Exists(string username, string phoneNum)
        {
            try
            {
                return _dbContext.Users.Any(x => x.PhoneNo == phoneNum || x.UserName.ToLower() == username.ToLower());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EditUserExists(string username, string phoneNum, string UniqueId)
        {
            try
            {
                User user = _dbContext.Users.Where(x => x.PhoneNo == phoneNum || x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    if (user.UniqueId == UniqueId)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public PromocodeDTO VerifyPromocode(string promocode, int userId)
        {
            try
            {
                Promocode code = _dbContext.Promocodes.FirstOrDefault(x => x.Code == promocode && x.IsDeleted == false && (x.User_Id == null || x.User_Id == userId));
                PromocodeDTO response = new PromocodeDTO();

                if (code != null)
                {
                    if (code.ActivationDate.Date <= DateTime.UtcNow.Date && code.ExpiryDate >= DateTime.UtcNow.Date && code.LimitOfUsage > 0 && code.IsExpired == false)
                    {
                        if (code.LimitOfUsage-- <= 0)
                        {
                            code.IsExpired = true;
                        }

                        _dbContext.SaveChanges();
                        return null;
                    }
                    if (_dbContext.UserPromocode.Any(x => x.User_Id == userId && x.Promocode_Id == code.Id))
                    {
                        return response;
                    }

                    Mapper.Map(code, response);
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerifyInvitaionCode(string invitationCode)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.UniqueId == invitationCode);
                if (user == null)
                {
                    return 0;
                }

                return user.Id;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return 0;
            }

        }

        public bool ContactUs(string message, int userId, bool isDriver)
        {
            try
            {
                ContactUs obj = new ContactUs { Message = message };
                if (isDriver)
                {
                    obj.Driver_Id = userId;
                }
                else
                {
                    obj.User_Id = userId;
                }

                _dbContext.ContactUs.Add(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }

        }

        public List<CancellationReasonDTO> GetAllCancellationReason(CultureType culture)
        {

            try
            {
                List<CancellationReason> cancellationReasonsList = _dbContext.CancellationReasons.Include(x => x.CancellationReasonMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture).ToList();
                List<CancellationReasonDTO> cancellationReasonsResponseList = Mapper.Map<List<CancellationReason>, List<CancellationReasonDTO>>(cancellationReasonsList);
                CancellationReasonDTO cancellationReasonsDTO = new CancellationReasonDTO();
                for (int i = 0; i < cancellationReasonsList.Count; i++)
                {
                    CancellationReasonML cancellationReasonML = cancellationReasonsList[i].CancellationReasonMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(cancellationReasonML, cancellationReasonsResponseList[i]);
                }
                return cancellationReasonsResponseList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }

        public List<NotificationDTO> GetNotifications(int id, UserTypes userType)
        {

            try
            {
                List<NotificationDTO> notificationDTOs = new List<NotificationDTO>();
                List<Notification> notifications = new List<Notification>();
                if (userType == UserTypes.Driver)
                {
                    notifications = _dbContext.Notifications.Where(x => x.Driver_Id == id && x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    notifications = _dbContext.Notifications.Where(x => x.User_Id == id && x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                }

                Mapper.Map(notifications, notificationDTOs);
                return notificationDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }



        public TripDTO GetCurrentStatus(UserTypes userType, int id)
        {
            try
            {
                TripDTO tripDTO = new TripDTO();
                Trip trip = new Trip();
                switch (userType)
                {
                    case UserTypes.User:
                        trip = _dbContext.Trips.Include(x => x.Driver).ThenInclude(X=>X.Vehicles).LastOrDefault(x => x.PrimaryUser_Id == id && x.Status >= TripStatus.Requested && x.Status <= TripStatus.Started && x.StartTime.Date == DateTime.UtcNow.Date);
                        if (trip != null)
                        {
                            Mapper.Map(trip, tripDTO);
                            return tripDTO;
                        }
                        else
                        {
                            return null;
                        }

                    case UserTypes.Driver:
                        trip = _dbContext.Trips.Include(X => X.PrimaryUser).LastOrDefault(x => x.Driver_Id == id && x.Status >= TripStatus.AssignedToDriver && x.Status <= TripStatus.Started && x.StartTime.Date == DateTime.UtcNow.Date);

                        if (trip != null)
                        {
                            Mapper.Map(trip, tripDTO);
                            return tripDTO;
                        }
                        else
                        {
                            return null;
                        }

                    default:
                        return null;
                }

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

        public User GetUserById(int Id)
        {
            try
            {
                User user = _dbContext.Users
                    .Include(x => x.CreditCards)
                    .FirstOrDefault(x => x.Id == Id);
                return user;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public UserDTO InsertUser(User user, string invitationCode)
        {
            try
            {
                var referrer = _dbContext.Users.FirstOrDefault(x => x.UniqueId == invitationCode);

                int code = new Random().Next(11111, 99999);
                generateId: string id = code.ToString();
                if (_dbContext.Users.Any(x => x.UniqueId == id && !x.IsDeleted))
                {
                    goto generateId;
                }

                user.UniqueId = id;
                user.DriverPreference = (user.Gender == Gender.Female) ? Gender.Female : Gender.Male;
                _dbContext.Users.Add(user);

                if (referrer != null)
                {
                    referrer.CurrentReferralCount++;
                    InvitedFriend invitedFriend = new InvitedFriend { InvitedUser_Id = user.Id, Referrer_Id = referrer.Id };
                    _dbContext.InvitedFriends.Add(invitedFriend);
                    if (referrer.CurrentReferralCount % 10 == 0 && referrer.CurrentReferralCount > 0)//
                    {
                        referrer.FreeRides++;
                        Promocode _code = new Promocode { Code = id, ActivationDate = DateTime.UtcNow, ExpiryDate = DateTime.UtcNow.AddMonths(1), LimitOfUsage = 1, User_Id = referrer.Id, Type = PromocodeType.DiscountPercentage, Discount = 100, Details = "Free Ride" };
                        Notification notf = new Notification();
                        var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id==referrer.Id).ToList();
                        PushNotificationsHelper.SendIOSPushNotifications(Message: "You have got free ride for referring users on our app. You can use promocode " + _code.Code + " to get 100% discount on your ride", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                        PushNotificationsHelper.SendAndroidPushNotifications(Message: "You have got free ride for referring users on our app. You can use promocode " + _code.Code + " to get 100% discount on your ride", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                        _dbContext.Notifications.Add(notf);
                        _dbContext.Promocodes.Add(_code);
                        _dbContext.SaveChanges();
                    }
                }

                _dbContext.SaveChanges();
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                return userDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FavouriteLocation AddFavouriteLocation(FavouriteLocationBindingModel model, int userId)
        {
            try
            {
                FavouriteLocation favouriteLocation = new FavouriteLocation(); // { FormattedAddress = model.FormattedAddress, PlaceId = model.PlaceId, User_Id = userId };
                Mapper.Map(model, favouriteLocation);
                favouriteLocation.User_Id = userId;
                _dbContext.FavouriteLocations.Add(favouriteLocation);
                _dbContext.SaveChanges();
                return favouriteLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public SupportConversation AddSupportConversation(SupportConversationBindingModel model)
        {
            try
            {
                SupportConversation support = (_dbContext.SupportConversations.Any(x => x.EntityId == model.EntityId && x.userType == model.userType)) ? _dbContext.SupportConversations.First(x => x.EntityId == model.EntityId && x.userType == model.userType) : new SupportConversation();
                if (support.Id > 0)
                {
                    support.LastConversationDate = DateTime.UtcNow;
                }
                else
                {
                    Mapper.Map(model, support);
                }

                _dbContext.SupportConversations.Add(support);
                _dbContext.SaveChanges();
                return support;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FavouriteLocationDTO> GetFavouriteLocations(int id)
        {

            try
            {
                List<FavouriteLocationDTO> favouriteLocationDTOs = new List<FavouriteLocationDTO>();
                List<FavouriteLocation> favourites = new List<FavouriteLocation>();

                favourites = _dbContext.FavouriteLocations.Where(x => x.User_Id == id && x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(favourites, favouriteLocationDTOs);
                return favouriteLocationDTOs;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }

        }


        public bool UnFavouriteLocation(int id)
        {
            try
            {
                FavouriteLocation loc = _dbContext.FavouriteLocations.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
                if (loc != null)
                {
                    loc.IsDeleted = true;
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

        public bool AddBankTopupRequest(int userId, List<string> topUpReceiptUrls, AddBankTopUpBindingModel model)
        {
            try
            {
                TopUp topUp = new TopUp { User_Id = userId, Amount = model.Amount, Account_Id = model.Account_Id, Status = TopUpStatus.Pending };
                _dbContext.BankTopUps.Add(topUp);

                foreach (var img in topUpReceiptUrls)
                {
                    TopUpMedia topUpMedia = new TopUpMedia { MediaUrl = img, TopUp_Id = topUp.Id };
                    _dbContext.TopUpMedias.Add(topUpMedia);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public CreditCardDTO AddUserCreditCard(AddCreditCardBindingModel model, int Id)
        {
            try
            {
                CreditCard card = new CreditCard { CardNumber = model.CardNumber, CVV = model.CVV, Name = model.Name, CardTypeName = model.CardNumber };
                if (model.UserType == UserTypes.User)
                {
                    card.User_Id = Id;
                }
                else
                {
                    card.Driver_Id = Id;
                }

                _dbContext.CreditCards.Add(card);
                _dbContext.SaveChanges();
                CreditCardDTO creditCardDTO = Mapper.Map<CreditCard, CreditCardDTO>(card);
                return creditCardDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User AuthenticateCredentials(string username, string password)
        {
            try
            {
                return _dbContext.Users
                    .Include(x => x.CreditCards)
                    .FirstOrDefault(x => (x.UserName.ToLower() == username.ToLower() || x.PhoneNo == username) && x.Password == password && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                User User = new User();
                if (user.Id > 0)
                {
                    User = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id && x.IsDeleted == false);
                    User.FullName = user.FullName;
                    User.UserName = user.UserName;
                    User.Email = user.Email;
                    User.PhoneNo = user.PhoneNo;
                    User.City_Id = user.City_Id;
                    User.Address = user.Address;
                    User.DateofBirth = user.DateofBirth;
                    User.Gender = user.Gender;
                    if (!String.IsNullOrEmpty(user.ProfilePictureUrl))
                    {
                        User.ProfilePictureUrl = user.ProfilePictureUrl;
                    }

                    _dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public User ResetForgotPassword(string CellNumOrUsername, string code)
        {

            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.PhoneNo == CellNumOrUsername || x.UserName == CellNumOrUsername);
                if (user == null)
                {
                    return null;
                }

                user.Password = CryptoHelper.Hash(code.ToString());
                _dbContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public bool IsUserNumberAlreadyVerified(string PhoneNum, UserTypes userType)
        {
            try
            {
                if (userType == UserTypes.User)
                {
                    return _dbContext.Users.Any(x => x.PhoneNo == PhoneNum);
                }
                else
                {
                    return _dbContext.Drivers.Any(x => x.PhoneNo == PhoneNum);
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return false;
            }
        }

        public int SendVerficationCode(string PhoneNum, UserTypes userType)
        {
            try
            {
                int code = new Random().Next(11111, 99999);
                if (_dbContext.VerifyNumberCodes.Any(x => x.Phone == PhoneNum && x.UserType == userType && !x.IsDeleted))
                {
                    var numCode = _dbContext.VerifyNumberCodes.FirstOrDefault(x => x.Phone == PhoneNum && x.UserType == userType && !x.IsDeleted);
                    numCode.Code = code;
                    numCode.CreatedDate = DateTime.UtcNow;
                }
                else
                {
                    _dbContext.VerifyNumberCodes.Add(new VerifyNumberCode { Phone = PhoneNum, Code = code, UserType = userType });
                }

                _dbContext.SaveChanges();
                Configuration.Instance.Settings["appSettings:Nexmo.api_key"] = AppSettingsProvider.NexmoApiKey;
                Configuration.Instance.Settings["appSettings:Nexmo.api_secret"] = AppSettingsProvider.NexmoApiSecret;
                Configuration.Instance.Settings["appSettings:NEXMO_FROM_NUMBER"] = AppSettingsProvider.NexmoFromNumber;
                Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"] = AppSettingsProvider.NexmoApplicationId;

                PhoneNum = "+" + PhoneNum;
                var results = SMS.Send(new SMS.SMSRequest
                {
                    from = AppSettingsProvider.NexmoMessageSenderName.ToUpper(),//KORSA
                    title = AppSettingsProvider.NexmoMessageSenderName.ToUpper(),//KORSA
                    to = PhoneNum,
                    text = "Verification Code : " + code
                });
                if (results.messages.First().status == "0")
                {
                    return code;
                }

                using (StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "/ErrorLog.txt"))
                {
                    sw.WriteLine("nexmo error : " + DateTime.Now + Environment.NewLine);
                    sw.WriteLine(Environment.NewLine + "Status" + results.messages.First().status);
                    sw.WriteLine(Environment.NewLine + "RemainingBalance" + results.messages.First().remaining_balance);
                    sw.WriteLine(Environment.NewLine + "MessagePrice" + results.messages.First().message_price);
                    sw.WriteLine(Environment.NewLine + "ErrorText" + results.messages.First().error_text);
                    sw.WriteLine(Environment.NewLine + "ClientRef" + results.messages.First().client_ref);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool VerifySmsCode(int code, UserTypes userType)
        {
            try
            {
                VerifyNumberCode codeEntry = _dbContext.VerifyNumberCodes.FirstOrDefault(x => x.Code == code && x.UserType == userType && x.IsDeleted == false && DateTime.UtcNow.Subtract(x.CreatedDate).TotalMinutes < 60);
                if (codeEntry != null)
                {
                    codeEntry.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<object> ConfirmPaypalPayment(PaymentConfirmationBindingModel model, int id)
        {
            try
            {
                if (_dbContext.PaymentHistories.Any(x => x.TransactionId.Equals(model.TransactionId)))
                {
                    return HttpStatusCode.Conflict;
                }
                
                var client = new HttpClient();
                string result = String.Empty;
                var keyValues = new List<KeyValuePair<string, string>>();
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(AppSettingsProvider.PaypalUsername + ":" + AppSettingsProvider.PaypalPassword));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
                var request = new HttpRequestMessage(HttpMethod.Post, AppSettingsProvider.PaypalAPIGetTokenUrl);
                keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                request.Content = new FormUrlEncodedContent(keyValues);
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = await response.Content.ReadAsStringAsync();
                    PaypalAccessTokenModel tokenModel = JsonConvert.DeserializeObject<PaypalAccessTokenModel>(result);
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return HttpStatusCode.NotFound;
                    }
                    else if (response.StatusCode == HttpStatusCode.OK)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.access_token);
                        response = await client.GetAsync(AppSettingsProvider.PaypalAPIVerificationUrl + model.TransactionId);
                        result = await response.Content.ReadAsStringAsync();
                        PaypalVerificationModel paymentModel = JsonConvert.DeserializeObject<PaypalVerificationModel>(result);


                        if (model.SubscriptionPackage_Id > 0)
                        {
                            var package = _dbContext.SubscriptionPackages.FirstOrDefault(x => x.Id == model.SubscriptionPackage_Id);
                            var driver = _dbContext.Drivers.Include(x => x.CashSubscriptions).FirstOrDefault(x => x.Id == id);
                            CashSubscription cashSubscription = new CashSubscription { RemainingRides = package.NumOfRides, Driver_Id = id, Amount = model.Amount, SubscriptionPackage_Id = model.SubscriptionPackage_Id.Value, Status = TopUpStatus.Accepted, isActive = true, PaymentType = PaymentMethods.CreditCard };
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
                            PaymentHistory paymentHistory = new PaymentHistory { TransactionId = model.TransactionId, CreatedDate = Convert.ToDateTime(paymentModel.Create_time), ModifiedDate = Convert.ToDateTime(paymentModel.Create_time), Driver_Id = id, Package_Id = model.SubscriptionPackage_Id };
                            driver.PaymentHistories.Add(paymentHistory);
                            _dbContext.SaveChanges();
                            return HttpStatusCode.OK;
                        }
                        else
                        {
                            var user = _dbContext.Users.Include(x => x.BankTopUps).FirstOrDefault(x => x.Id == id);
                            var settings = _dbContext.Settings.FirstOrDefault();
                            var paypalAmount = paymentModel.Transactions.First().Amount;
                            double convertedAmount = 0;
                            if (paypalAmount != null)
                            {
                                TopUp topUp = new TopUp { Amount = convertedAmount, PaymentType = PaymentMethods.CreditCard, Status = TopUpStatus.Accepted };
                                user.BankTopUps.Add(topUp);
                                convertedAmount = paypalAmount.Total / settings.CurrencyToUSDRatio;
                                PaymentHistory paymentHistory = new PaymentHistory { TransactionId = model.TransactionId, CreatedDate = Convert.ToDateTime(paymentModel.Create_time), ModifiedDate = Convert.ToDateTime(paymentModel.Create_time), USDAmount = paypalAmount.Total, LocalCurrencyAmount = convertedAmount };
                                user.PaymentHistories.Add(paymentHistory);
                                user.Wallet += convertedAmount;
                            }
                            _dbContext.SaveChanges();
                            return HttpStatusCode.OK;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserDeviceDTO RegisterDeviceForPushNotification(int id, UserTypes userType, UserDevice device)
        {
            UserDeviceDTO response = new UserDeviceDTO();
            if (userType == UserTypes.User)
            {
                User user = _dbContext.Users.Include(x => x.UserDevices).FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    UserDevice existingUserDevice = user.UserDevices.FirstOrDefault(x => x.UDID.Equals(device.UDID));
                    if (existingUserDevice == null)
                    {
                        PushNotificationsUtil.ConfigurePushNotifications(device);

                        user.UserDevices.Add(device);
                        _dbContext.SaveChanges();
                        response = Mapper.Map<UserDevice, UserDeviceDTO>(device);
                        return response;
                    }
                    else
                    {
                        _dbContext.UserDevices.FirstOrDefault(x => x.UDID == existingUserDevice.UDID && x.IsActive == true).IsActive = false;
                        existingUserDevice.Driver_Id = null;
                        existingUserDevice.User_Id = user.Id;
                        existingUserDevice.IsActive = true;
                        _dbContext.SaveChanges();
                        PushNotificationsUtil.ConfigurePushNotifications(existingUserDevice);
                        response = Mapper.Map<UserDevice, UserDeviceDTO>(existingUserDevice);
                        return response;
                    }
                }

                else
                {
                    return null;
                }
            }

            else if (userType == UserTypes.Driver)
            {
                Driver driver = _dbContext.Drivers.Include(x => x.DriverDevices).FirstOrDefault(x => x.Id == id);
                if (driver != null)
                {
                    UserDevice existingDevice = driver.DriverDevices.FirstOrDefault(x => x.UDID.Equals(device.UDID));
                    if (existingDevice == null)
                    {
                        PushNotificationsUtil.ConfigurePushNotifications(device);
                        driver.DriverDevices.Add(device);
                        _dbContext.SaveChanges();
                        response = Mapper.Map<UserDevice, UserDeviceDTO>(device);
                        return response;
                    }
                    else
                    {
                        _dbContext.UserDevices.FirstOrDefault(x => x.UDID == existingDevice.UDID && x.IsActive == true).IsActive = false;
                        existingDevice.User_Id = null;
                        existingDevice.Driver_Id = driver.Id;
                        existingDevice.IsActive = true;
                        _dbContext.SaveChanges();
                        PushNotificationsUtil.ConfigurePushNotifications(existingDevice);
                        response = Mapper.Map<UserDevice, UserDeviceDTO>(existingDevice);
                        return response;
                    }
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public bool isPhoneConfirmationValid(string phoneNum)
        {
            return (_dbContext.VerifyNumberCodes.Any(x => x.Phone == phoneNum && x.IsDeleted == true && DateTime.UtcNow.Subtract(x.CreatedDate).TotalMinutes < 60)) ? true : false;
        }

        public User GetUser(string usernameOrCellNum)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.PhoneNo == usernameOrCellNum || x.UserName == usernameOrCellNum);
                return user;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public Admin WebPanelLogin(string username, string password)
        {
            var admin = _dbContext.Admins.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }



    }
}

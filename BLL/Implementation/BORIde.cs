using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services;
using static Services.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class BORide : IBORide
    {

        public DataContext _dbContext { get; set; }

        public BORide(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<RideTypeDTO> GetAllRideTypes(CultureType culture, int estimatedTime, double distanceInKm, string promocode, string PickupCity, string DestinationCity, PaymentMethods paymentType, int? userId, out int promoId)
        {
            try
            {
                List<RideType> RideTypeList = _dbContext.RideTypes.Include(x => x.RideTypeMLsList).Where(x => x.Culture == CultureType.Both || x.Culture == culture && x.IsDeleted == false).ToList();
                List<RideTypeDTO> RideTypeResponseList = Mapper.Map<List<RideType>, List<RideTypeDTO>>(RideTypeList);
                RideTypeDTO RideTypeDTO = new RideTypeDTO();
                promoId = 0;
                FareCalculation fare = null;
                if (!String.IsNullOrEmpty(PickupCity) && !String.IsNullOrEmpty(DestinationCity))
                {
                    var pickup = _dbContext.CityMLs.FirstOrDefault(x => x.Name.ToLower().Equals(PickupCity) && x.City.IsDeleted == false && x.City.IsActive == true);
                    var destination = _dbContext.CityMLs.FirstOrDefault(x => x.Name.ToLower().Equals(DestinationCity) && x.City.IsDeleted == false && x.City.IsActive == true);
                    if (pickup == null || destination == null)
                    {
                        return null;
                    }

                    fare = _dbContext.FareCalculations.FirstOrDefault(x => x.City_Id == pickup.City_Id && x.StartTime < DateTime.UtcNow.TimeOfDay && x.EndTime > DateTime.UtcNow.TimeOfDay && x.PaymentMethod == paymentType);
                }

                for (int i = 0; i < RideTypeList.Count; i++)
                {
                    RideTypeML rideTypeML = RideTypeList[i].RideTypeMLsList.FirstOrDefault(x => x.Culture == culture);
                    Mapper.Map(rideTypeML, RideTypeResponseList[i]);

                    if (distanceInKm > 0)
                    {
                        if (fare != null)
                        {
                            RideTypeResponseList[i].EstimatedFare = fare.BasicCharges + (distanceInKm * fare.FarePerKM) + (estimatedTime * fare.FarePerMin);
                        }
                        else
                        {
                            RideTypeResponseList[i].EstimatedFare = RideTypeList[i].BasicCharges + (distanceInKm * RideTypeList[i].FarePerKM) + (estimatedTime * RideTypeList[i].FarePerMin);
                        }
                    }

                    if (!String.IsNullOrEmpty(promocode))
                    {
                        Promocode code = _dbContext.Promocodes.FirstOrDefault(x => x.Code == promocode && x.IsDeleted == false && x.ActivationDate.Date <= DateTime.UtcNow.Date && x.ExpiryDate.Date >= DateTime.UtcNow.Date && x.LimitOfUsage > x.UsageCount && x.IsExpired == false && (x.User_Id==null || x.User_Id==userId ));
                        PromocodeDTO response = new PromocodeDTO();

                        if (code != null)
                        {
                            if (code.Type == PromocodeType.FixedDiscount)
                            {
                                RideTypeResponseList[i].EstimatedFare = RideTypeResponseList[i].EstimatedFare - code.Discount;
                            }
                            else if (code.Type == PromocodeType.DiscountPercentage)
                            {
                                RideTypeResponseList[i].EstimatedFare = RideTypeResponseList[i].EstimatedFare * ((100 - code.Discount) / 100);
                            }

                            promoId = code.Id;
                        }
                    }
                }
                return RideTypeResponseList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public MessageDTO InsertMessage(Message message)
        {
            try
            {
                _dbContext.Messages.Add(message);
                _dbContext.SaveChanges();
                MessageDTO response = Mapper.Map<Message, MessageDTO>(message);
                return response;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public Trip InsertTrip(Trip trip)
        {
            try
            {
                _dbContext.Trips.Add(trip);
                _dbContext.SaveChanges();
                UserTrip userTrip = new UserTrip { User_Id = trip.PrimaryUser_Id.Value, Trip_Id = trip.Id, RequestTime = trip.RequestTime, isScheduled = trip.isScheduled };
                _dbContext.UserTrips.Add(userTrip);
                _dbContext.SaveChanges();
                return trip;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DriverDTO> GetNearByDrivers(Loc location, int rideId, int? rideTypId, string pushKey)
        {
            try
            {
                List<Driver> drivers = (rideTypId.HasValue) ? _dbContext.Drivers.Include(x => x.DriverDevices).Where(x => x.IsDeleted == false && x.IsNotificationsOn).ToList() : _dbContext.Drivers.Include(x => x.DriverDevices).Where(x => x.IsDeleted == false && x.IsNotificationsOn).ToList();
                Settings settings = _dbContext.Settings.FirstOrDefault();
                //drivers.RemoveAll(x => x.Location.Distance(location) > settings.MinimumRequestRadius);
                //_dbContext.Database.GetDbConnection().Close();
                var driverIds = drivers.Take(10).Select(x => x.Id);
                List<UserDevice> userdevices = _dbContext.UserDevices.Where(x1 => x1.Platform == false & x1.IsActive == true && driverIds.Contains(x1.Driver_Id.Value)).ToList();
                //Global.objPushNotifications.SendIOSPushNotification(userdevices, OtherNotification: new Notification { Title = "KORSA", Text = "There is a ride near you", Entity_ID = rideId }, key: pushKey, Type: (int)PushNotificationType.NearbyRideForDriver);
                List<DriverDTO> driverDTOList = new List<DriverDTO>();
                driverDTOList = Mapper.Map(drivers, driverDTOList);
                return driverDTOList;
                //return _dbContext.Drivers.Take(5).ToList();
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public bool SendPushToUser(int userId, int driverId, Notification notf, PushNotificationType type, string key)
        {

            List<UserDevice> userdevices = _dbContext.UserDevices.Where(x1 => x1.Platform == false & x1.IsActive == true && x1.User_Id == userId).ToList();
            Global.objPushNotifications.SendIOSPushNotification(userdevices, OtherNotification: notf, key: key, Type: (int)type);

            _dbContext.Notifications.Add(notf);
            _dbContext.SaveChanges();
            return true;
        }

        public bool SendPushToDriver(int userId, int driverId, Notification notf, PushNotificationType type, string key)
        {
            List<UserDevice> userdevices = _dbContext.UserDevices.Where(x1 => x1.Platform == false & x1.IsActive == true && x1.Driver_Id == driverId).ToList();
            Global.objPushNotifications.SendIOSPushNotification(userdevices, OtherNotification: notf, key: key, Type: (int)type);

            _dbContext.Notifications.Add(notf);
            _dbContext.SaveChanges();
            return true;
        }

        public List<MessageDTO> GetMessages(int userId, int driverId)
        {
            try
            {
                var messages = _dbContext.Messages.Where(x => x.IsDeleted == false && x.User_Id == userId && x.Driver_Id == driverId && DateTime.UtcNow.Subtract(x.CreatedDate).TotalHours < 24).OrderBy(x => x.CreatedDate).ToList();
                List<MessageDTO> msgDTOList = new List<MessageDTO>();
                msgDTOList = Mapper.Map(messages, msgDTOList);
                return msgDTOList;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public bool UpdateTrip(Trip trip)
        {
            try
            {




                _dbContext.Trips.Update(trip);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EndRide(FinishRideBindingModel model)
        {
            try
            {
                Trip ride = _dbContext.Trips.Include(x => x.Driver)
                    .Include(s => s.PrimaryUser)
                    .FirstOrDefault(x => x.Id == model.RideId);

                ride.EndTime = DateTime.UtcNow;
                ride.Status = TripStatus.Completed;
                ride.Fare = ride.EstimatedFare;
                if (ride.PrimaryUser.UseCreditFirst)
                {
                    if (ride.EstimatedFare <= ride.PrimaryUser.Wallet)
                    {
                        ride.PrimaryUser.Wallet -= ride.EstimatedFare;
                        ride.WalletDeduction = ride.EstimatedFare;
                    }
                    else
                    {
                        ride.WalletDeduction = ride.PrimaryUser.Wallet;
                        ride.PrimaryUser.Wallet = 0;
                    }

                }
                ride.Driver.RidesCount++;
                ride.Driver.TotalMileage += model.DistanceTravelled;
                ride.Driver.Wallet += ride.WalletDeduction;
                ride.PrimaryUser.RidesCount++;
                ride.PrimaryUser.DistanceTravelled += model.DistanceTravelled;
                ride.EndTime = DateTime.UtcNow;
                _dbContext.Trips.Update(ride);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Trip GetTripById(int Id)
        {
            try
            {
                Trip trip = _dbContext.Trips.FirstOrDefault(x => x.Id == Id);
                return trip;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public Trip GetTripByIdWithDriverAndUser(int Id)
        {
            try
            {
                Trip trip = _dbContext.Trips.Include(x => x.Driver)
                    .Include(s => s.PrimaryUser)
                    .FirstOrDefault(x => x.Id == Id);
                return trip;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public List<TripDTO> GetUpcomingRidesByUserId(int id, CultureType culture)
        {
            try
            {
                List<Trip> trips = _dbContext.UserTrips
                    .Where(x => x.User_Id == id && x.isScheduled == true && x.Trip.Status == TripStatus.Requested && x.RequestTime >= DateTime.UtcNow).Select(c => c.Trip)
                    .Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList).Include(x => x.PrimaryUser).ToList();
                List<TripDTO> tripsResponse = Mapper.Map<List<Trip>, List<TripDTO>>(trips);
                for (int i = 0; i < trips.Count; i++)
                {
                    tripsResponse[i].RideTypeName = trips[i].RideType.RideTypeMLsList.FirstOrDefault(x => x.Culture == culture).Name;
                }
                return tripsResponse;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }

        public List<TripDTO> GetPastRidesByUserId(int id, CultureType culture)
        {
            try
            {
                List<Trip> trips = _dbContext.UserTrips
                    .Where(x => x.User_Id == id && x.Trip.Status == TripStatus.Completed && x.Trip.EndTime <= DateTime.UtcNow).Select(c => c.Trip)
                    .Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList).Include(x => x.Driver).Include(x => x.PrimaryUser).OrderByDescending(x => x.EndTime).ToList();
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

        public double GetEstimatedFare(int rideTypeId, double distance, int? promocodeId)
        {
            try
            {
                RideType rideType = _dbContext.RideTypes.FirstOrDefault(x => x.Id == rideTypeId);
                Settings settings = _dbContext.Settings.FirstOrDefault();
                double estimatedFare = rideType.BasicCharges + (distance * rideType.FarePerKM);
                if (promocodeId.HasValue)
                {
                    Promocode code = _dbContext.Promocodes.FirstOrDefault(x => x.Id == promocodeId);
                    if (code.Type == PromocodeType.FixedDiscount)
                    {
                        estimatedFare = estimatedFare - code.Discount;
                    }
                    else if (code.Type == PromocodeType.DiscountPercentage)
                    {
                        estimatedFare = estimatedFare * (code.Discount / 100);
                    }
                }
                return estimatedFare;
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return 0;
            }
        }

        public TripDTO GetRideById(int id)
        {
            try
            {
                Trip trip = _dbContext.Trips.Include(x => x.PrimaryUser).Include(x => x.Driver).ThenInclude(x => x.Vehicles)
                    .FirstOrDefault(x => x.Id == id);
                TripDTO tripResponse = Mapper.Map<Trip, TripDTO>(trip);
                return tripResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TripDTO> GetPastRidesByDriverId(int id, CultureType culture)
        {
            try
            {
                List<Trip> trips = _dbContext.Trips.Include(x => x.PrimaryUser).Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList)
                    .Where(x => x.Status == TripStatus.Completed && x.Driver_Id.Value == id && x.EndTime <= DateTime.UtcNow).OrderByDescending(x => x.EndTime).ToList();
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

        public List<TripDTO> GetFutureRidesByDriverId(int id, CultureType culture)
        {
            try
            {
                List<Trip> trips = _dbContext.Trips.Include(x => x.PrimaryUser).Include(d => d.RideType).Include(d => d.RideType.RideTypeMLsList)
                    .Where(x => x.Status == TripStatus.AssignedToDriver && x.Driver_Id.Value == id && x.EndTime <= DateTime.UtcNow).ToList();
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

        public bool UsePromocode(int? promocodeId, int userId)
        {
            try
            {
                Promocode code = _dbContext.Promocodes.FirstOrDefault(x => x.Id == promocodeId && x.IsDeleted == false && (x.User_Id == null || x.User_Id == userId));
                if (code == null)
                {
                    return false;
                }

                if (code.User_Id == userId)
                {
                    code.IsDeleted = true;
                }
                else
                {
                    _dbContext.UserPromocode.Add(new UserCode { Promocode_Id = userId, User_Id = userId });
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



        public bool CheckCityStatus(string cityName)
        {
            try
            {
                CityML city = _dbContext.CityMLs.FirstOrDefault(x => x.Name == cityName && x.City.IsActive && x.IsDeleted == false);
                if (city == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ManagePromocode(int? promocode_Id)
        {
            try
            {
                Promocode code = _dbContext.Promocodes.FirstOrDefault(x => x.Id == promocode_Id.Value);
                if (code != null)
                {
                    code.UsageCount++;
                    if (code.LimitOfUsage <= code.UsageCount || code.ExpiryDate.Date <= DateTime.UtcNow.Date)
                    {
                        code.IsExpired = true;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> CloseRide(CloseRideBindingModel model)
        {
            try
            {
                var settings = _dbContext.Settings.FirstOrDefault();
                Trip trip = _dbContext.Trips.Include(x => x.Driver).ThenInclude(x=>x.CashSubscriptions).Include(s => s.PrimaryUser).FirstOrDefault(x => x.Id == model.RideId);
                if (trip.PrimaryUser.PrefferedPaymentMethod == PaymentMethods.CreditCard && model.CollectCash > 0)
                {
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
                            var payload = new MakePaypalPaymentModel();
                            PaypalCreditCardModel _newModel = new PaypalCreditCardModel { credit_card_id = trip.PrimaryUser.ActiveCardId, external_customer_id = "CUSTOMER_" + trip.PrimaryUser.Id.ToString() };
                            PaypalFundingModel paypalFunding = new PaypalFundingModel { credit_card_token = _newModel };
                            PaypalTransactionInputModel paypalTransaction = new PaypalTransactionInputModel();
                            payload.payer.funding_instruments.Add(paypalFunding);
                            payload.transactions.Add(paypalTransaction);
                            var stringPayload = JsonConvert.SerializeObject(payload);
                            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                            using (var httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.access_token);
                                var httpResponse = await httpClient.PostAsync(AppSettingsProvider.PaypalAPIMakePaymentUrl, httpContent);
                                if (httpResponse.Content != null)
                                {
                                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                                    MakePaymentResponseModel responseModel = JsonConvert.DeserializeObject<MakePaymentResponseModel>(result);
                                    if (httpResponse.StatusCode == HttpStatusCode.Created)
                                    {
                                        double convertedAmount = model.CollectCash * settings.CurrencyToUSDRatio;

                                        PaymentHistory _payment = new PaymentHistory { CreatedDate = Convert.ToDateTime(responseModel.create_time), ModifiedDate = Convert.ToDateTime(responseModel.update_time), TransactionId = responseModel.id, USDAmount = convertedAmount, LocalCurrencyAmount = model.CollectCash, User_Id = trip.PrimaryUser.Id };
                                        _dbContext.PaymentHistories.Add(_payment);
                                        trip.Driver.Wallet += model.CollectCash;
                                        _dbContext.SaveChanges();
                                        return HttpStatusCode.OK;
                                    }
                                }
                            }
                        }
                    }

                    return null;
                }
                else
                {
                    if (model.CollectCash > (trip.Fare - trip.WalletDeduction))
                    {
                        
                        trip.PrimaryUser.Wallet += model.CollectCash - (trip.Fare - trip.WalletDeduction);
                    }
                    else if (model.CollectCash < (trip.Fare - trip.WalletDeduction))
                    {
                        trip.PrimaryUser.Wallet -= model.CollectCash - (trip.Fare - trip.WalletDeduction);
                        trip.Driver.Wallet += (trip.Fare - trip.WalletDeduction) - model.CollectCash;
                    }
                    //trip.Driver.CashSubscriptions = trip.Driver.CashSubscriptions.Where(x => x.isActive).ToList();
                    var pkg= trip.Driver.CashSubscriptions.LastOrDefault(x=>x.isActive==true && x.Status==TopUpStatus.Accepted && x.ExpiryDate>DateTime.UtcNow);
                    if (pkg != null && pkg.RemainingRides > 0 && trip.PrimaryUser.PrefferedPaymentMethod==PaymentMethods.Cash)
                        pkg.RemainingRides--;
                    _dbContext.SaveChanges();
                    return HttpStatusCode.OK;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GetScheduledRideLaterRides()
        {
            try
            {
                List<Trip> tripsToRemind = _dbContext.Trips.Where(x => x.Status == TripStatus.Requested && x.IsDeleted == false && x.isScheduled == true && Convert.ToInt32((x.RequestTime - DateTime.UtcNow).TotalMinutes) == 5).ToList();
                foreach (var item in tripsToRemind)
                {
                    Notification notf = new Notification();
                    var targetedDevices = _dbContext.UserDevices.Where(x => x.IsDeleted == false && x.User_Id == item.PrimaryUser_Id).ToList();
                    PushNotificationsHelper.SendAndroidPushNotifications(Message: "You have a scheduled ride that is going to start after 5 minutes.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == true & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    PushNotificationsHelper.SendIOSPushNotifications(Message: "You have a scheduled ride that is going to start after 5 minutes.", Title: "Korsa", DeviceTokens: targetedDevices.Where(x1 => x1.Platform == false & x1.IsActive == true).Select(a => a.AuthToken).ToList());
                    _dbContext.Notifications.Add(notf);
                }

                List<Trip> tripsToStart = _dbContext.Trips.Include(x => x.PrimaryUser).Where(x => x.Status == TripStatus.Requested && x.IsDeleted == false && x.isScheduled == true && Convert.ToInt32((x.RequestTime - DateTime.UtcNow).TotalMinutes) >=0).ToList();
                foreach (var item in tripsToStart)
                {
                    Loc location = Mapper.Map<Location, Loc>(item.PickupLocation);
                    string gender = "1";
                    bool isCash = false;
                    if(item.PrimaryUser.Gender==Gender.Female)
                    {
                        gender = (item.PrimaryUser.DriverPreference == Gender.Female) ? "0" : "2";
                    }
                    if (item.PrimaryUser.PrefferedPaymentMethod == PaymentMethods.Cash)
                    {
                        isCash = (item.PrimaryUser.PrefferedPaymentMethod == PaymentMethods.Cash) ? true : false;
                    }
                    IUserModel userdata = new IUserModel { userId = item.PrimaryUser_Id.Value, userName = item.PrimaryUser.UserName, rating = item.PrimaryUser.Rating, userType = 0 };
                    ILocationModel payload = new ILocationModel { latitude = item.PickupLocation.Latitude, longitude = item.PickupLocation.Longitude, dropoflatitude = item.DestinationLocation.Latitude, dropoflongitude = item.DestinationLocation.Longitude, channel = "io:startridenow", locationType = 2, orderid = item.Id, direction = 0, distance = 1000, numberofdrivers = 10000, price = item.EstimatedFare, pickupLocationTitle = item.PickupLocationName, dropofLocationTitle = item.DestinationLocationName, vehicalType = item.RideType_Id , gender= gender, isCash=isCash };
                    payload.userData = userdata;
                    string json = JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(json);
                    using (var httpClient = new HttpClient())
                    {
                        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var httpResponse = httpClient.PostAsync(AppSettingsProvider.RideLaterService, stringContent).Result;

                        if (httpResponse.Content != null)
                        {
                            var responseContent = await httpResponse.Content.ReadAsStringAsync();
                            if (httpResponse.IsSuccessStatusCode)
                                _dbContext.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}

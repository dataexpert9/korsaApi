using AppModel.BindingModels;
using AppModel.CustomModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Utility;
using static Services.Global;

namespace LIMO.Controllers
{
    [Route("/api/Ride")]
    public class RideController : Controller
    {
        #region Properties and constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBORide _bORide;
        protected readonly IBOUser _bOUser;
        protected readonly IBODriver _bODriver;
        private readonly IHostingEnvironment _environment;

        public RideController(DataContext dataContext, IConfiguration configuration, IBODriver bODriver, IBOUser bOUser, IBORide bORide, IHostingEnvironment environment)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bORide = bORide;
            _environment = environment;
            _bODriver = bODriver;
        }
        #endregion

        [Route("GetAllRideTypes")]
        [HttpGet]
        public IActionResult GetAllRideTypes(int estimatedTime, double distanceInKm, string promocode, string PickupCity, string DestinationCity, PaymentMethods paymentType, int? userId)
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                int promocodeId = 0;
                List<RideTypeDTO> rideTypes = _bORide.GetAllRideTypes(culture, estimatedTime, distanceInKm, promocode, PickupCity, DestinationCity, paymentType,userId, out promocodeId);
                if (rideTypes == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Sorry we cannot initiate your ride for this location" } });
                }

                return Ok(new CustomResponse<RideTypeDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new RideTypeDTOList { RideTypeList = rideTypes, PromocodeId = promocodeId } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [Route("GetUpcomingRides")]
        [HttpGet]
        public IActionResult GetUpcomingRides()
        {
            try
            {

                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                List<TripDTO> rides = _bORide.GetUpcomingRidesByUserId(userId, culture);
                if (rides == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                }

                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new TripDTOList { Rides = rides } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]
        [Route("GetPastRides")]
        [HttpGet]
        public IActionResult GetPastRides()
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<TripDTO> rides = _bORide.GetPastRidesByUserId(userId, culture);
                if (rides == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                }

                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new TripDTOList { Rides = rides } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [HttpGet]
        [Route("StartCRONJobs")]
        public IActionResult StartCRONJobs()
        {
            try
            {
                RecurringJob.AddOrUpdate(
              () => RunRideLaterService(),
              Cron.Minutely);
                return Redirect(AppSettingsProvider.APIBaseURL+"swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpGet]
        [Route("RunRideLaterService")]
        public async Task<IActionResult> RunRideLaterService()
        {
            try
            {
                #region Response
                if (! await _bORide.GetScheduledRideLaterRides())
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Ride Later Background Service is not Working" } });

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Success" });

                #endregion

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return StatusCode(Error.LogError(ex));
            }
        }





        [Authorize]
        [Route("GetRideById")]
        [HttpGet]
        public IActionResult GetRideById(int rideId)
        {
            try
            {
                TripDTO ride = _bORide.GetRideById(rideId);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                }

                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = ride });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [Authorize]//Driver
        [Route("GetDriversCompletedRides")]
        [HttpGet]
        public IActionResult GetDriversCompletedRides()
        {
            try
            {
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<TripDTO> rides = _bORide.GetPastRidesByDriverId(driverId, culture);
                if (rides == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                }

                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new TripDTOList { Rides = rides } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [Authorize]//Driver
        [Route("GetDriversPendingRides")]
        [HttpGet]
        public IActionResult GetDriversPendingRides()
        {
            try
            {
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<TripDTO> rides = _bORide.GetFutureRidesByDriverId(driverId, culture);
                if (rides == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                }

                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new TripDTOList { Rides = rides } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        //[Authorize]
        //[Route("GetNearbyDrivers")]
        //[HttpGet]
        //public IActionResult GetNearbyDrivers()
        //{
        //    try
        //    {

        //        List<DriverDTO> driverDTOList = _bORide.GetNearByDrivers(location: new Loc(), rideId:0,rideTypId:null);
        //        if (driverDTOList == null)
        //            return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "No driver is available in your nearby area" } });
        //        return Ok(new CustomResponse<DriverDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new DriverDTOList { Drivers = driverDTOList } });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }

        //}



        [Authorize]
        [Route("GetMessages")]
        [HttpGet]
        public IActionResult GetMessages(GetMessagesBindingModel model)
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                List<MessageDTO> messagesList = (model.isUser) ? _bORide.GetMessages(id, model.Id) : _bORide.GetMessages(model.Id, id);
                if (messagesList == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "No message found" } });
                }

                return Ok(new CustomResponse<MessageDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new MessageDTOList { Messages = messagesList } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [Authorize]//Both User and Driver
        [Route("ChangeRideStatus")]
        [HttpPost]
        public IActionResult ChangeRideStatus(ChangeRideStatusBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion
                Trip ride = _bORide.GetTripById(model.Ride_Id);
                Notification notification = new Notification { Title = "KORSA" };

                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid Ride" } });
                }

                ride.Status = model.Status;
              
                
                if (!_bORide.UpdateTrip(ride))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to rate ride") } });
                }

                switch (model.Status)
                {
                    case TripStatus.Arrived:
                        notification.Text = "Rider has arrived";
                        notification.Entity_Id = ride.Id;
                        notification.User_Id = ride.PrimaryUser_Id.Value;
                        notification.Type = (int)PushNotificationType.DriverHasArrived;

                        break;
                    case TripStatus.Started:
                        if (ride.Promocode_Id > 0)
                        {
                           if( !_bORide.ManagePromocode(ride.Promocode_Id))
                                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Promocode can't be expired" } });
                        }
                        notification.Text = "Ride has started";
                        notification.User_Id = ride.PrimaryUser_Id.Value;
                        notification.Type = (int)PushNotificationType.RideStarted;
                        notification.Entity_Id = ride.Id;
                        break;
                    case TripStatus.Completed:
                        notification.Text = "Your ride has been completed";
                        notification.User_Id = ride.PrimaryUser_Id.Value;
                        notification.Type = (int)PushNotificationType.RideCompleted;
                        notification.Entity_Id = ride.Id;
                        break;
                }
                _bORide.SendPushToUser(ride.PrimaryUser_Id.Value, ride.Driver_Id.Value, notification, (PushNotificationType)notification.Type, AppSettingsProvider.FCMServerKeyUserApp);//Sends Push To Ride User
                TripDTO tripDTO = Mapper.Map<Trip, TripDTO>(ride);
                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTO });


            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [Route("SendMessage")]
        [HttpPost]
        public IActionResult SendMessage(MessageBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                #region MessageSaving
                if (model.isUserSender)
                {
                    model.User_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                }
                else
                {
                    model.Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                }

                Message msg = Mapper.Map<MessageBindingModel, Message>(model);
                MessageDTO response = _bORide.InsertMessage(msg);
                #endregion

                #region Response
                if (response == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Message sending failed." } });
                }

                return Ok(new CustomResponse<MessageDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }






        [Authorize]
        [HttpPost]
        [Route("GetEstimatedFare")]
        public IActionResult GetEstimatedFare(EstimateFareBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                double estimatedFare = _bORide.GetEstimatedFare(model.RideType_Id, model.DistanceInKm, model.Promocode_Id);
                return Ok(new CustomResponse<EstimatedFareDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new EstimatedFareDTO { EstimatedFare = estimatedFare } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        //[HttpGet]
        //[Route("TestHangFire")]
        //public IActionResult Dosomething()
        //{
        //    try
        //    {
        //      //  RecurringJob.AddOrUpdate(
        //      //() => GetRideLater(),
        //      //Cron.Minutely);
        //        return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Executing" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}


            

        [Authorize]
        [Route("RideNow")]
        [HttpPost]
        public async Task<IActionResult> RideNow(RideNowBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                #region RideSaving
                Trip ride = new Trip { PrimaryUser_Id = Convert.ToInt32(User.GetClaimValue("Id")), RequestTime = DateTime.UtcNow, Status = TripStatus.Requested };
                Mapper.Map(model, ride);
                ride.ImageUrl = await ImageHelper.SaveImageUrlFromGoogleMap(Directory.GetCurrentDirectory(), AppSettingsProvider.RideImageFolderPath, ride.PickupLocation.Longitude.ToString(), ride.PickupLocation.Latitude.ToString(), ride.DestinationLocation.Longitude.ToString(), ride.DestinationLocation.Latitude.ToString(), AppSettingsProvider.GoogleMapsAPIKey);
                // if(!_bORide.CheckCityStatus(model.PickupCity))
                //    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Sorry Pickup from mentioned city is not possible." } });
                //if (!_bORide.CheckCityStatus(model.DestinationCity))
                //    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Sorry dropoff in mentioned city is not possible." } });

                ride.EstimatedFare = model.EstimatedFare;
                ride = _bORide.InsertTrip(ride);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to start ride" } });
                }
                #endregion

                #region Response
                Loc location = Mapper.Map<Locations, Loc>(model.PickupLocation);
                List<DriverDTO> driverDTOList = _bORide.GetNearByDrivers(location, ride.Id, rideTypId: null, key: AppSettingsProvider.FCMServerKeyDriverApp);
                if (driverDTOList == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "No driver is available in your nearby area" } });
                }

                return Ok(new CustomResponse<DriverDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new DriverDTOList { Drivers = driverDTOList, RideId = ride.Id } });
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [Route("RideLater")]
        [HttpPost]
        public async Task<IActionResult> RideLater(RideLaterBindingModel model)
        {
            try
            {
                #region ImageValidationAndSaving
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                #endregion

                #region RideSaving
                Trip ride = new Trip { PrimaryUser_Id = Convert.ToInt32(User.GetClaimValue("Id")), RequestTime = model.StartTime, Status = TripStatus.Requested, isScheduled = true };
                Mapper.Map(model, ride);
                ride.ImageUrl = await ImageHelper.SaveImageUrlFromGoogleMap(Directory.GetCurrentDirectory(), AppSettingsProvider.RideImageFolderPath, ride.PickupLocation.Longitude.ToString(), ride.PickupLocation.Latitude.ToString(), ride.DestinationLocation.Longitude.ToString(), ride.DestinationLocation.Latitude.ToString(), AppSettingsProvider.GoogleMapsAPIKey);
                ride.EstimatedFare = model.EstimatedFare;
                ride = _bORide.InsertTrip(ride);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to start ride" } });
                }
                #endregion

                #region Response
                //SendNearByDriversRequestForRide
                List<TripDTO> rides = _bORide.GetUpcomingRidesByUserId(ride.PrimaryUser_Id.Value, culture);

                if (rides == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides types.") } });
                }

                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new TripDTOList { Rides = rides } });

                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        /// <summary>
        /// Access it from Driver
        /// </summary>
        [Authorize] //Driver Id Will be obtained from token
        [Route("AssignRideToDriver")]
        [HttpPost]
        public IActionResult AssignRideToDriver(DriverRideAssigningModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                Trip ride = _bORide.GetTripById(model.Ride_Id);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid Ride" } });
                }

                if (ride.Status == TripStatus.AssignedToDriver)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Ride has been assigned to another driver" } });
                }

                ride.Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                ride.StartTime = DateTime.UtcNow;
                ride.Status = TripStatus.AssignedToDriver;
                if (!_bORide.UpdateTrip(ride))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to assign ride") } });
                }

                Notification notification = new Notification
                {
                    Title = "KORSA",
                    Text = "Rider has accepted your ride.",
                    Entity_Id = ride.Driver_Id,
                    Type = (int)PushNotificationType.RideAssignedToRider,
                    User_Id = ride.PrimaryUser_Id.Value
                };
                PushNotificationType type = PushNotificationType.RideAssignedToRider;
                _bORide.SendPushToUser(ride.PrimaryUser_Id.Value, ride.Driver_Id.Value, notification, type, key: AppSettingsProvider.FCMServerKeyUserApp);//Sends Push To Ride User
                TripDTO tripDTO = Mapper.Map<Trip, TripDTO>(ride);
                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTO });


            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]//Driver
        [Route("EndRide")]
        [HttpPost]
        public IActionResult EndRide(FinishRideBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                Trip ride = _bORide.GetTripByIdWithDriverAndUser(model.RideId);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid Ride" } });
                }


                if (!_bORide.EndRide(model))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to rate ride") } });
                }

                Notification notification = new Notification
                {
                    Title = "KORSA",
                    Text = "Rider has ended your ride.",
                    Entity_Id = ride.Id,
                    User_Id = ride.PrimaryUser_Id.Value,
                    Type = (int)PushNotificationType.RideCompleted
                };
                _bORide.SendPushToUser(ride.PrimaryUser_Id.Value, ride.Driver_Id.Value, notification, (PushNotificationType)notification.Type, key: AppSettingsProvider.FCMServerKeyUserApp);//Sends Push To Ride User
                TripDTO tripDTO = Mapper.Map<Trip, TripDTO>(ride);
                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTO });


            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]//Driver
        [Route("CloseRide")]
        [HttpPost]
        public async Task<IActionResult> CloseRide(CloseRideBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                //Notification notification = new Notification
                //{
                //    Title = "KORSA",
                //    Text = "Rider " + ride.Driver.Username.ToUpper() + " has taken LD " + model.CollectCash + " for ride",
                //    Entity_Id = ride.Id,
                //    User_Id = ride.PrimaryUser_Id.Value,
                //    Type = (int)PushNotificationType.RideClosed
                //};

                var response = await _bORide.CloseRide(model);
                
                switch (response)
                {
                    case HttpStatusCode.Conflict:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Transaction already made." } });

                    case HttpStatusCode.Unauthorized:
                        return Ok(new CustomResponse<Error> { Message = "Unauthorized", StatusCode = StatusCodes.Status401Unauthorized, Result = new Error { ErrorMessage = "Invalid paypal credentials" } });

                    case HttpStatusCode.OK:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Successfully done" } });

                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid Response" } });

                }                
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [Route("RateRide")]
        [HttpPost]
        public IActionResult RateRide(RateRideBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion
                Trip ride = _bORide.GetTripByIdWithDriverAndUser(model.Ride_Id);
                if (ride == null /*|| ride.PrimaryUser_Id != Convert.ToInt32(User.GetClaimValue("Id"))*/)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid ride" } });
                }

                if (ride.Status != TripStatus.Completed)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Ride must be completed before rating it" } });
                }

                if (!model.isDriver)//User is Rating Ride
                {
                    if (ride.DriverRating != 0)
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Ride already rated" } });
                    }

                    ride.DriverRating = model.Rating;
                    ride.FeedbackForDriver = model.FeedBack;
                    ride.Driver.Rating = (ride.Driver.RatedRidesCount == 0) ? model.Rating : ((ride.Driver.Rating * ride.Driver.RatedRidesCount) + model.Rating) / ride.Driver.RatedRidesCount++;
                }
                else//Driver is Rating Ride
                {
                    if (ride.UserRating != 0)
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Ride already rated" } });
                    }

                    ride.UserRating = model.Rating;
                    ride.FeedbackForUser = model.FeedBack;
                    ride.PrimaryUser.Rating = (ride.PrimaryUser.RatedRidesCount == 0) ? model.Rating : ((ride.PrimaryUser.Rating * ride.PrimaryUser.RatedRidesCount) + model.Rating) / ride.PrimaryUser.RatedRidesCount++;
                }

                ride.ModifiedDate = DateTime.UtcNow;
                if (!_bORide.UpdateTrip(ride))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to rate ride") } });
                }

                TripDTO tripDTO = Mapper.Map<Trip, TripDTO>(ride);
                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]//User and driver
        [Route("CancelRide")]
        [HttpPost]
        public async Task<IActionResult> CancelRide(CancelRideBindingModel model)
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #endregion

                Trip ride = _bORide.GetTripById(model.RideId);
                if (ride == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid Ride" } });
                }

                ride.CancellationReason_Id = model.CancellationReasonId;
                ride.ReasonToCancel = model.ReasonToCancel;
                ride.EndTime = DateTime.UtcNow;
                //if User cancels ride
                if (!model.isDriverCancelling)
                {
                    ride.Status = TripStatus.CancelledByUser;
                    if (!_bORide.UpdateTrip(ride))
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to cancel ride") } });
                    }

                    if (ride.Driver_Id != null)
                    {
                        Notification notification = new Notification
                        {
                            Title = "KORSA",
                            Text = "Your ride has been cancelled.",
                            Entity_Id = ride.Id,
                            Type = (int)PushNotificationType.RideCancelled,
                            Driver_Id = ride.Driver_Id.Value,
                        };
                        _bORide.SendPushToDriver(ride.PrimaryUser_Id.Value, ride.Driver_Id.Value, notification, PushNotificationType.RideCancelled, AppSettingsProvider.FCMServerKeyDriverApp);//Sends Push To Ride Driver
                    }
                    TripDTO tripDTO = Mapper.Map<Trip, TripDTO>(ride);
                    return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTO });
                }

                //if driver cancels ride
                ride.Status = TripStatus.CancelledByDriver;
                if (!_bORide.UpdateTrip(ride))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to cancel ride") } });
                }

                if (ride.Driver_Id != null)
                {
                    Notification notificationToUser = new Notification
                    {
                        Title = "KORSA",
                        Text = "Your ride has been cancelled.",
                        Entity_Id = ride.Id,
                        Type = (int)PushNotificationType.RideCancelled,
                        User_Id = ride.PrimaryUser_Id.Value,
                    };
                    _bORide.SendPushToUser(ride.PrimaryUser_Id.Value, ride.Driver_Id.Value, notificationToUser, PushNotificationType.RideCancelled, AppSettingsProvider.FCMServerKeyUserApp);//Sends Push To Ride User
                }
                TripDTO tripDTOUser = Mapper.Map<Trip, TripDTO>(ride);
                return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = tripDTOUser });



            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

    }
}
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interface;
using AutoMapper;
using Wasalee.JwtHelpers;
using Microsoft.Extensions.Configuration;
using Component.ResponseFormats;
using Component.Utility;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using AppModel.DTOs;
using AppModel.BindingModels;
using DAL.DomainModels;
using Component;
using System.Collections.Generic;
using Utility;
using static Utility.ImageHelper;
using System.Threading.Tasks;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        #region Properties and Constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBODriver _bODriver;

        public DriverController(DataContext dataContext, IConfiguration configuration, IBODriver bODriver)
        {
            _configuration = configuration;
            _dbContext = dataContext;
            _bODriver = bODriver;
        }
        #endregion
        [HttpPost]
        [Route("RegisterAsDriver")]
        public IActionResult Register(RegisterDriverBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Checked Here Coz In Case it exists un necessary photos won't be saved
                if (_bODriver.Exists(model.Username, model.PhoneNo))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Username or mobile number") } });

                #region ProfilePic
                string ImagePath = "";
                ImageSavingEnum responseCode = 0;
                if (model.ProfilePicture != null)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.ProfilePicture, out ImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                }
                #endregion

                #region DrivingLicenseFront
                string LicenseFrontImagePath = "";
                List<string> LicenseFrontUrls = new List<string>();
                foreach (var item in model.DrivingLicenseFrontImages)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseFrontImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    LicenseFrontUrls.Add(LicenseFrontImagePath);
                }
                #endregion

                #region DrivingLicenseBack
                string LicenseBackImagePath = "";
                List<string> LicenseBackUrls = new List<string>();
                foreach (var item in model.DrivingLicenseBackImages)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseBackImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    LicenseBackUrls.Add(LicenseBackImagePath);
                }
                #endregion

                #region RegistrationCopyImages
                string RegistrationCopyImagePath = "";
                List<string> RegistrationCopyUrls = new List<string>();
                foreach (var item in model.RegistrationCopyImages)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.RegistrationCopyImageFolderPath, item, out RegistrationCopyImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    RegistrationCopyUrls.Add(RegistrationCopyImagePath);
                }
                #endregion

                #region CarPhotos
                string CarImagePath = "";
                List<string> CarImagesUrls = new List<string>();

                foreach (var item in model.CarPhotos)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.CarImageFolderPath, item, out CarImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    CarImagesUrls.Add(CarImagePath);
                }
                #endregion

                #region DriverSaving
                model.Password = CryptoHelper.Hash(model.Password);
                DriverDTO driverDTO = _bODriver.AddDriver(model, ImagePath, LicenseFrontUrls, LicenseBackUrls, CarImagesUrls, RegistrationCopyUrls);
                if (driverDTO == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Input.Something is wrong with request") } });
                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
                #endregion




            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("AddEditVehicle")]
        public IActionResult AddEditVehicle(VehicleBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                ImageSavingEnum responseCode = 0;
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));

                #region RegistrationCopyImages
                string RegistrationCopyImagePath = "";
                List<string> RegistrationCopyUrls = new List<string>();
                if(model.RegistrationCopyImages!=null)
                {
                    foreach (var item in model.RegistrationCopyImages)
                    {
                        responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.RegistrationCopyImageFolderPath, item, out RegistrationCopyImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        RegistrationCopyUrls.Add(RegistrationCopyImagePath);
                    }
                }
                #endregion

                #region CarPhotos
                string CarImagePath = "";
                List<string> CarImagesUrls = new List<string>();
                if (model.RegistrationCopyImages != null)
                {
                    foreach (var item in model.CarPhotos)
                    {
                        responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.CarImageFolderPath, item, out CarImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        CarImagesUrls.Add(CarImagePath);
                    }
                }
                #endregion

                #region VehicleSaving
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                VehicleDTO vehicleDTO = _bODriver.AddEditVehicle(driverId, model,CarImagesUrls, RegistrationCopyUrls);
                if (vehicleDTO == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Input.Something is wrong with request") } });
                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });

                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                foreach (var item in driverDTO.CashSubscriptions)
                    item.Driver.CashSubscriptions = null;
                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });

                #endregion




            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("LoginAsDriver")]
        [HttpPost]
        public IActionResult Login(LoginBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                DriverDTO driver = _bODriver.AuthenticateCredentials(model.CellNumOrUsername, CryptoHelper.Hash(model.Password));
                if (driver != null)
                {
                    driver.AppSettings = _bODriver.GetSettings();
                    driver.GenerateToken(_configuration);
                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driver });
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username/phone number or password") } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [Route("GetDriver")]
        [HttpGet]
        public IActionResult GetDriver()
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                DriverDTO driver = _bODriver.GetDriverFromToken(id);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get driver" } });
                driver.AppSettings = _bODriver.GetSettings();
                driver.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driver });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Route("GetDriversbyIds")]
        [HttpGet]
        public async Task<IActionResult> GetDriversbyIds(string driverIds, string searchString="")
        {
            try
            {
                DriverDTOList returnModel = new DriverDTOList();

                var Drivers = _bODriver.GetDriversbyIds(driverIds,searchString);

                if (Drivers != null)
                {
                    Mapper.Map(Drivers, returnModel.Drivers);
                }

                return Ok(new CustomResponse<DriverDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddBankCashSubscription")]
        public IActionResult AddBankCashSubscription(AddBankCashSbuscriptionBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                int Id = Convert.ToInt32(User.GetClaimValue("Id"));

                #region DrivingLicenseFront
                string TopupReceiptsPath = "";
                ImageSavingEnum responseCode = 0;

                List<string> TopUpReceiptUrls = new List<string>();
                foreach (var item in model.Receipts)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.TopupReceiptsImageFolderPath, item, out TopupReceiptsPath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    TopUpReceiptUrls.Add(TopupReceiptsPath);
                }
                #endregion
                if (!_bODriver.AddBankCashSubscription(Id, TopUpReceiptUrls, model))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to add cash subscription" } });


                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Cash subscription is done successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("ChangeDriverNotificationStatus")]
        public IActionResult ChangeNotificationStatus(bool isOn)
        {
            try
            {

                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });

                driver.IsNotificationsOn = isOn;

                if (!_bODriver.UpdateDriver(driver))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("ChangeOnlineStatus")]
        public IActionResult ChangeOnlineStatus(bool isOnline)
        {
            try
            {

                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });

                driver.LoginStatus = (isOnline)? LoginStatus.Online: LoginStatus.Offline;

                if(!_bODriver.LogDriver(driverId, isOnline))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });

                if (!_bODriver.UpdateDriver(driver))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                foreach (var item in driverDTO.CashSubscriptions)
                {
                    item.Driver = null;
                }
                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpGet]
        [Route("VerifyUserName")]
        public IActionResult VerifyUserName(string username, UserTypes userType)
        {
            try
            {
                if (!_bODriver.VerifyUserName(username, userType))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username") } });
                return Ok(new CustomResponse<bool> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("UpdateLocation")]
        [HttpPost]
        public IActionResult UpdateLocation(LocationBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int Id = Convert.ToInt32(User.GetClaimValue("Id"));
            Driver driver = _bODriver.GetDriverById(Id);
            if (driver == null)
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "You are an authorized driver" } });
            driver.Location.Latitude = model.Latitude;
            driver.Location.Longitude = model.Longitude;
            if (!_bODriver.UpdateDriver(driver))
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to update driver" } });

            #region Response
            DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

            driverDTO.AppSettings = _bODriver.GetSettings();
            driverDTO.GenerateToken(_configuration);
            return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
            #endregion

        }

        [Authorize]
        [HttpPost]
        [Route("UpdatePassword")]
        public IActionResult UpdatePassword(UpdatePasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });
                if (driver.Password != CryptoHelper.Hash(model.Password))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Old Password") } });


                driver.Password = CryptoHelper.Hash(model.NewPassword);
                driver.ModifiedDate = DateTime.UtcNow;
                if (!_bODriver.UpdateDriver(driver))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });
                DriverDTO driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [Route("GetVehicleById")]
        [HttpGet]
        public IActionResult GetVehicleById(int id)
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                VehicleDTO vehicle = _bODriver.GetVehicleById(id, culture);
                if (vehicle == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });

                return Ok(new CustomResponse<VehicleDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = vehicle });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [Route("GetVehiclesByDriverId")]
        [HttpGet]
        public IActionResult GetVehiclesByDriverId()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));
                VehicleDTOList response = new VehicleDTOList();
                response.Vehicles = _bODriver.GetVehiclesByDriverId(driverId, culture);
                if (response.Vehicles == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get vehicles." } });

                return Ok(new CustomResponse<VehicleDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [HttpPost]
        [Route("SetForgotPassword")]
        public IActionResult SetForgotPassword(SetPasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Driver driver = _bODriver.GetDriver(model.CellNumOrUsername);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });

                driver.Password = CryptoHelper.Hash(model.NewPassword);
                driver.ModifiedDate = DateTime.UtcNow;
                if (!_bODriver.UpdateDriver(driver))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Password changed successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("SetActiveVehicle")]
        public IActionResult SetActiveVehicle(int VehicleId)
        {
            try
            {
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));         
                if (!_bODriver.SetActiveVehicle(driverId,VehicleId))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to set active vehicle" } });
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Vehicle set successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("EditDriverProfile")]
        public IActionResult EditDriverProfile(EditDriverProfileBindingModel model)
        {
            try
            {
                ImageSavingEnum responseCode = new ImageSavingEnum();
                int driverId = Convert.ToInt32(User.GetClaimValue("Id"));

                #region ValidationsAndImageSaving
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string ImagePath = "";
                if (model.ProfilePicture != null)
                {
                    responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), _configuration.GetValue<string>("UserImageFolderPath"), model.ProfilePicture, out ImagePath);
                }

                if (responseCode == ImageSavingEnum.InvalidExtension)
                    return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                #endregion


                #region DrivingLicenseFront
                string LicenseFrontImagePath = "";
                List<string> LicenseFrontUrls = new List<string>();
                if (model.DrivingLicenseFrontImages != null)
                {
                    foreach (var item in model.DrivingLicenseFrontImages)
                    {
                        responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseFrontImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        LicenseFrontUrls.Add(LicenseFrontImagePath);
                    }
                }
                #endregion

                #region DrivingLicenseBack
                string LicenseBackImagePath = "";
                List<string> LicenseBackUrls = new List<string>();
                if (model.DrivingLicenseBackImages != null)
                {
                    foreach (var item in model.DrivingLicenseBackImages)
                    {
                        responseCode = ImageHelper.SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseBackImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        LicenseBackUrls.Add(LicenseBackImagePath);
                    }
                }
                #endregion

                
                #region VehicleSaving
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                DriverDTO driverDTO = _bODriver.EditDriverProfile(driverId,ImagePath, model, LicenseFrontUrls, LicenseBackUrls);
                if (driverDTO == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Input.Something is wrong with request") } });

                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });

                 driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
             
                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
                #endregion

            }
            catch (Exception ex)
            {
                return Ok(new CustomResponse<Error> { Message = "IntenalServerError", StatusCode = StatusCodes.Status500InternalServerError, Result = new Error { ErrorMessage = ex.Message } });
                //return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetAllSubscriptionPackage")]
        public IActionResult GetAllSubscriptionPackages()
        {
            try
            {
                SubscriptionPackageDTOList response = new SubscriptionPackageDTOList();
                response.SubscriptionPackagesList = _bODriver.GetAllSubscriptionPackages();
                return Ok(new CustomResponse<SubscriptionPackageDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [HttpGet]
        [Route("GetAllBanks")]
        public IActionResult GetAllBanks()
        {
            try
            {
                BankDTOList response = new BankDTOList();
                response.Banks = _bODriver.GetAllBanks();
                return Ok(new CustomResponse<BankDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [HttpGet]
        [Route("GetAllCountries")]
        public IActionResult GetAllCountries(bool isAll = false)
        {
            try
            {
                CountryDTOList response = new CountryDTOList();
                response.Countries = _bODriver.GetAllCountries(isAll);
                return Ok(new CustomResponse<CountryDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [HttpGet]
        [Route("GetAllCities")]
        public IActionResult GetAllCities(bool isAll=false)
        {
            try
            {
                CitiesDTOList response = new CitiesDTOList();
                response.Cities = _bODriver.GetAllCities(isAll);
                return Ok(new CustomResponse<CitiesDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [HttpGet]
        [Route("GetAllBranches")]
        public IActionResult GetAllBranches()
        {
            try
            {
                BranchDTOList response = new BranchDTOList();
                response.Branches = _bODriver.GetAllBranches();
                return Ok(new CustomResponse<BranchDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [HttpGet]
        [Route("GetAllBranchesByBankId")]
        public IActionResult GetAllBranchesByBankId(int Id)
        {
            try
            {
                BranchDTOList response = new BranchDTOList();
                response.Branches = _bODriver.GetAllBranchesByBankId(Id);
                return Ok(new CustomResponse<BranchDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            try
            {
                AccountDTOList response = new AccountDTOList();
                response.Accounts = _bODriver.GetAllAccounts();
                return Ok(new CustomResponse<AccountDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [HttpGet]
        [Route("GetAllCitiesByCountryId")]
        public IActionResult GetAllCitiesByCountryId(int id, bool isAll=false)
        {
            try
            {
                CitiesDTOList response = new CitiesDTOList();
                response.Cities = _bODriver.GetAllCitiesByCountryId(id, isAll);
                return Ok(new CustomResponse<CitiesDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        //[HttpPost]
        //[Route("ResetDriverForgotPassword")]
        //public IActionResult ResetForgotPassword(ResetPasswordBindingModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        string code = Convert.ToString(new Random().Next(111111, 999999));
        //        Driver driver = _bODriver.ResetForgotPassword(model.Email, code);
        //        if (driver != null)
        //        {
        //            var callbackUrl = Url.Link("Default", new { Controller = "ResetPassword", Action = "ResetPassword", code = code });
        //            const string subject = "Reset your password - LIMO App";
        //            //const string body = "Use this code to reset your password";

        //            var smtp = new SmtpClient
        //            {
        //                Host = _configuration.GetValue<string>("EmailHost"),
        //                Port = Convert.ToInt32(_configuration.GetValue<string>("EmailPort")),
        //                EnableSsl = Convert.ToBoolean(_configuration.GetValue<string>("EmailSSL")),
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                UseDefaultCredentials = Convert.ToBoolean(_configuration.GetValue<string>("EmailUseDefaultCredentials")),
        //                Credentials = new NetworkCredential(_configuration.GetValue<string>("FromMailAddress"), _configuration.GetValue<string>("FromPassword"))
        //            };

        //            MailAddress FromMailAddress = new MailAddress(_configuration.GetValue<string>("FromMailAddress"), _configuration.GetValue<string>("FromMailName"));
        //            var message = new MailMessage(FromMailAddress, new MailAddress(model.Email))
        //            {
        //                Subject = subject,
        //                Body = "Hello, We received a request to reset your LIMO password.Kindly use this code to reset your password:" + code + " Thanks for using LIMO! LIMO Team"
        //            };

        //            smtp.Send(message);

        //            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = (int)HttpStatusCode.OK, Result = "Your password has been reset successfully!" });
        //        }
        //        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") } });

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}


        [Route("GetDriverById")]
        [HttpGet]
        public IActionResult GetDriverById(int id)
        {
            try
            {
                Driver driver = _bODriver.GetDriverById(id);
                DriverDTO driverDTO = new DriverDTO();
                Mapper.Map(driver, driverDTO);
                foreach (var item in driverDTO.CashSubscriptions)
                    item.Driver = null;
                driverDTO.AppSettings = _bODriver.GetSettings();
                driverDTO.GenerateToken(_configuration);
                if (driver == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to find driver.") } });

                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Route("GetAllCarMakers")]
        [HttpGet]
        public IActionResult GetAllCarMakers()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CarCompanyDTO> carCompanies = _bODriver.GetAllCarMakers(culture);
                if (carCompanies == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides companies.") } });

                return Ok(new CustomResponse<CarCompanyDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CarCompanyDTOList { CarCompanies = carCompanies } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Route("GetAllCarModels")]
        [HttpGet]
        public IActionResult GetAllCarModels()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CarModelDTO> carModels = _bODriver.GetAllCarModels(culture);
                if (carModels == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get car models.") } });

                return Ok(new CustomResponse<CarModelDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CarModelDTOList { CarModels = carModels } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Route("GetAllCarYears")]
        [HttpGet]
        public IActionResult GetAllCarYears()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CarYearDTO> carYears = _bODriver.GetAllCarYears(culture);
                if (carYears == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides years.") } });

                return Ok(new CustomResponse<CarYearDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CarYearDTOList { CarYears = carYears } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Route("GetAllCarTypes")]
        [HttpGet]
        public IActionResult GetAllCarTypes()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CarTypeDTO> carTypes = _bODriver.GetAllCarTypes(culture);
                if (carTypes == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get car types.") } });

                return Ok(new CustomResponse<CarTypeDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CarTypeDTOList { CarTypes = carTypes } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Route("GetAllCarCapacities")]
        [HttpGet]
        public IActionResult GetAllCarCapacities()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CarCapacityDTO> carCapacities = _bODriver.GetAllCarCapacities(culture);
                if (carCapacities == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get car types.") } });

                return Ok(new CustomResponse<CarCapacityDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CarCapacityDTOList { CarCapacities = carCapacities } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Route("GetVehicleAllDropDowns")]
        [HttpGet]
        public IActionResult GetVehicleAllDropDowns()
        {
            try
            {
                VehicleAllDropDownList response = new VehicleAllDropDownList();
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                response.CarCompanies = _bODriver.GetAllCarMakers(culture);
                if (response.CarCompanies == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get car companies." } });

                response.CarModels = _bODriver.GetAllCarModels(culture);
                if (response.CarModels == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get car models." } });

                response.CarYears = _bODriver.GetAllCarYears(culture);
                if (response.CarYears == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get car years." } });

                response.CarTypes = _bODriver.GetAllCarTypes(culture);
                if (response.CarTypes == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get car types." } });

                response.CarCapacities = _bODriver.GetAllCarCapacities(culture);
                if (response.CarCapacities == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get car seating capacities." } });

                return Ok(new CustomResponse<VehicleAllDropDownList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

    }
}
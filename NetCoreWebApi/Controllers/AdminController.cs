using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component;
using Component.ResponseFormats;
using Component.Utility;
using DAL.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utility;
using Wasalee.JwtHelpers;
using static Utility.ImageHelper;

namespace Korsa.Controllers
{
    [Route("/api/Admin")]
    public class AdminController : Controller
    {


        #region Properties and constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBOAdmin _bOAdmin;
        protected readonly IBOUser _bOUser;
        protected readonly IBODriver _bODriver;
        private readonly IHostingEnvironment _environment;

        public AdminController(DataContext dataContext, IConfiguration configuration, IBOUser bOUser, IBOAdmin bOAdmin, IBODriver bODriver, IHostingEnvironment environment)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOUser = bOUser;
            _bODriver = bODriver;
            _bOAdmin = bOAdmin;
            _environment = environment;
        }
        #endregion



        [HttpPost]
        [Route("TestAddImage")]
        public IActionResult TestAddImage([FromBody]ImageModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ImageSavingEnum responseCode = 0;
                string ImagePath = "";
                if (model != null)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model, out ImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }
                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Successfully saved!" });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [HttpPost]
        [Route("AddMapCode")]
        public IActionResult AddMapCode([FromBody]AddPromocodeBindingModel model)
        {
            try
            {
                var res = _bOAdmin.AddMapCode(model);
                if (res == null)
                {
                    return Ok(new CustomResponse<Error> { Message = "Conflict", StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Promocode already exists" } });
                }
                else
                {
                    return Ok(new CustomResponse<PromocodeDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = res });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpGet]
        [Route("GetCodes")]
        public IActionResult GetCodes()

        {
            try
            {
                PromocodeDTOList returnModel = new PromocodeDTOList();

                returnModel = _bOAdmin.GetCodes();
                if (returnModel == null)
                {
                    return Ok(new CustomResponse<Error> { Message = "InternalServerError", StatusCode = StatusCodes.Status500InternalServerError, Result = new Error { ErrorMessage = "Promocode not found" } });
                }

                return Ok(new CustomResponse<PromocodeDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [HttpPost]
        [Route("AddUpdateAdmin")]
        public IActionResult AddUpdateAdmin([FromBody]AdminBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ImageSavingEnum responseCode = 0;
                string ImagePath = "";
                if (model.Image != null)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.Image, out ImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }
                model.ImageUrl = ImagePath;
                Admin admin = _bOAdmin.AddUpdateAdmin(model);

                if (admin != null)
                {
                    AdminDTO adminDTO = Mapper.Map<Admin, AdminDTO>(admin);
                    adminDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<AdminDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = adminDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Admin") } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpPost]
        [Route("AddRole")]
        public IActionResult AddRole([FromBody]RoleScreenModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AddUpdateResponse response = _bOAdmin.AddUpdateRole(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Role added successfully!" });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Role updated successfully!" });

                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("EditDriverProfile")]
        public IActionResult EditDriverProfile([FromBody]EditDriverProfileBindingModelAdmin model)
        {
            try
            {
                ImageSavingEnum responseCode = new ImageSavingEnum();
                int driverId = model.Id;
                #region ValidationsAndImageSaving
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string ImagePath = "";
                if (model.ProfilePicture != null)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.ProfilePicture, out ImagePath);
                }

                if (responseCode == ImageSavingEnum.InvalidExtension)
                {
                    return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                }
                else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                {
                    return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                }
                #endregion


                #region DrivingLicenseFront
                string LicenseFrontImagePath = "";
                List<string> LicenseFrontUrls = new List<string>();
                if (model.DrivingLicenseFrontImages != null)
                {
                    foreach (var item in model.DrivingLicenseFrontImages)
                    {
                        responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseFrontImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                        {
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        }
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        {
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        }

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
                        responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseBackImagePath);
                        if (responseCode == ImageSavingEnum.InvalidExtension)
                        {
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                        }
                        else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        {
                            return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                        }

                        LicenseBackUrls.Add(LicenseBackImagePath);
                    }
                }
                #endregion


                #region VehicleSaving
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                DriverDTO driverDTO = _bODriver.EditDriverProfileAdmin(driverId, ImagePath, model, LicenseFrontUrls, LicenseBackUrls);
                if (driverDTO == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Input.Something is wrong with request") } });
                }

                Driver driver = _bODriver.GetDriverById(driverId);
                if (driver == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                }

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

        [HttpGet]
        [Route("SearchRoles")]
        public IActionResult SearchRoles()
        {
            try
            {
                SearchRoleListDTO response = _bOAdmin.SearchRoles();
                if (response == null)
                {
                    return Ok(new CustomResponse<Error> { Message = "InternalServerError", StatusCode = StatusCodes.Status500InternalServerError, Result = new Error { ErrorMessage = "Roles not found" } });
                }

                return Ok(new CustomResponse<SearchRoleListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpPost]
        [Route("AddUpdateUser")]
        public IActionResult AddUpdateUser([FromBody]AddUserBindingModel model)
        {
            try
            {
                #region ImageValidationAndSaving

                string ImagePath = "";

                if (model.ProfilePicture != null)
                {
                    ImageSavingEnum responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.ProfilePicture, out ImagePath);

                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }
                #endregion

                #region UserSaving
                UserDTO userDTO = null;
                if (model.Id > 0)
                {
                    if (_bOUser.EditUserExists(model.UserName, model.PhoneNo, model.UniqueId))
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Username or Phone Number") } });
                    }

                    User user = null;
                    if (model.ProfilePicture == null)
                    {
                        user = new User { SignInType = (int)SignInType.ThroughApp, Rating = 0, IsNotificationsOn = true };
                    }
                    else
                    {
                        user = new User { SignInType = (int)SignInType.ThroughApp, ProfilePictureUrl = ImagePath, Rating = 0, IsNotificationsOn = true };
                    }
                    model.Password = CryptoHelper.Hash(model.Password);
                    Mapper.Map(model, user);
                    bool resultUpdated = _bOUser.UpdateUser(user);
                    if (resultUpdated)
                    {
                        userDTO = Mapper.Map<User, UserDTO>(user);
                    }
                    if (user == null)
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to insert user" } });
                    }
                }
                else
                {
                    if (_bOUser.Exists(model.UserName, model.PhoneNo))
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Username or Phone Number") } });
                    }

                    User user = new User { SignInType = (int)SignInType.ThroughApp, ProfilePictureUrl = ImagePath, Rating = 0, IsNotificationsOn = true };
                    model.Password = CryptoHelper.Hash(model.Password);

                    Mapper.Map(model, user);
                    userDTO = _bOUser.InsertUser(user, model.InvitationCode);
                    if (user == null)
                    {
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to insert user" } });
                    }
                }
                #endregion
                #region Response
                //userDTO.AppSettings = _bOUser.GetSettings();
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }




        }



        [HttpPost]
        [Route("AddDriver")]
        public IActionResult AddDriver([FromBody]AddDriverBindingModel model)
        {
            try
            {

                //Checked Here Coz In Case it exists un necessary photos won't be saved
                if (_bODriver.Exists(model.Username, model.PhoneNo))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Username or mobile number") } });
                }

                #region ProfilePic
                string ImagePath = "";
                ImageSavingEnum responseCode = 0;
                if (model.ProfilePicture != null)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.ProfilePicture, out ImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }
                #endregion

                #region DrivingLicenseFront
                string LicenseFrontImagePath = "";
                List<string> LicenseFrontUrls = new List<string>();
                foreach (var item in model.DrivingLicenseFrontImages)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseFrontImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }

                    LicenseFrontUrls.Add(LicenseFrontImagePath);
                }
                #endregion

                #region DrivingLicenseBack
                string LicenseBackImagePath = "";
                List<string> LicenseBackUrls = new List<string>();
                foreach (var item in model.DrivingLicenseBackImages)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.LicenseImageFolderPath, item, out LicenseBackImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }

                    LicenseBackUrls.Add(LicenseBackImagePath);
                }
                #endregion

                #region RegistrationCopyImages
                string RegistrationCopyImagePath = "";
                List<string> RegistrationCopyUrls = new List<string>();
                foreach (var item in model.RegistrationCopyImages)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.RegistrationCopyImageFolderPath, item, out RegistrationCopyImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }

                    RegistrationCopyUrls.Add(RegistrationCopyImagePath);
                }
                #endregion

                #region CarPhotos
                string CarImagePath = "";
                List<string> CarImagesUrls = new List<string>();

                foreach (var item in model.CarPhotos)
                {
                    responseCode = ImageHelper.SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.CarImageFolderPath, item, out CarImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }

                    CarImagesUrls.Add(CarImagePath);
                }
                #endregion

                #region DriverSaving
                model.Password = CryptoHelper.Hash(model.Password);
                DriverDTO driverDTO = _bODriver.AddDriverAdmin(model, ImagePath, LicenseFrontUrls, LicenseBackUrls, CarImagesUrls, RegistrationCopyUrls);
                if (driverDTO == null)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Input.Something is wrong with request") } });
                }

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
        [Route("AddEditCarCompany")]//For Add and Editing
        public IActionResult AddEditCarCompany([FromBody]CarCompanyBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditCarCompany(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car company added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car company updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Car company with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Car company not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddEditCarModels")]//For Add and Editing
        public IActionResult AddEditCarModels([FromBody]CarModelBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditCarModels(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car model added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car model updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Car model with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Car model not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddEditCarYear")]//For Add and Editing
        public IActionResult AddEditCarYear([FromBody]CarYearBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditCarYear(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car year added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car year updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car year with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car year not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddEditCarCapacity")]//For Add and Editing
        public IActionResult AddEditCarCapacity([FromBody]CarCapacityBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditCarCapacity(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car capacity added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car capacity updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Car capacity with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Car capacity not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddEditCarType")]//For Add and Editing
        public IActionResult AddEditCarType([FromBody]CarTypeBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditCarType(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car type added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car type updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Car type with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Car type not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize] //Admin
        [Route("GetRidesById")]
        [HttpGet]
        public IActionResult GetRidesById(UserTypes userType, int id)
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<TripDTO> rides = _bOAdmin.GetRidesById(id, userType);
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
        [HttpPost]
        [Route("AddUpdateFareCalculation")]//For Add and Editing
        public IActionResult AddUpdateFareCalculation([FromBody]FareCalculationBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddUpdateFareCalculation(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Fare calculation setting added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Fare calculation setting updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Fare calculation setting for same time,city,paymnet method already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Fare calculation setting not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [HttpGet]
        [Route("GetAllFareCalculation")]
        public IActionResult GetAllFareCalculation(int cityId = 0, PaymentMethods paymentType = 0)
        {
            try
            {
                FareCalculationListDTO response = new FareCalculationListDTO();
                response.FareCalculations = _bOAdmin.GetAllFareCalculation(cityId, paymentType);
                return Ok(new CustomResponse<FareCalculationListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [HttpPost]
        [Route("AddSubscriptionPackage")]//For Add and Editing
        public IActionResult AddSubscriptionPackage([FromBody]SubscriptionBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditSubscriptionPackage(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Membership Package added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Membership Package updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Membership Package with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Membership Package not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddEditBank")]//For Add and Editing
        public IActionResult AddEditBank([FromBody]BankBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditBank(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Bank added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Bank updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Bank with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Bank not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [HttpPost]
        [Route("AddEditBankBranch")]//For Add and Editing
        public IActionResult AddEditBankBranch([FromBody]BankBranchBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditBankBranch(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Branch added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Branch updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Branch with same name already exists under same bank" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Branch not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddEditAccount")]//For Add and Editing
        public IActionResult AddEditAccount([FromBody]AccountBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditAccount(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Account added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Account updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Account with same title or IBAN number already exists under same branch" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Account not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("AddUpdateCountry")]//For Add and Editing
        public IActionResult AddUpdateCountry([FromBody]CountryBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddUpdateCountry(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Country added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Country updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Country with same name already exists" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Country not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddUpdateCity")]//For Add and Editing
        public IActionResult AddUpdateCity([FromBody]CityBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddUpdateCity(model);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "City added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "City updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "City with same name exists under same country" } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "City not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [HttpPost]
        [Route("AddEditRideType")]
        public IActionResult AddEditRideType([FromBody]RideTypeBindingModel model)
        {
            try
            {
                string selectedImagePath = "";
                string defImagePath = "";
                ImageSavingEnum responseCode;

                if (model.DefaultImage != null)
                {
                    responseCode = SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.RideImageFolderPath, model.DefaultImage, out defImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }
                if (model.SelectedImage != null)
                {
                    responseCode = SaveImageForWebPanel(Directory.GetCurrentDirectory(), AppSettingsProvider.RideImageFolderPath, model.SelectedImage, out selectedImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    }
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                    {
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                    }
                }


                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                AddUpdateResponse response = _bOAdmin.AddEditRideType(model, defImagePath, selectedImagePath);
                switch (response)
                {
                    case AddUpdateResponse.Added:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Vehicle type added successfully!" } });

                    case AddUpdateResponse.Updated:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Vehicle type updated successfully!" } });

                    case AddUpdateResponse.AlreadyExist:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Vehicle type with same name already exists " } });

                    case AddUpdateResponse.NotFound:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Vehicle type not found" } });
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Internal Server Error" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpGet]
        [Route("DeleteSubscriptionPackage")]
        public IActionResult DeleteSubscriptionPackage(int id)
        {
            try
            {
                if (_bOAdmin.DeleteSubscriptionPackage(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Package is successfully removed" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove package" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteAdmin")]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                if (_bOAdmin.DeleteAdmin(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Admin is successfully removed" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove Admin" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]
        [HttpGet]
        [Route("DeleteEntity")]
        public IActionResult DeleteEntity(KorsaEntityTypes type, int id)
        {
            try
            {
                if (_bOAdmin.DeleteEntity(type, id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [HttpGet]
        [Route("DeleteRideType")]
        public IActionResult DeleteRideType(int id)
        {
            try
            {
                if (_bOAdmin.DeleteRideType(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Vehicle type is successfully removed" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove vehicle type" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]
        [HttpGet]
        [Route("DeleteBank")]
        public IActionResult DeleteBank(int id)
        {
            try
            {
                if (_bOAdmin.DeleteBank(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Bank is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove bank " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteBranch")]
        public IActionResult DeleteBranch(int id)
        {
            try
            {
                if (_bOAdmin.DeleteBranch(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Branch is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove branch" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [HttpGet]
        [Route("DeleteCountry")]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCountry(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Country is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove country " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteCity")]
        public IActionResult DeleteCity(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCity(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "City is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove city" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]
        [HttpGet]
        [Route("DeletePromocode")]
        public IActionResult DeletePromocode(int id)
        {
            try
            {
                if (_bOAdmin.DeletePromocode(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Promocode is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove promocode" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [Authorize]
        [HttpGet]
        [Route("DeleteAccount")]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                if (_bOAdmin.DeleteAccount(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Account is deleted successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove account" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [HttpGet]
        [Route("DeleteCarCompany")]
        public IActionResult DeleteCarCompany(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCarCompany(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car company is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove car company " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteCarModel")]
        public IActionResult DeleteCarModel(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCarModel(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car Model is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove car model " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteCarYear")]
        public IActionResult DeleteCarYear(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCarYear(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car year is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove car year " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteCarType")]
        public IActionResult DeleteCarType(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCarType(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car type is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove car type " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("DeleteCarCapacity")]
        public IActionResult DeleteCarCapacity(int id)
        {
            try
            {
                if (_bOAdmin.DeleteCarCapacity(id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Car capacity is removed successfully" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove car capacity " } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }





        [HttpPost]
        [Route("WebPanelLogin")]
        public IActionResult WebPanelLogin([FromBody]AdminLoginBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                var admin = _bOAdmin.WebPanelLogin(model.Email, CryptoHelper.Hash(model.Password));

                if (admin != null)
                {
                    AdminDTO adminDTO = Mapper.Map<Admin, AdminDTO>(admin);
                    adminDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<AdminDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = adminDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error>
                    {
                        Message = Global.ResponseMessages.NotFound,
                        StatusCode = StatusCodes.Status409Conflict,
                        Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("email or password") }
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]//Admin
        [HttpPost]
        [Route("PayOutstandingBalance")]
        public IActionResult PayOutstandingBalance([FromBody]DriverPaymentBindingModelList model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                if (_bOAdmin.PayOutstandingBalance(model))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Balance is successfully paid to driver." } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to pay outstanding balance" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]//Admin
        [HttpGet]
        [Route("GetDriverPaymentHistory")]
        public IActionResult GetDriverPaymentHistory(int DriverId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                var response = _bOAdmin.GetDriverPaymentHistory(DriverId);
                if (response != null)
                {
                    return Ok(new CustomResponse<DriverPaymentListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new DriverPaymentListDTO { DriverPayments = response } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get payment history of this driver" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]//Admin
        [HttpGet]
        [Route("GetPaymentDetailsById")]
        public IActionResult GetPaymentDetailsById(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                var response = _bOAdmin.GetPaymentDetailsById(Id);
                if (response != null)
                {
                    return Ok(new CustomResponse<DriverPaymentDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get payment history of this driver" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _bOAdmin.GetAdmins();
                if (response != null)
                {
                    return Ok(new CustomResponse<List<AdminDTO>> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to get Admins" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]//Admin
        [HttpPost]
        [Route("AcceptTopUpRequest")]
        public IActionResult AcceptTopUpRequest([FromBody]TopUpRequestBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                if (_bOAdmin.AcceptTopUpRequest(model,id)) // this one is nice :P :D 
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Successfully performed action on your topup request" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to perform action against topup request" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]//Admin
        [HttpPost]
        [Route("AcceptCashSubscriptionRequest")]
        public IActionResult AcceptCashSubscriptionRequest([FromBody]CashSubscriptionRequestBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                if (_bOAdmin.AcceptCashSubscriptionRequest(model,id))
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Successfully performed action on your subscription request" } });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to perform action against subscription request" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("GetAdminDashboardStats")]
        [HttpGet]
        public async Task<IActionResult> GetAdminDashboardStats()
        {
            try
            {
                DashboardStatsDTO returnModel = new DashboardStatsDTO();

                var DashboardStats = _bOAdmin.GetAdminDashboardStats();

                if (DashboardStats != null)
                {
                    Mapper.Map(DashboardStats, returnModel);
                }
                return Ok(new CustomResponse<DashboardStatsDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("GetTopUpRequests")]
        [HttpGet]
        public async Task<IActionResult> GetTopUpRequests()
        {
            try
            {
                TopUpListDTO returnModel = new TopUpListDTO();

                returnModel.BankTopUps = _bOAdmin.GetTopUpRequests();
                foreach (var item in returnModel.BankTopUps)
                {
                    item.User.BankTopUps = null;
                }
                return Ok(new CustomResponse<TopUpListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("GetAppReferrers")]
        [HttpGet]
        public async Task<IActionResult> GetAppReferrers(string username = "")
        {
            try
            {
                InvitedFriendDTOList returnModel = new InvitedFriendDTOList();
                returnModel.InvitedFriends = _bOAdmin.GetAppReferrers(username);
                return Ok(new CustomResponse<InvitedFriendDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("UserWalletReport")]
        [HttpGet]
        public async Task<IActionResult> UserWalletReport(string uniqueId = "")
        {
            try
            {
                TopUpListDTO returnModel = new TopUpListDTO();
                returnModel.BankTopUps = _bOAdmin.UserWalletReport(uniqueId);
                foreach (var item in returnModel.BankTopUps)
                {
                    item.User.BankTopUps = null;
                    item.User.AppSettings = null;
                }
                return Ok(new CustomResponse<TopUpListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("PaymentReport")]
        [HttpGet]
        public async Task<IActionResult> PaymentReport(string uniqueId = "", string StartDate = "", string EndDate = "")
        {
            try
            {
                DriverPaymentListDTO returnModel = new DriverPaymentListDTO();
                returnModel.DriverPayments = _bOAdmin.PaymentReport(uniqueId, StartDate,EndDate);
                return Ok(new CustomResponse<DriverPaymentListDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("TotalEarningsReport")]
        [HttpGet]
        public async Task<IActionResult> TotalEarningsReport(string uniqueId = "", string StartDate = "", string EndDate = "")
        {
            try
            {
                EarningReportDTO returnModel = new EarningReportDTO();
                returnModel = _bOAdmin.TotalEarningsReport(StartDate, EndDate);
                return Ok(new CustomResponse<EarningReportDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("DriverLogReport")]
        [HttpGet]
        public async Task<IActionResult> DriverLogReport(string StartDate = "", string EndDate = "")
        {
            try
            {
                DriverLogDTOList returnModel = new DriverLogDTOList();
                returnModel = _bOAdmin.DriverLogReport(StartDate, EndDate);
                return Ok(new CustomResponse<DriverLogDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Route("GetSubscriptionPackagesRequests")]
        [HttpGet]
        public async Task<IActionResult> GetSubscriptionPackagesRequests()
        {
            try
            {
                CashSubscriptionDTOList returnModel = new CashSubscriptionDTOList();
                returnModel.CashSubscriptions = _bOAdmin.GetSubscriptionPackagesRequests();
                foreach (var item in returnModel.CashSubscriptions)
                {
                    item.Driver.CashSubscriptions = null;
                }
                return Ok(new CustomResponse<CashSubscriptionDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("GetSupportConversationsList")]
        [HttpGet]
        public async Task<IActionResult> GetSupportConversationsList(string search)
        {
            try
            {
                SupportConversationDTOList returnModel = new SupportConversationDTOList();

                returnModel.Conversations = _bOAdmin.GetSupportConversationsList(search);
                return Ok(new CustomResponse<SupportConversationDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(string StartDate = "", string EndDate = "", SearchType UserSearchBy = 0, string UserName = "", string userId = "")
        {
            try
            {
                UserDTOList returnModel = new UserDTOList();

                var Users = _bOAdmin.GetAllUsers(StartDate, EndDate, UserSearchBy, UserName, userId);

                if (Users != null)
                {
                    Mapper.Map(Users, returnModel.Users);
                }

                return Ok(new CustomResponse<UserDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int User_Id)
        {
            try
            {
                UserViewModel returnModel = new UserViewModel();

                returnModel.User = _bOAdmin.GetUserById(User_Id);
                foreach (var item in returnModel.User.BankTopUps)
                {
                    item.User = null;
                }
                return Ok(new CustomResponse<UserViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Route("GetUserReferralsByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetUserReferralsByUserId(int User_Id)
        {
            try
            {
                InvitedFriendDTOList returnModel = new InvitedFriendDTOList();

                returnModel = _bOAdmin.GetUserReferralsByUserId(User_Id);

                return Ok(new CustomResponse<InvitedFriendDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("GetDriver")]
        [HttpGet]
        public async Task<IActionResult> GetDriver(int Driver_Id)
        {
            try
            {
                var driver = _bOAdmin.GetDriverById(Driver_Id);
                return Ok(new CustomResponse<DriverViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new DriverViewModel { Driver = driver } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("GetAllDrivers")]
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers(bool isBecomeDriverRequests = false, string StartDate = "", string EndDate = "", SearchType UserSearchBy = 0, string UserName = "", string userId = "")
        {
            try
            {
                DriverDTOList returnModel = new DriverDTOList();

                var Drivers = _bOAdmin.GetAllDrivers(isBecomeDriverRequests, StartDate, EndDate, UserSearchBy, UserName, userId);

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

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var Id = Convert.ToInt32(User.GetClaimValue("Id"));

                var resp = _bOAdmin.ChangePassword(Id, CryptoHelper.Hash(model.OldPassword), CryptoHelper.Hash(model.NewPassword));
                if (resp == 0)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Invalid old password." } });
                }
                else if (resp == 2)
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Old and new password cannot be same." } });
                }
                else
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Password changed successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [HttpPost]
        [Route("ChangeUserStatuses")]
        public IActionResult ChangeUserStatuses([FromBody]ChangeUserStatusListModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _bOAdmin.ChangeUserStatuses(model);

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Status updated successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpPost]
        [Route("SaveDriverStatuses")]
        public IActionResult SaveDriverStatuses([FromBody]ChangeUserStatusListModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _bOAdmin.ChangeDriverStatuses(model);

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Status updated successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpPost]
        [Route("SaveDriverRequestStatuses")]
        public IActionResult SaveDriverRequestStatuses([FromBody]ChangeUserStatusListModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_bOAdmin.SaveDriverRequestStatuses(model))
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Status updated successfully." });
                }
                else
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Status updation failed." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("GetAllRides")]
        [HttpGet]
        public async Task<IActionResult> GetAllRides(string StartDate = "", string EndDate = "", RideSearchType FilterType = 0, int rideId = 0)
        {
            try
            {
                TripDTOList returnModel = new TripDTOList();
                List<TripDTO> tripDTOs = new List<TripDTO>();
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                var rides = _bOAdmin.GetAllRides(StartDate, EndDate, FilterType, rideId);
                Mapper.Map(rides, tripDTOs);
                returnModel.Rides = tripDTOs;
                return Ok(new CustomResponse<TripDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpGet]
        [Route("GetEntityById")]
        public async Task<IActionResult> GetEntityById(KorsaEntityTypes EntityType, int? Id)
        {
            try
            {
                CultureType culture = CultureType.English;
                switch (EntityType)
                {
                    case KorsaEntityTypes.Admin:
                        {
                            Admin obj = _bOAdmin.GetAdminById(Id.Value);
                            AdminDTO adminDTO = Mapper.Map<Admin, AdminDTO>(obj);
                            return Ok(new CustomResponse<AdminDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = adminDTO });
                        }
                    case KorsaEntityTypes.Settings:
                        {
                            SettingsDTO settingsDTO = _bOUser.GetSettings();
                            return Ok(new CustomResponse<SettingsDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = settingsDTO });
                        }

                    case KorsaEntityTypes.SubscriptionPackage:
                        {
                            SubscriptionPackage settings = _bOAdmin.GetSubscriptionPackageById(Id);
                            SubscriptionPackageDTO subscriptionPackageDTO = new SubscriptionPackageDTO();
                            Mapper.Map(settings, subscriptionPackageDTO);
                            return Ok(new CustomResponse<SubscriptionPackageDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = subscriptionPackageDTO });
                        }
                    case KorsaEntityTypes.RideType:
                        {
                            RideTypeDTO rideTypeDTO = _bOAdmin.GetRideTypeById(Id);
                            return Ok(new CustomResponse<RideTypeDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = rideTypeDTO });
                        }

                    case KorsaEntityTypes.Bank:
                        {
                            BankDTO bankDTO = _bOAdmin.GetBankById(Id.Value, culture);
                            return Ok(new CustomResponse<BankDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = bankDTO });
                        }

                    case KorsaEntityTypes.Branch:
                        {
                            BranchDTO branchDTO = _bOAdmin.GetBranchById(Id.Value, culture);
                            return Ok(new CustomResponse<BranchDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = branchDTO });
                        }
                    case KorsaEntityTypes.Account:
                        {
                            AccountDTO accountDTO = _bOAdmin.GetAccountById(Id.Value, culture);
                            return Ok(new CustomResponse<AccountDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = accountDTO });
                        }
                    case KorsaEntityTypes.Country:
                        {
                            CountryDTO countryDTO = _bOAdmin.GetCountryById(Id.Value, culture);
                            return Ok(new CustomResponse<CountryDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = countryDTO });
                        }
                    case KorsaEntityTypes.City:
                        {
                            CityDTO cityDTO = _bOAdmin.GetCityById(Id.Value, culture);
                            return Ok(new CustomResponse<CityDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = cityDTO });
                        }
                    case KorsaEntityTypes.CarCompany:
                        {
                            CarCompanyDTO carCompanyDTO = _bOAdmin.GetCarCompanyById(Id.Value, culture);
                            return Ok(new CustomResponse<CarCompanyDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = carCompanyDTO });
                        }
                    case KorsaEntityTypes.CarModel:
                        {
                            CarModelDTO carModelDTO = _bOAdmin.GetCarModelById(Id.Value, culture);
                            return Ok(new CustomResponse<CarModelDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = carModelDTO });
                        }
                    case KorsaEntityTypes.CarType:
                        {
                            CarTypeDTO carTypeDTO = _bOAdmin.GetCarTypeById(Id.Value, culture);
                            return Ok(new CustomResponse<CarTypeDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = carTypeDTO });
                        }
                    case KorsaEntityTypes.CarYear:
                        {
                            CarYearDTO carYearDTO = _bOAdmin.GetCarYearById(Id.Value, culture);
                            return Ok(new CustomResponse<CarYearDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = carYearDTO });
                        }
                    case KorsaEntityTypes.CarCapacity:
                        {
                            CarCapacityDTO carCapacityDTO = _bOAdmin.GetCarCapacityById(Id.Value, culture);
                            return Ok(new CustomResponse<CarCapacityDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = carCapacityDTO });
                        }
                    case KorsaEntityTypes.Role:
                        {
                            RoleDTO roleDTO = _bOAdmin.GetRoleById(Id.Value, culture);
                            return Ok(new CustomResponse<RoleDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = roleDTO });
                        }
                    case KorsaEntityTypes.FareCalculation:
                        {
                            FareCalculationDTO roleDTO = _bOAdmin.GetFareCalculationDetailsById(Id.Value, culture);
                            return Ok(new CustomResponse<FareCalculationDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = roleDTO });
                        }
                    default:
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid entity type" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));

            }
        }


        [HttpPost]
        [Route("SetSettings")]
        public async Task<IActionResult> SetSettings([FromBody]SettingsDTO settingsModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Settings model = new Settings();
                if (_bOAdmin.SetSettings(settingsModel) != null)
                {
                    Mapper.Map(model, settingsModel);
                    return Ok(new CustomResponse<SettingsDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = settingsModel });
                }
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Invalid settings" } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));

            }
        }

    }
}
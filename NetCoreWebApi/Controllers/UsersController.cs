using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Wasalee.JwtHelpers;
using Microsoft.Extensions.Configuration;
using BLL.Interface;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LIMO.BindingModels;
using Component.Utility;
using Component.ResponseFormats;
using AppModel.DTOs;
using AppModel.BindingModels;
using System.Collections.Generic;
using DAL.DomainModels;
using Component;
using static Utility.ImageHelper;
using System.Threading.Tasks;
using Utility;
using System.Net;

namespace NetCoreWebApi.Controllers
{
    [Route("/api/User")]
    public class UsersController : Controller
    {
        #region Properties and constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBOUser _bOUser;
        protected readonly IBODriver _bODriver;
        private readonly IHostingEnvironment _environment;

        public UsersController(DataContext dataContext, IConfiguration configuration, IBOUser bOUser, IBODriver bODriver, IHostingEnvironment environment)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOUser = bOUser;
            _bODriver = bODriver;
            _environment = environment;
        }
        #endregion




        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            try
            {
                #region ImageValidationAndSaving
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string ImagePath = "";

                if (model.ProfilePicture != null)
                {
                    ImageSavingEnum responseCode = SaveImage(Directory.GetCurrentDirectory(), AppSettingsProvider.UserImageFolderPath, model.ProfilePicture, out ImagePath);

                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                }
                #endregion

                #region UserSaving
                if (_bOUser.Exists(model.UserName, model.PhoneNo))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Username or Phone Number") } });
                User user = new User { SignInType = (int)SignInType.ThroughApp, ProfilePictureUrl = ImagePath, Rating = 0, IsNotificationsOn = true };
                model.Password = CryptoHelper.Hash(model.Password);

                if (!_bOUser.isPhoneConfirmationValid(model.PhoneNo))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Phone number not confirmed." } });
                Mapper.Map(model, user);                
                UserDTO userDTO = _bOUser.InsertUser(user, model.InvitationCode);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to insert user" } });
              
                #endregion

                #region Response
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }




        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _bOUser.AuthenticateCredentials(model.CellNumOrUsername, CryptoHelper.Hash(model.Password));

                if (user != null)
                {
                    UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                    userDTO.AppSettings = _bOUser.GetSettings();
                    userDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username/mobile number  or password") } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpPost]
        [Route("AddSupportConversation")]
        public IActionResult AddSupportConversation(SupportConversationBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                SupportConversation conversation = _bOUser.AddSupportConversation(model);
                if (conversation == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to inititiate support conversation" } });
                SupportConversationDTO response = new SupportConversationDTO();
                Mapper.Map(conversation, response);
                return Ok(new CustomResponse<SupportConversationDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [HttpPost]
        [Route("AddFavouriteLocation")]
        public IActionResult AddFavouriteLocation(FavouriteLocationBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                FavouriteLocation favourite = _bOUser.AddFavouriteLocation(model, id);
                if (favourite == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to add location to favourite list" } });

                FavouriteLocationDTO response = new FavouriteLocationDTO();
                Mapper.Map(favourite, response);
                    return Ok(new CustomResponse<FavouriteLocationDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }
        
        [Authorize]//user
        [HttpGet]
        [Route("GetFavouriteLocations")]
        public async Task<IActionResult> GetFavouriteLocations()
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                FavouriteLocationDTOList response = new FavouriteLocationDTOList();
                response.FavouriteLocationsList = _bOUser.GetFavouriteLocations(id);
                return Ok(new CustomResponse<FavouriteLocationDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [Route("GetCurrentStatus")]
        [HttpGet]
        public IActionResult GetCurrentStatus(UserTypes userType)
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                TripDTO response = _bOUser.GetCurrentStatus(userType, id);
                if (response != null)
                    return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                else
                    return Ok(new CustomResponse<TripDTO> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = null});
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]//user
        [HttpGet]
        [Route("UnFavouriteLocation")]
        public async Task<IActionResult> UnFavouriteLocation(int id)
        {
            try
            {
                FavouriteLocationDTOList response = new FavouriteLocationDTOList();
                if (_bOUser.UnFavouriteLocation(id))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Location is successfully removed from favourite list" } });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to remove location from favourite list" } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }



        [Authorize]
        [HttpPost]
        [Route("AddBankTopUp")]
        public IActionResult AddBankTopUp(AddBankTopUpBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));

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
                if(!_bOUser.AddBankTopupRequest(userId, TopUpReceiptUrls,model))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to topup amount" } });


                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Topup is done successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        //[HttpGet]
        //[Route("ExternalLogin")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLogin(ExternalLoginBindingModel model)
        //{
        //    try
        //    {
        //        if (model.SocialLoginType == SocialLoginType.Facebook && !string.IsNullOrEmpty(model.AccessToken))
        //        {
        //            SocialLogins socialLogin = new SocialLogins();
        //            var socialUser = await socialLogin.GetSocialUserData(model.AccessToken, model.SocialLoginType);
        //            if (socialUser != null)
        //            {
        //                return BadRequest("Unable to get user info");
        //            }
        //            else
        //            {
        //                #region UserSaving
        //                if (_bOUser.Exists(socialUser.username, socialUser.username))
        //                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Social user") } });
        //                User user = new User
        //                {
        //                    UserName = socialUser.name,
        //                    SignInType = (int)SignInType.Facebook,
        //                    EmailConfirmed = false,
        //                    PhoneConfirmed = false,
        //                    Email = socialUser.email,
        //                    ProfilePictureUrl = socialUser.picture,
        //                    Password = CryptoHelper.Hash(socialUser.password),
        //                    Rating = 5
        //                };

        //                Mapper.Map(model, user);
        //                UserDTO userDTO = _bOUser.InsertUser(user);
        //                if (user == null)
        //                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to insert user" } });
        //                #endregion


        //            }
        //        }
        //        else
        //            return BadRequest("Please provide access token along with social login type");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}





        [Authorize]//user,driver
        [HttpGet]
        [Route("GetNotifications")]
        public async Task<IActionResult> GetNotifications(UserTypes userType)
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                NotificationDTOList response = new NotificationDTOList();
                response.NotificationsList = _bOUser.GetNotifications(id, userType);
                return Ok(new CustomResponse<NotificationDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Authorize]
        [Route("GetUserService")]
        [HttpGet]
        public IActionResult GetUserService()
        {
            try
            {
                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                User user = _bOUser.GetUserById(id);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides.") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        //[HttpPost]
        //[Route("WebPanelLogin")]
        //public IActionResult WebPanelLogin([FromBody]WebPanelLoginBindingModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

        //        var admin = _bOUser.WebPanelLogin(model.Email, CryptoHelper.Hash(model.Password));

        //        if (admin != null)
        //        {
        //            var adminDTO = Mapper.Map<Admin, AdminDTO>(admin);
        //            adminDTO.GenerateToken(_configuration);

        //            return Ok(new CustomResponse<AdminDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = adminDTO });
        //        }
        //        else
        //        {
        //            return Ok(new CustomResponse<Error>{Message = Global.ResponseMessages.NotFound,StatusCode = StatusCodes.Status409Conflict,Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("email or password") }});
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}


        [Authorize]
        [HttpPost]
        [Route("ChangeNotificationStatus")]
        public IActionResult ChangeNotificationStatus(bool isOn)
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });


                user.IsNotificationsOn = isOn;
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UseCreditFirst")]
        public IActionResult UseCreditFirst(bool useCredit)
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });


                user.UseCreditFirst = useCredit;
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("SelectPaymentMethod")]
        public IActionResult SelectPaymentMethod(PaymentMethodBindingModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update payment method") } });
                if(String.IsNullOrEmpty(user.ActiveCardId) && user.PrefferedPaymentMethod==PaymentMethods.CreditCard)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Please add credit card first" } });


                user.PrefferedPaymentMethod = model.PaymentMethodType;

                user.ModifiedDate = DateTime.UtcNow;

                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update payment method") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("AddCreditCard")]
        public IActionResult AddCreditCard(AddCreditCardBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int id = Convert.ToInt32(User.GetClaimValue("Id"));
                var response = _bOUser.AddUserCreditCard(model, id);
                if (response==null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to add credit card" } });
                return Ok(new CustomResponse<CreditCardDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response  });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        




        [Authorize]
        [HttpPost]
        [Route("SetDriverPreference")]
        public IActionResult SetDriverPreference(Gender gender)
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });


                user.DriverPreference = gender;
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpGet]
        [Route("SetActiveCard")]
        public IActionResult SetActiveCard(string cardId)
        {
            try
            {
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to update active card" } });


                user.ActiveCardId = cardId;
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Failed to update active card" } });

                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Card marked active successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [Authorize]
        [HttpPost]
        [Route("ContactUs")]
        public IActionResult ContactUs(ContactUsBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Id = Convert.ToInt32(User.GetClaimValue("Id"));
                if (model.UserType == UserTypes.User)
                {
                    User user = _bOUser.GetUserById(Id);
                    if (user == null)
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Unauthorized User") } });
                    if (!_bOUser.ContactUs(model.Message, Id, isDriver: false))
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Contact to app failed" } });

                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Your message to administrator has been sent successfully" } });
                }
                else
                {
                    Driver driver = _bODriver.GetDriverById(Id);
                    if (driver == null)
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Unauthorized Driver") } });
                    if (!_bOUser.ContactUs(model.Message, Id, isDriver: true))
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Contact to app failed" } });

                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Your message to administrator has been sent successfully" } });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
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
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Failed to update password" } });
                if (user.Password != CryptoHelper.Hash(model.Password))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("old Password") } });


                user.Password = CryptoHelper.Hash(model.NewPassword);
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
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
                User user = _bOUser.GetUser(model.CellNumOrUsername);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });

                user.Password = CryptoHelper.Hash(model.NewPassword);
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update password") } });
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Password changed successfully" } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpPost]
        [Route("EditUserProfile")]
        public IActionResult EditUserProfile(EditUserProfileBindingModel model)
        {
            try
            {
                #region ValidationsAndImageSaving
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string ImagePath = "";
                if (model.ProfilePicture != null)
                {
                    ImageSavingEnum responseCode = SaveImage(Directory.GetCurrentDirectory(), _configuration.GetValue<string>("UserImageFolderPath"), model.ProfilePicture, out ImagePath);
                    if (responseCode == ImageSavingEnum.InvalidExtension)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image of type .jpg,.gif,.png. " } });
                    else if (responseCode == ImageSavingEnum.MaxSizeExceeded)
                        return Ok(new CustomResponse<Error> { Message = "UnsupportedMediaType", StatusCode = StatusCodes.Status415UnsupportedMediaType, Result = new Error { ErrorMessage = "Please Upload image upto " + Global.ImageMaxSize + "." } });
                }
                #endregion

                #region ProfileUpdation
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                User user = _bOUser.GetUserById(userId);
                string ImageUrl = String.IsNullOrEmpty(ImagePath) ? user.ProfilePictureUrl : ImagePath;
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Invalid User") } });
                Mapper.Map(model, user);
                user.ProfilePictureUrl = ImageUrl;
                user.ModifiedDate = DateTime.UtcNow;
                if (!_bOUser.UpdateUser(user))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = new Error { ErrorMessage = "Unable to insert user" } });
                #endregion

                #region Response
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        /// <summary>
        /// using Nexmo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendVerificationSms")]
        public IActionResult SendVerificationSms(PhoneBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!model.isResetCase)
                {
                    if (_bOUser.IsUserNumberAlreadyVerified(model.PhoneNumber, model.UserType))
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Phone Number already exists" } });
                }
                else
                {

                    switch (model.UserType)
                    {
                        case UserTypes.User:
                            {
                                if (!_bOUser.Exists(model.PhoneNumber, model.PhoneNumber))
                                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Unregistered user" } });
                            }
                            break;
                        case UserTypes.Driver:
                            {
                                if (!_bODriver.Exists(model.PhoneNumber, model.PhoneNumber))
                                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Unregistered driver" } });
                            }
                            break;
                        default:
                            return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Invalid User type" } });
                    }
                }

                if (model.isResetCase)
                    model.PhoneNumber = _bOUser.GetPhoneNo(model.PhoneNumber, model.UserType); // In case User Enter Username instead of Contact Number 
                int code = _bOUser.SendVerficationCode(model.PhoneNumber, model.UserType);
                if (code <= 0)
                    return Ok(new CustomResponse<Error> { Message = "InternalServerError", StatusCode = StatusCodes.Status500InternalServerError, Result = new Error { ErrorMessage = "SMS failed due to some reason." } });
                return Ok(new CustomResponse<NexmoCodeDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new NexmoCodeDTO { Code = code.ToString() } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }



        [HttpPost]
        [Route("VerifySmsCode")]
        public IActionResult VerifySmsCode(PhoneVerificationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                if (!_bOUser.VerifySmsCode(model.Code, model.UserType))
                    return Ok(new CustomResponse<Error> { Message = "NotFound", StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Invalid code" } });

                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new Error { ErrorMessage = "Code Verified Successfully!" } });


            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }




        [Authorize]
        [HttpPost]
        [Route("VerifyPromocode")]
        public IActionResult VerifyPromocode(PromocodeBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                PromocodeDTO code = _bOUser.VerifyPromocode(model.Promocode, userId);

                if (code != null)
                {
                    if (code.Id == 0)
                        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Promocode is already used." } });
                    else
                        return Ok(new CustomResponse<PromocodeDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = code });
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = "Invalid promocode." } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Authorize]
        [HttpGet]
        [Route("ConfirmPaypalPayment")]
        public async Task<IActionResult> ConfirmPaypalPayment(PaymentConfirmationBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId = Convert.ToInt32(User.GetClaimValue("Id"));
                var response=  await _bOUser.ConfirmPaypalPayment(model, userId);
                switch(response)
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




        [Route("LoginAsAdmin")]
        [HttpGet]
        public IActionResult LoginAsAdmin(string username, string password)
        {
            if (username == "Admin" && password == "Pass")
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                                .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                                .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                                .AddExpiry(1)
                                .AddClaim("Name", "Admin")
                                .AddRole("Admin")
                                .Build();

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = token.Value });
            }
            else
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.NotFound, StatusCode = StatusCodes.Status404NotFound, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") } });

        }

        [Route("GetUser")]
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var name = User.GetClaimValue("Name");

            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

        [Route("GetAdmin")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAdmin()
        {
            var name = User.GetClaimValue("Name");

            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

        //[Route("UpdateLocation")]
        //[HttpPost]
        //public IActionResult UpdateLocation(LocationBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    int userId = Convert.ToInt32(User.GetClaimValue("Id"));
        //    User user = _bOUser.GetUserById(userId);
        //    if (user == null)
        //        return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage= "You are an authorized user" } });
        //    user.
        //}


        /// <summary>
        /// Register for getting push notifications
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("RegisterPushNotification")]
        public IActionResult RegisterPushNotification(RegisterPushNotificationBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int id = Convert.ToInt32(User.GetClaimValue("Id"));
            UserDevice userDeviceModel = new UserDevice
            {
                Platform = model.IsAndroidPlatform,
                ApplicationType = model.IsPlayStore ? UserDevice.ApplicationTypes.PlayStore : UserDevice.ApplicationTypes.Enterprise,
                EnvironmentType = model.IsProduction ? UserDevice.ApnsEnvironmentTypes.Production : UserDevice.ApnsEnvironmentTypes.Sandbox,
                UDID = model.UDID,
                AuthToken = model.AuthToken,
                IsActive = true,
                DeviceName=model.DeviceName
            };
            UserDeviceDTO deviceDTO = _bOUser.RegisterDeviceForPushNotification(id, model.SignInType, userDeviceModel);
            if (deviceDTO != null)
                return Ok(new CustomResponse<UserDeviceDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = deviceDTO });
            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = "Failed to register device" });

        }



        [Route("GetSettings")]
        [HttpGet]
        public IActionResult GetSettings()
        {
            try
            {
                SettingsDTO appSettings = _bOUser.GetSettings();
                if (appSettings == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateNotFound("Settings") } });

                return Ok(new CustomResponse<SettingsDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = appSettings });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [Route("GetUserById")]
        [HttpGet]
        public IActionResult GetUserById(int Id)
        {
            try
            {
                User user = _bOUser.GetUserById(Id);
                if (user == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to update notification status") } });
                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                userDTO.AppSettings = _bOUser.GetSettings();
                userDTO.GenerateToken(_configuration);
                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }




        [Route("GetAllCancellationReason")]
        [HttpGet]
        public IActionResult GetAllCancellationReason()
        {
            try
            {
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                List<CancellationReasonDTO> cancellationReasons = _bOUser.GetAllCancellationReason(culture);
                if (cancellationReasons == null)
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("Failed to get rides types.") } });

                return Ok(new CustomResponse<CancellationReasonDTOList> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new CancellationReasonDTOList { CancellationReasons = cancellationReasons } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }
    }
}
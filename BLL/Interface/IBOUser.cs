using AppModel;
using AppModel.BindingModels;
using AppModel.DomainModels;
using AppModel.DTOs;
using Component.Utility;
using DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IBOUser
    {
        TripDTO GetCurrentStatus(UserTypes userType, int id);
        FavouriteLocation AddFavouriteLocation(FavouriteLocationBindingModel model, int userId);
        UserDTO InsertUser(User user, string invitationCode);
        bool isPhoneConfirmationValid(string phoneNum);
        bool Exists(string email);
        string GetPhoneNo(string username, UserTypes userType);
        bool Exists(string username, string phoneNum);
        User AuthenticateCredentials(string username, string password);
        bool UpdateUser(User user);
        User ResetForgotPassword(string email, string code);
        User GetUser(string usernameOrCellNum);
        bool ContactUs(string message, int userId, bool isDriver);
        User GetUserById(int Id);
        int VerifyInvitaionCode(string invitationCode);

        bool IsUserNumberAlreadyVerified(string PhoneNum, UserTypes userType);
        int SendVerficationCode( string PhoneNum,UserTypes userType);
        bool VerifySmsCode(int code, UserTypes userType);
        PromocodeDTO VerifyPromocode(string promocode, int userId);
        SettingsDTO GetSettings();
        List<CancellationReasonDTO> GetAllCancellationReason(CultureType culture);
        UserDeviceDTO RegisterDeviceForPushNotification(int id, UserTypes userType, UserDevice device);
        Admin WebPanelLogin(string username, string password);
        List<NotificationDTO> GetNotifications(int id, UserTypes userType);
        List<FavouriteLocationDTO> GetFavouriteLocations(int id);
        bool UnFavouriteLocation(int id);
        bool EditUserExists(string username, string phoneNum, string UniqueId);
        bool AddBankTopupRequest(int userId, List<string> topUpReceiptUrls, AddBankTopUpBindingModel model);
        CreditCardDTO AddUserCreditCard(AddCreditCardBindingModel model, int Id);
        SupportConversation AddSupportConversation(SupportConversationBindingModel model);
        Task<object> ConfirmPaypalPayment(PaymentConfirmationBindingModel model, int id);
    }
}

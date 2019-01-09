using AppModel;
using AppModel.BindingModels;
using AppModel.DTOs;
using Component.Utility;
using DAL.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Services.Global;

namespace BLL.Interface
{
    public interface IBORide
    {
        List<RideTypeDTO> GetAllRideTypes(CultureType culture, int estimatedTime, double distanceInKm, string promocode, string PickupCity, string DestinationCity, PaymentMethods paymentType,int? userId, out int promoId);
        Trip InsertTrip(Trip trip);
        List<DriverDTO> GetNearByDrivers(Loc location, int rideId, int? rideTypId, string key);
        Trip GetTripById(int Id);
        Trip GetTripByIdWithDriverAndUser(int Id);
        bool UsePromocode(int? promocodeId, int userId);
        bool UpdateTrip(Trip ride);
        bool EndRide(FinishRideBindingModel model);

        List<TripDTO> GetUpcomingRidesByUserId(int id, CultureType culture);
        List<TripDTO> GetPastRidesByUserId(int id,CultureType culture);

        Task<bool> GetScheduledRideLaterRides();
        MessageDTO InsertMessage(Message message);
        List<MessageDTO> GetMessages(int userId, int driverId);
        List<TripDTO> GetPastRidesByDriverId(int id, CultureType culture);
        List<TripDTO> GetFutureRidesByDriverId(int id, CultureType culture);
        bool SendPushToUser(int userId, int driverId, Notification notf, PushNotificationType type, string key);
        bool SendPushToDriver(int userId, int driverId, Notification notf, PushNotificationType type, string key);
        double GetEstimatedFare(int rideTypeId, double distance, int? promocodeId);
        TripDTO GetRideById(int id);
        bool CheckCityStatus(string pickupCity);
        bool ManagePromocode(int? promocode_Id);
        Task<object> CloseRide(CloseRideBindingModel model);
    }
}

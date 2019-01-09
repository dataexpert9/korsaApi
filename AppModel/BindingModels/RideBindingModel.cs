using AppModel.CustomModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public class RideNowBindingModel
    {

        public Locations PickupLocation { get; set; }
        public Locations DestinationLocation { get; set; }
        [Required]
        public int RideType_Id { get; set; }
        [Required]
        public string PickupLocationName { get; set; }
        [Required]
        public string DestinationLocationName { get; set; }
        public string DestinationCity { get; set; }
        public string PickupCity { get; set; }

        [Required]
        public float EstimatedFare { get; set; }
        public int? Promocode_Id { get; set; }

        public double PeakFactor { get; set; }
        public double FarePerKm { get; set; }
    }

    public class RideLaterBindingModel
    {

        [Required]
        public Locations PickupLocation { get; set; }
        [Required]
        public Locations DestinationLocation { get; set; }
        [Required]
        public string PickupLocationName { get; set; }
        [Required]
        public string DestinationLocationName { get; set; }
        public string DestinationCity { get; set; }
        public string PickupCity { get; set; }
        public string SocketJson { get; set; }
        //[Required]
        public float EstimatedFare { get; set; }
        [Required]
        public int RideType_Id { get; set; }
        public int? Promocode_Id { get; set; }
        public DateTime StartTime { get; set; }
    }


    public class FinishRideBindingModel
    {
        [Required]
        public int RideId { get; set; }
        [Required]
        public double DistanceTravelled { get; set; }

    }


    public class CloseRideBindingModel
    {
        [Required]
        public int RideId { get; set; }
        [Required]
        public double CollectCash { get; set; }
    }



    public class CancelRideBindingModel
    {
        [Required]
        public int RideId { get; set; }
        public int? CancellationReasonId { get; set; } = null;
        public bool isDriverCancelling { get; set; }
        public string ReasonToCancel { get; set; }

    }

    public class ILocationModel
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
        public double dropoflatitude { get; set; }
        public double dropoflongitude { get; set; }
        public string channel { get; set; }
        public double locationType { get; set; }
        public IUserModel userData { get; set; }
        public int orderid { get; set; }
        public int direction { get; set; }
        public double distance { get; set; }
        public int numberofdrivers { get; set; }
        public double price { get; set; }
        public string pickupLocationTitle { get; set; }
        public string dropofLocationTitle { get; set; }
        public int driverid { get; set; }
        public string gender { get; set; }
        public int vehicalType { get; set; }
        public bool isCash { get; set; }
    }

    public class IUserModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public int userType { get; set; }
        public double rating { get; set; }
    }

}

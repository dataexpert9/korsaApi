using AppModel.CustomModels;
using AppModel.DomainModels;
using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class Trip : BaseModel
    {
        public int Id { get; set; }
        public Location PickupLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public double Fare { get; set; }
        public double WalletDeduction { get; set; }
        public double CollectCash { get; set; }
        public string PickupLocationName { get; set; }
        public string DestinationLocationName { get; set; }
        public float EstimatedFare { get; set; }
        public float Discount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public double DriverRating { get; set; }
        public double UserRating { get; set; }
        public string FeedbackForUser { get; set; }
        public string FeedbackForDriver { get; set; }
        public TripStatus Status { get; set; }
        public bool isScheduled { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey("Promocode")]
        public int? Promocode_Id { get; set; }
        public Promocode Promocode { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("CancellationReason")]
        public int? CancellationReason_Id { get; set; }
        public string ReasonToCancel { get; set; }
        public CancellationReason CancellationReason { get; set; }

        public float FarePerKm { get; set; }
        public float PeakFactor { get; set; }

        [ForeignKey("PrimaryUser")]
        public int? PrimaryUser_Id { get; set; }
        public User PrimaryUser { get; set; }

        [ForeignKey("RideType")]
        public int RideType_Id { get; set; }
        public RideType RideType { get; set; }

        [ForeignKey("Driver")]
        public int? Driver_Id { get; set; }
        public Driver Driver { get; set; }




        public ICollection<UserTrip> UserTrips { get; set; }
    }
}

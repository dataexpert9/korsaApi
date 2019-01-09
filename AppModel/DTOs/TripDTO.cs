using AppModel.CustomModels;
using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class TripDTO
    {
        public int Id { get; set; }
        public Locations PickupLocation { get; set; }
        public Locations DestinationLocation { get; set; }
        public string PickupLocationName { get; set; }
        public string DestinationLocationName { get; set; }
        public float EstimatedFare { get; set; }
        public float FarePerKm { get; set; }
        public float PeakFactor { get; set; }
        public double DriverRating { get; set; }
        public double UserRating { get; set; }
        public string FeedbackForUser { get; set; }
        public string FeedbackForDriver { get; set; }
        public float Fare { get; set; }
        public float Discount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public double CollectCash { get; set; }
        public double WalletDeduction { get; set; }
        public TripStatus Status { get; set; }
        public bool isScheduled { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; }
        public int? Promocode_Id { get; set; }

        public string RideTypeName { get; set; }
        public int? PrimaryUser_Id { get; set; }
        public UserDTO PrimaryUser { get; set; }
        public int RideType_Id { get; set; }
        //public RideTypeDTO RideType { get; set; }
        public int Driver_Id { get; set; }
        public DriverDTO Driver { get; set; }

    }

    public class TripDTOList 
    {
        public List<TripDTO> Rides { get; set; }
        
    }
}

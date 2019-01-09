using Component.Utility;
using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public  class RateRideBindingModel
    {
        [Required]
        public int Ride_Id { get; set; }
        [Required]
        [Range(1,5, ErrorMessage ="Rating must be between 1-5")]
        public short Rating { get; set; }
        public string FeedBack { get; set; }
        public bool isDriver { get; set; }
    }

    public class ChangeRideStatusBindingModel
    {
        [Required]
        public int Ride_Id { get; set; }
        [Required]
        public TripStatus Status { get; set; }
    }

    
}

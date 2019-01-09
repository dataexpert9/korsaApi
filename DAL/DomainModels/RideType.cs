using Component.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class RideType : BaseModel
    {
        public RideType()
        {
            RideTypeMLsList = new List<RideTypeML>();
            Vehicles = new List<Vehicle>();
            Trips = new List<Trip>();
            FareCalculations = new List<FareCalculation>();
        }
        public int Id { get; set; }
        public int PersonsCapacity { get; set; }
        public int CapacityInKGs { get; set; }

        public float FarePerKM { get; set; }
        public string DefaultImageUrl { get; set; }
        public string SelectedImageUrl { get; set; }
        public float PeakFactor { get; set; }
        public float BasicCharges { get; set; }
        public double FarePerMin { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<FareCalculation> FareCalculations { get; set; }

        public virtual ICollection<RideTypeML> RideTypeMLsList { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }

    public class RideTypeML : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AboutRideType { get; set; }
        public CultureType Culture { get; set; }
        [ForeignKey("RideType")]
        public int RideType_Id { get; set; }
        public RideType RideType { get; set; }
    }
}

using System.Collections.Generic;

namespace AppModel.DTOs
{
    public class RideTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonsCapacity { get; set; }
        public string AboutRideType { get; set; }
        public float FarePerKm { get; set; }
        public double FarePerMin { get; set; }
        public int CapacityInKGs { get; set; }

        public float BasicCharges { get; set; }
        public string DefaultImageUrl { get; set; }
        public string SelectedImageUrl { get; set; }
        public float PeakFactor { get; set; }
        public double EstimatedFare { get; set; }

    }
    public class RideTypeDTOList
    {
        public RideTypeDTOList()
        {
            RideTypeList = new List<RideTypeDTO>();
        }
        public List<RideTypeDTO> RideTypeList { get; set; }
        public int PromocodeId { get; set; }
    }
}

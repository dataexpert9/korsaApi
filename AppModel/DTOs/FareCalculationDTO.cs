using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class FareCalculationDTO
    {
        public int Id { get; set; }
        public int? City_Id { get; set; }
        public int? RideType_Id { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public double BasicCharges { get; set; }
        public double FarePerKM { get; set; }
        public double FarePerMin { get; set; }
        public CityDTO City { get; set; }

    }
    public class FareCalculationListDTO
    {
        public List<FareCalculationDTO> FareCalculations { get; set; }
    }
}
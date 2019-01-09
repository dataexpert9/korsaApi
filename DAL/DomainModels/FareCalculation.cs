using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class FareCalculation : BaseModel
    {
        public int Id { get; set; }
        public int? City_Id { get; set; }
        public int? RideType_Id { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public double BasicCharges { get; set; }
        public double FarePerKM { get; set; }
        public double FarePerMin { get; set; }
        public City City { get; set; }
        public RideType RideType { get; set; }
    }
}

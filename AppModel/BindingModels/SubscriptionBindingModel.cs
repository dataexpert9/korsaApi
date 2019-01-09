using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{
    public class SubscriptionBindingModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfRides { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public DurationType DurationType { get; set; }

    }


    public class FareCalculationBindingModel
    {
        public double BasicCharges { get; set; }
        public double FarePerKM { get; set; }
        public double FarePerMin { get; set; }

        public int Id { get; set; }
        public int? City_Id { get; set; }
        public int? RideType_Id { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
    }
}

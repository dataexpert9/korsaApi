using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
   public class SubscriptionPackageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfRides { get; set; }
        public double Price { get; set; }

        public int Duration { get; set; }
        public DurationType DurationType { get; set; }
    }

    public class SubscriptionPackageDTOList
    {
        public List<SubscriptionPackageDTO> SubscriptionPackagesList { get; set; }
        public string PaymentCurrency { get; set; } = "USD";

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class DriverSubscription :BaseModel
    {
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public int SubscriptionPackage_Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int RemainingRides { get; set; }
        public virtual Driver Driver { get; set; }

        public virtual SubscriptionPackage SubscriptionPackage { get; set; }
    }
}

using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class SubscriptionPackage :BaseModel
    {
        public SubscriptionPackage()
        {
            CashSubscriptions = new HashSet<CashSubscription>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfRides { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public DurationType DurationType { get; set; }
        public ICollection<CashSubscription> CashSubscriptions { get; set; }
        public virtual ICollection<DriverSubscription> DriverSubscriptions { get; set; }


    }
    
}

using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class CashSubscription : BaseModel
    {
        public CashSubscription()
        {
            Receipts = new HashSet<CashSubscriptionMedia>();
        }
        public int Id { get; set; }
        public double Amount { get; set; }
        public int RemainingRides { get; set; }
        public int PreviousPackageRides { get; set; }
        public string ImageUrl { get; set; }
        public TopUpStatus Status { get; set; }
        public PaymentMethods PaymentType { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int Driver_Id { get; set; }
        public virtual Driver Driver { get; set; }
        public bool isActive { get; set; }
        public int? Account_Id { get; set; }
        public virtual Account Account { get; set; }

        public int SubscriptionPackage_Id { get; set; }
        public virtual SubscriptionPackage SubscriptionPackage { get; set; }

        public virtual ICollection<CashSubscriptionMedia> Receipts { get; set; }

    }


}

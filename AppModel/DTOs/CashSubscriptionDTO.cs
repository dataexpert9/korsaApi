using Component.Utility;
using System;
using System.Collections.Generic;

namespace AppModel.DTOs
{
    public class CashSubscriptionDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int RemainingRides { get; set; }
        public int PreviousPackageRides { get; set; }

        public string ImageUrl { get; set; }
        public TopUpStatus Status { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Driver_Id { get; set; }
        public bool isActive { get; set; }

        public int Account_Id { get; set; }
        public virtual DriverDTO Driver { get; set; }
        public virtual AccountDTO Account { get; set; }

        public int SubscriptionPackage_Id { get; set; }
        public virtual SubscriptionPackageDTO SubscriptionPackage { get; set; }
    }
    public class CashSubscriptionDTOList
    {
        public List<CashSubscriptionDTO> CashSubscriptions { get; set; }
    }
}

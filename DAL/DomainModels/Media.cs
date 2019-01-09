using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class DriversMedia :BaseModel
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
        public int? Driver_Id { get; set; }
        public virtual Driver Driver { get; set; }
    }
    public class VehicleMedia : BaseModel
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
        public int? Vehicle_Id { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }

    public class TopUpMedia : BaseModel
    {
        public int Id { get; set; }
        public MediaType Type { get; set; } = MediaType.TopUpReciept;
        public string MediaUrl { get; set; }
        public int? TopUp_Id { get; set; }
        public virtual TopUp TopUp { get; set; }
    }


    public class CashSubscriptionMedia : BaseModel
    {
        public int Id { get; set; }
        public MediaType Type { get; set; } = MediaType.CashSubscription;
        public string MediaUrl { get; set; }
        public int? CashSubscription_Id { get; set; }
        public virtual CashSubscription CashSubscription { get; set; }
    }
}

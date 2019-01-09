using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class PaymentHistory : BaseModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int? Package_Id { get; set; }
        public double USDAmount { get; set; }
        public double LocalCurrencyAmount { get; set; }
        public int? User_Id { get; set; }
        public User User { get; set; }

        public int? Driver_Id { get; set; }
        public Driver Driver { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class CreditCard : BaseModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string Name { get; set; }
        public string CardTypeName { get; set; }
        public int? User_Id { get; set; }
        public User User { get; set; }
        public int? Driver_Id { get; set; }
        public Driver Driver { get; set; }

    }
}

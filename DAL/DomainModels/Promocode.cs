using Component.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
   public  class Promocode : BaseModel
    {
       
        public int Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public int ExpiryHours { get; set; }
        public int CodeType { get; set; }
        public int CouponType { get; set; }
        public int CouponAmount { get; set; }
        public bool IsExpired { get; set; } = false;
        public string Details { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public PromocodeType Type { get; set; }
        [ForeignKey("User")]
        public int? User_Id { get; set; }
        public User User { get; set; }
        public DateTime ActivationDate { get; set; }
        public int LimitOfUsage { get; set; }
        public int UsageCount { get; set; }


        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCode> UserPromocodes { get; set; }
    }
}


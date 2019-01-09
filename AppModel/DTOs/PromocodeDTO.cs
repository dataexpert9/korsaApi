using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class PromocodeDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public int ExpiryHours { get; set; }
        public int CodeType { get; set; }
        public int CouponType { get; set; }
        public int CouponAmount { get; set; }
        public bool IsExpired { get; set; } = false;
        public string FullName { get; set; }
        public int LimitOfUsage { get; set; }
        public int UsageCount { get; set; }
        public string Details { get; set; }


        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public PromocodeType Type { get; set; }
        public int? User_Id { get; set; }
        public UserDTO User { get; set; }

    }
    public class PromocodeDTOList
    {
        public PromocodeDTOList()
        {
            Codes = new List<PromocodeDTO>();
        }
        public List<PromocodeDTO> Codes { get; set; }
        public string ErrorMessage { get; set; }
    }
}

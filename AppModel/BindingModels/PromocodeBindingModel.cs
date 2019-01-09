using System;
using System.ComponentModel.DataAnnotations;
namespace AppModel.BindingModels
{
    public class PromocodeBindingModel
    {
        [Required]
        public string Promocode { get; set; }


    }

    public class EstimateFareBindingModel
    {
        [Required]
        public int RideType_Id { get; set; }
        [Required]
        public double DistanceInKm { get; set; }
        public int? Promocode_Id { get; set; }
    }



    public class AddPromocodeBindingModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsExpired { get; set; }
        public int CodeType { get; set; }
        public int CouponType { get; set; }
        public double CouponAmount { get; set; }
        public string Details { get; set; }

        public bool IsDeleted { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int LimitOfUsage { get; set; }

        
    }


}

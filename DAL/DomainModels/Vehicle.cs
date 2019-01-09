using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class Vehicle : BaseModel
    {
        public Vehicle()
        {
            Medias = new List<VehicleMedia>();
        }
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime RegistrationExpiry { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int Driver_Id { get; set; }
        public int Company_Id { get; set; } //Maker
        public int Model_Id { get; set; }
        public int? RideType_Id { get; set; }

        public virtual CarCompany CarCompany { get; set; }
        public virtual CarModel CarModel { get; set; }
        public virtual CarYear CarYear { get; set; }
        public virtual CarType CarType { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual CarCapacity CarCapacity { get; set; }
        public virtual RideType RideType { get; set; }
        public virtual ICollection<VehicleMedia> Medias { get; set; }
        public bool isActive { get; set; }
        public string Classification { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Capacity { get; set; }

    }
}

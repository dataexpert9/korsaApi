using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class VehicleClass
    {
        public int Id { get; set; }
        public int CarCompany_Id { get; set; }
        public int CarModel_Id { get; set; }
        public int CarYear_Id { get; set; }
        public int CarType_Id { get; set; }
        public int RideType_Id { get; set; }
        public string CarClassName { get; set; }
    }
}

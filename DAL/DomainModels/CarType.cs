using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
  
    public class CarType : BaseModel
    {
        public CarType()
        {
            CarTypeMLsList = new List<CarTypeML>();
            Vehicles = new List<Vehicle>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<CarTypeML> CarTypeMLsList { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }


    public class CarTypeML
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureType Culture { get; set; }

        [ForeignKey("CarType")]
        public int CarType_Id { get; set; }
        public CarType CarType { get; set; }
    }



}

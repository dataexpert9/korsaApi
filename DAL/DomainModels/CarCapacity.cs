using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{


    public class CarCapacity : BaseModel
    {
        public CarCapacity()
        {
            CarCapacityMLsList = new List<CarCapacityML>();
            Vehicles = new List<Vehicle>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<CarCapacityML> CarCapacityMLsList { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

    public class CarCapacityML
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureType Culture { get; set; }

        [ForeignKey("CarCapacity")]
        public int CarCapacity_Id { get; set; }
        public CarCapacity CarCapacity { get; set; }
    }
}

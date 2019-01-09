using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public class CarYear : BaseModel
    {
        public CarYear()
        {
            CarYearMLsList = new List<CarYearML>();
            Vehicles = new List<Vehicle>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<CarYearML> CarYearMLsList { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
    
    public class CarYearML
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureType Culture { get; set; }

        [ForeignKey("CarYear")]
        public int CarYear_Id { get; set; }
        public CarYear CarYear { get; set; }
    }
}

using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{

    public class CarModel : BaseModel
    {
        public CarModel()
        {
            CarModelMLsList = new List<CarModelML>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public int Company_Id { get; set; }
        public virtual ICollection<CarModelML> CarModelMLsList { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual CarCompany CarCompany { get; set; }
    }


    public class CarModelML
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureType Culture { get; set; }

        [ForeignKey("CarModel")]
        public int CarModel_Id { get; set; }
        public CarModel CarModel { get; set; }
    }

}

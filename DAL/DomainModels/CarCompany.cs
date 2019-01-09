using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public class CarCompany : BaseModel
    {
        public CarCompany()
        {
            CarCompanyMLsList = new List<CarCompanyML>();
            CarModels = new List<CarModel>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<CarCompanyML> CarCompanyMLsList { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }
    }
    

    public class CarCompanyML
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureType Culture { get; set; }

        [ForeignKey("CarCompany")]
        public int CarCompany_Id { get; set; }
        public CarCompany CarCompany { get; set; }
    }


}

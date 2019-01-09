using Component.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public partial class Country : BaseModel
    {
        public Country()
        {
            CountryMLsList = new HashSet<CountryML>();
            Cities = new HashSet<City>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<CountryML> CountryMLsList { get; set; }
    }

    public partial class CountryML : BaseModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int Country_Id { get; set; }
        public Country Country { get; set; }
    }


    public partial class City : BaseModel
    {
        public City()
        {
            CityMLsList = new List<CityML>();
            Drivers = new List<Driver>();
            Users = new List<User>();
            FareCalculations = new List<FareCalculation>();
        }
        public int Id { get; set; }

        [ForeignKey("Country")]
        public int Country_Id { get; set; }
        public bool IsActive { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<CityML> CityMLsList { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<FareCalculation> FareCalculations { get; set; }


    }

    public class CityML : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CultureType Culture { get; set; }
        [ForeignKey("City")]
        public int City_Id { get; set; }
        public City City { get; set; }
    }



}

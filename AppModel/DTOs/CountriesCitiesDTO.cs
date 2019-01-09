using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class CountryDTO
    {
        public CountryDTO()
        {
            English = new CountryMLsDTO();
            Arabic = new CountryMLsDTO();
        }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public CountryMLsDTO English { get; set; }
        public CountryMLsDTO Arabic { get; set; }
    }

    public class CountryMLsDTO
    {
        public string Name { get; set; }
    }

    public class CountryDTOList
    {
        public List<CountryDTO> Countries { get; set; }

    }



    public class CityDTO
    {
        public CityDTO()
        {
            English = new CityMLsDTO();
            Arabic = new CityMLsDTO();
            Country = new CountryDTO();
        }
        public int Id { get; set; }
        public int Branch_Id { get; set; }
        public bool IsActive { get; set; }
        public CityMLsDTO English { get; set; }
        public CityMLsDTO Arabic { get; set; }
        public CountryDTO Country { get; set; }

    }

    public class CityMLsDTO
    {
        public string Name { get; set; }
    }

    public class CitiesDTOList
    {
        public List<CityDTO> Cities { get; set; }
    }

}

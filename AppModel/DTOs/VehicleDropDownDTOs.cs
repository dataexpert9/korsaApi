using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class CarCompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarCompanyDTOList
    {
        public List<CarCompanyDTO> CarCompanies { get; set; }
    }


    public class CarModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarModelDTOList
    {
        public List<CarModelDTO> CarModels { get; set; }
    }


    public class CarYearDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarYearDTOList
    {
        public List<CarYearDTO> CarYears { get; set; }
    }


    public class CarTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarTypeDTOList
    {
        public List<CarTypeDTO> CarTypes { get; set; }
    }

    public class CarCapacityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarCapacityDTOList
    {
        public List<CarCapacityDTO> CarCapacities { get; set; }
    }

    public class VehicleAllDropDownList
    {
        public List<CarCompanyDTO> CarCompanies  { get; set; }
        public List<CarModelDTO> CarModels { get; set; }
        public List<CarCapacityDTO> CarCapacities { get; set; }
        public List<CarYearDTO> CarYears { get; set; }
        public List<CarTypeDTO> CarTypes { get; set; }
    }
}

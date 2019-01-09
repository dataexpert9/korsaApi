using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{

    public class CountryBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }


    public class CityBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }
    }
}

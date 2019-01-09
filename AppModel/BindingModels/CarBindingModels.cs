using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{
    public class CarCompanyBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
    }

    public class CarModelBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
    }

    public class CarYearBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
    }

    public class CarCapacityBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
    }

    public class CarTypeBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
    }
}

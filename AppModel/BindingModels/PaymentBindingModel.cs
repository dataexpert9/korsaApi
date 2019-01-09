using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{


    public class DriverPaymentBindingModel
    {
        public int DriverId { get; set; }
        public double Amount { get; set; }
    }


    public class DriverPaymentBindingModelList
    {
        public List<DriverPaymentBindingModel> Drivers { get; set; }
    }
}

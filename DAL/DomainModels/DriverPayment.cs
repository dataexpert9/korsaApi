using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class DriverPayment : BaseModel
    {
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public double Amount { get; set; }
        public string InvoiceUrl { get; set; }
        public virtual Driver Driver { get; set; }        
    }
}

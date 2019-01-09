using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class DriverPaymentDTO : BaseModelDTO
    {
        public DriverPaymentDTO()
        {
            Driver = new DriverDTO();
        }
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public double Amount { get; set; }
        public string InvoiceUrl { get; set; }
        public virtual DriverDTO Driver { get; set; }
    }

    public class DriverPaymentListDTO
    {
        public List<DriverPaymentDTO> DriverPayments { get; set; }
    }
}

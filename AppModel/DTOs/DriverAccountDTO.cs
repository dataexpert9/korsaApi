using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
   
    public class DriverAccountDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public double Amount { get; set; }
        public string InvoiceUrl { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public BranchDTO Branch { get; set; }
        public bool isActive { get; set; }
        //public virtual DriverDTO Driver { get; set; }
    }

    public class DriverAccountListDTO
    {

        public List<DriverAccountDTO> DriverAccounts { get; set; }
    }
}

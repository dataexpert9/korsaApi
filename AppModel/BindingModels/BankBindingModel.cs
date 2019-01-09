using Component.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class BankBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }

        public string Name { get; set; }
    }




    public class BankBranchBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int Bank_Id { get; set; }
    }



    public class TopUpRequestBindingModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public TopUpStatus Status { get; set; }
    }

    public class CashSubscriptionRequestBindingModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public TopUpStatus Status { get; set; }
    }



    public class AddBankTopUpBindingModel
    {
        [Required]
        public int Account_Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]

        public List<IFormFile> Receipts { get; set; }
    }


    public class AddBankCashSbuscriptionBindingModel
    {
        [Required]
        public int Account_Id { get; set; }
        [Required]
        public int SubscriptionPackage_Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]

        public List<IFormFile> Receipts { get; set; }
    }



    public class AccountBindingModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IBN { get; set; }
        public int Branch_Id { get; set; }
    }
}

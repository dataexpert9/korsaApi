using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public class Bank : BaseModel
    {
        public Bank()
        {
            Branches = new List<Branch>();
            BankMLsList = new List<BankML>();
        }
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<BankML> BankMLsList { get; set; }


    }

    public class BankML : BaseModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public string Name { get; set; }
        [ForeignKey("Bank")]
        public int Bank_Id { get; set; }
        public Bank Bank { get; set; }
    }



    public class Branch : BaseModel
    {
        public Branch()
        {
            Accounts = new List<Account>();
            BranchMLsList = new List<BranchML>();

        }
        public int Id { get; set; }

        [ForeignKey("Bank")]
        public int Bank_Id { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<BranchML> BranchMLsList { get; set; }


    }

    public class BranchML : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public CultureType Culture { get; set; }
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public Branch Branch { get; set; }
    }




    public class Account : BaseModel
    {
        public Account()
        {

            BankTopUps = new List<TopUp>();
            AccountMLsList = new List<AccountML>();
            CashSubscriptions = new HashSet<CashSubscription>();

        }
        public int Id { get; set; }

        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ICollection<AccountML> AccountMLsList { get; set; }
        public virtual ICollection<TopUp> BankTopUps { get; set; }
        public ICollection<CashSubscription> CashSubscriptions { get; set; }


    }


    public class AccountML : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IBN { get; set; }
        public CultureType Culture { get; set; }
        [ForeignKey("Account")]
        public int Account_Id { get; set; }
        public Account Account { get; set; }
    }



}

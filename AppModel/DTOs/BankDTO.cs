using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class BankDTO
    {
        public BankDTO()
        {
            English = new BankMLsDTO();
            Arabic = new BankMLsDTO();
        }
        public int Id { get; set; }
        public BankMLsDTO English { get; set; }
        public BankMLsDTO Arabic { get; set; }
    }

    public class BankMLsDTO
    {
        public string Name { get; set; }
    }

    public class BankDTOList
    {
        public List<BankDTO> Banks { get; set; }

    }

    public class BranchDTO
    {
        public BranchDTO()
        {
            English = new BranchMLsDTO();
            Arabic = new BranchMLsDTO();
            Bank = new BankDTO();

        }
        public int Id { get; set; }
        public int Bank_Id { get; set; }
        public BankDTO Bank { get; set; }

        public BranchMLsDTO English { get; set; }
        public BranchMLsDTO Arabic { get; set; }

    }

    public class BranchMLsDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
    }

    public class BranchDTOList
    {
        public List<BranchDTO> Branches { get; set; }

    }



    public class AccountDTO
    {
        public AccountDTO()
        {
            English = new AccountMLsDTO();
            Arabic = new AccountMLsDTO();
            Branch = new BranchDTO();
        }
        public int Id { get; set; }
        public int Branch_Id { get; set; }
        public AccountMLsDTO English { get; set; }
        public AccountMLsDTO Arabic { get; set; }
        public BranchDTO Branch { get; set; }
        
    }

    public class AccountMLsDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string IBN { get; set; }
    }

    public class AccountDTOList
    {
        public List<AccountDTO> Accounts { get; set; }

    }

}

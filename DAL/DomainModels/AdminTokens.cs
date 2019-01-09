using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class AdminTokens
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public int Admin_Id { get; set; }

        public Admin Admin { get; set; }

        public bool IsActive { get; set; }
    }
}

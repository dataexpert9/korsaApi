using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class Mailing
    {
        
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Type { get; set; }

    }
}

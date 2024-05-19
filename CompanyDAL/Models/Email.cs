using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDAL.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }

    }
}

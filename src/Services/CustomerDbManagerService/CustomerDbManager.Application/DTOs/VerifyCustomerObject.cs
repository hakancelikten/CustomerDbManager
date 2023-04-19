using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.DTOs
{
    public class VerifyCustomerObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long TCKN { get; set; }
        public int BirthDateYear { get; set; }
        public bool Verified { get; set; } = false;
    }
}

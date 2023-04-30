using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.DTOs
{
    public class CustomerRequestObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long TCKN { get; set; }
        public string BirthDate { get; set; }
    }
}

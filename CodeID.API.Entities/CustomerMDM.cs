using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeID.API.Entities
{
    public class CustomerMDM
    {
        public string CustomerName { get; set; }

        public string PriorityInformation { get; set; }

        public string EnterpriseId { get; set; }

        public string DOB { get; set; }

        public string CIF { get; set; }

        public string Email { get; set; }

        public string AccountNumber { get; set; }

        public string ProductName { get; set; }

        public string PhoneNumber { get; set; }

        public string CardNumber { get; set; }

        public string SourceSystem { get; set; }
    }
}

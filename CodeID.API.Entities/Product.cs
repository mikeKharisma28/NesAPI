using PB.Common.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeID.API.Entities
{
    public class Product : DbBaseEntity
    {
        public string EnterpriseId { get; set; }

        public string CifNumber { get; set; }

        public string AccountNumber { get; set; }

        public string CardNumber { get; set; }

        public string ProductType { get; set; }

        public string RegistrationType { get; set; }

        public string ProductName { get; set; }

        public string EmailId { get; set; }

        public bool IsRegistered { get; set; }

        public string Registration { get; set; }

        public bool IsExcluded { get; set; }

        public DateTime? ExcludedFromDate { get; set; }

        public DateTime? ExcludedToDate { get; set; }

        public string Status { get; set; }

        public string SourceSystem { get; set; }
    }
}

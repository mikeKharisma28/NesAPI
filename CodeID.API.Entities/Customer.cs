using PB.Common.DataAccess.Model;

namespace CodeID.API.Entities
{
    public class Customer : DbBaseEntity
    {
        public string PriorityInformation { get; set; }

        public string EnterpriseId { get; set; }

        public string Source_CIF { get; set; }

        public string Name { get; set; }

        public string Dob { get; set; }

        public bool? UnConsolidation { get; set; }

        public List<Product> Products { get; set; }

        public string StatementFormat { get; set; }

        public string StatementPassword { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public int Status1 { get; set; }

        public List<CustomerHistory> History { get; set; }

        public string RegistrationType { get; set; }

        public string Message { get; set; }
    }
}

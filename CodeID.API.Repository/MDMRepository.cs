using CodeID.API.Entities;
using PB.Common.DataAccess.Teradata;
using System.Data;

namespace CodeID.API.Repository
{
    public class MDMRepository
    {
        private QueryHelper _queryHelper;
        private string _databaseName;
        private int _records = 10;

        private readonly Func<IDataReader, CustomerMDM> MakeCustomer = (reader => new CustomerMDM()
        {
            CustomerName = reader["CUST_NAME"].AsString(),
            PriorityInformation = reader["PRIORITY_INFORMATION"].AsString(),
            CIF = reader["SOURCE_CIF"].AsString(),
            AccountNumber = reader["CONTRACT_NUMBER"].AsString(),
            ProductName = reader["PROD_NAME"].AsString(),
            Email = reader["EMAIL_1"].AsString(),
            DOB = reader["DATEOFBIRTH"].AsDateTime().ToString("ddMMyyyy"),
            EnterpriseId = reader["ENTERPRISE_ID"].AsString(),
            SourceSystem = reader["M_SOURCE_SYSTEM"].AsString()
        });

        public MDMRepository(string connectionString, string databaseName)
        {
            _queryHelper = new QueryHelper(connectionString);
            _databaseName = databaseName;
        }

        public List<CustomerMDM> GetCustomerByEID(string eid)
        {
            return _queryHelper.Read<CustomerMDM>("", null, MakeCustomer).ToList<CustomerMDM>();
        }
    }
}

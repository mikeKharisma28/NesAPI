using Mike.API.Entities;
using PB.Common.DataAccess.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mike.API.Repositories
{
    public class OracleRepository
    {
        private readonly QueryHelper _queryHelper;
        private readonly string _databaseName;

        private readonly Func<IDataReader, Product> CifNumbersMake = (reader => new Product()
        {
            AccountNumber = reader["IFUA_SRAN"].AsString(),
            CifNumber = reader["BUSINESS_PARTNER_CIF"].AsString()
        });

        public OracleRepository(string connectionString, string databaseName)
        {
            _queryHelper = new QueryHelper(connectionString);
            _databaseName = databaseName;
        }

        public List<Product> GetMfAccountsByCif(string cifNumbers)
        {
            List<Product> mfAccountsByCif = new List<Product>();
            if (!string.IsNullOrEmpty(cifNumbers))
                mfAccountsByCif = this._queryHelper.Read<Product>("select distinct  A.BUSINESS_PARTNER_CIF, B.IFUA_SRAN from " + _databaseName + ".DIM_BUSINESS_PARTNER A \r\n                        inner join " + _databaseName + ".FACT_WM_POSITION B on A.BUSINESS_PARTNER_NUMBER = B.BUSINESS_PARTNER_NUMBER \r\n                        where A.BUSINESS_PARTNER_CIF In (" + cifNumbers + ")\r\n                 and BUSINESS_PARTNER_CIF <> '0' and IFUA_SRAN <> 'null'\r\n                        order by A.BUSINESS_PARTNER_CIF", null, CifNumbersMake).ToList<Product>();
            return mfAccountsByCif;
        }
    }
}

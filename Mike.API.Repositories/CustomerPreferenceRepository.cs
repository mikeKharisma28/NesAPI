using Mike.API.Entities;
using Newtonsoft.Json;
using PB.Common.DataAccess;
using PB.Common.DataAccess.MsSql;
using System.Data;

namespace Mike.API.Repositories
{
    public class CustomerPreferenceRepository
    {
        private readonly QueryHelper _queryHelper;

        private readonly Func<IDataReader, Customer> Make = reader =>
        {
            Customer customer = new Customer();
            customer.Id = reader["Id"].AsString();
            customer.EnterpriseId = reader["EnterpriseID"].AsString();
            customer.Name = reader["Name"].AsString();
            customer.RegistrationType = reader["type"].AsString();
            var unconsolidation = reader["UnConsolidation"];
            customer.UnConsolidation = unconsolidation != null ? new bool?(unconsolidation.AsBool()) : new bool?();
            customer.StatementFormat = reader["StatementFormat"].AsString();
            customer.StatementPassword = reader["StatementPassword"].AsString();
            customer.History = reader.IsDBNull(reader.GetOrdinal("History")) ? null : JsonConvert.DeserializeObject<List<CustomerHistory>>(reader["History"].AsString());
            customer.Status = reader["Status"].AsString();
            customer.CreatedDate = new DateTime?(reader["CreatedDate"].AsDateTime());
            List<Product> productNewList = new List<Product>();
            Product product = new Product();
            product.AccountNumber = reader["AccountNumber"].AsString();
            var cifNumber = reader["CifNumber"];
            product.CifNumber = cifNumber != null ? cifNumber.AsString() : null;
            product.ProductName = reader["ProductName"].AsString();
            product.ProductType = reader["ProductType"].AsString();
            product.EmailId = reader["EmailId"].AsString();
            product.IsExcluded = reader["IsExcluded"].AsBool();
            product.IsRegistered = reader["IsRegistered"].AsBool();
            product.ExcludedFromDate = new DateTime?(reader["ExcludedFromDate"].AsDateTime());
            product.ExcludedToDate = new DateTime?(reader["ExcludedToDate"].AsDateTime());
            productNewList.Add(product);
            customer.Products = productNewList;

            return customer;
        };

        public List<Customer> GetCustomerByEId(string enterpriseId)
        {
            return _queryHelper.Read<Customer>("SELECT DISTINCT C.ID, C.EnterpriseID,'dummy' as type, C.Name, C.UnConsolidation, C.StatementPassword, C.StatementFormat, C.IsSelected, C.History, C.Status,C.CreatedDate,C.CreatedDate,\r\n                        P.EmailId, P.AccountNumber,null as CifNumber, P.IsRegistered, P.ProductName,P.ProductType,P.IsExcluded, P.ExcludedFromDate, P.ExcludedToDate from Customers C , Products P where C.EnterpriseId = P.EnterpriseId and C.EnterpriseId = '" + enterpriseId + "' and p.status = '1' order by c.CreatedDate DESC", null, this.Make).Result.ToList<Customer>();
        }

        public async Task<bool> CheckCustomerPendingTask(string enterpriseId)
        {
            return (int) await _queryHelper.ExecuteScalar("select \r\n                            count(c.Enterpriseid) \r\n                            from Customers c\r\n                            where\r\n                             c.enterpriseid='" + enterpriseId + "' and c.status='0'", (List<IDataParameter>) null) > 0;
        }
    }
}
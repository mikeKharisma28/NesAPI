using Mike.API.Entities;
using Mike.API.Repositories;
using NesAPI.Interfaces;

namespace NesAPI.Services
{
    public class CustomerPreferenceServices : ICustomerPreferenceServices
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;


        public CustomerPreferenceServices (ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<List<Customer>> GetCustomerDetailsByEID(string eid)
        {
            List<Customer> customerList = new List<Customer>();
            string empty = string.Empty;


            if (string.IsNullOrEmpty(eid))
                return new List<Customer>();

            List<CustomerMDM> customerDetails = new MDMRepository("", "").GetCustomerByEID(eid);
            List<string> custCifs = customerDetails.Select(x => x.CIF).Distinct().ToList();

            _logger.LogInformation("CIF Number for MF check:" + string.Join(",", custCifs));

            List<Product> mfAccounts = new OracleRepository("", "").GetMfAccountsByCif(string.Join(",", custCifs));
            List<Customer> customers = new CustomerPreferenceRepository().GetCustomerByEId(eid);
            bool flag = await new CustomerPreferenceRepository().CheckCustomerPendingTask(eid);
            if (customerDetails == null || customerDetails.Count<CustomerMDM>() == 0)
                return new List<Customer>();

            customerList.Add(new Customer()
            {
                EnterpriseId = eid,
                PriorityInformation = customerDetails[0].PriorityInformation,
                Name = customerDetails[0].CustomerName,
                Dob = customerDetails[0].DOB,
                StatementFormat = customers.Count == 0 ? "pdf" : customers[0].StatementFormat,
                StatementPassword = customers.Count == 0 ? customerDetails[0].DOB : customers[0].StatementPassword,
                UnConsolidation = customers.Count == 0 ? new bool?(false) : customers[0].UnConsolidation,
                Status = flag ? "0" : " "
            });

            if (customerList[0].Products == null)
                customerList[0].Products = new List<Product>();

            foreach (Customer customer in customers)
            {
                customerList[0].Products.Add(new Product()
                {
                    EnterpriseId = customer.EnterpriseId,
                    AccountNumber = customer.Products[0].AccountNumber,
                    IsExcluded = customer.Products[0].IsExcluded,
                    IsRegistered = customer.Products[0].IsRegistered,
                    EmailId = (customer.Products[0]?.EmailId) ?? "",
                    ProductName = !(customer.Products[0].ProductType == "RD") || customer.Products[0].ProductName != null ? customer.Products[0].ProductName : "",
                    SourceSystem = customerDetails.FirstOrDefault(x => x.AccountNumber.TrimStart('0') == customer.Products[0].AccountNumber.TrimStart('0'))?.SourceSystem,
                    ExcludedToDate = customer.Products[0].ExcludedToDate.ToString().Contains("0001") ? new DateTime?() : customer.Products[0].ExcludedToDate,
                    ExcludedFromDate = customer.Products[0].ExcludedFromDate.ToString().Contains("0001") ? new DateTime?() : customer.Products[0].ExcludedFromDate
                });
            }

            foreach (CustomerMDM customerMDM in customerDetails)
            {
                if (customerList[0].Products.Find(x => x.AccountNumber.TrimStart('0') == customerMDM.AccountNumber.TrimStart('0')) == null)
                    customerList[0].Products.Add(new Product()
                    {
                        EnterpriseId = customerMDM.EnterpriseId,
                        AccountNumber = customerMDM.AccountNumber,
                        EmailId = customerMDM.Email == null ? "" : customerMDM.Email,
                        ProductName = customerMDM.ProductName == null ? "" : customerMDM.ProductName,
                        SourceSystem = customerMDM.SourceSystem
                    });
            }

            

            return customerList;
        }
    }
}

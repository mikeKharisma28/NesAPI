using Mike.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NesAPI.Interfaces;
using NesAPI.Services;

namespace NesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetCustomer(string type, string value)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                switch(type)
                {
                    case "entid":
                        ICustomerPreferenceServices customerPreference = new CustomerPreferenceServices(_logger, _config);
                        customers = await customerPreference.GetCustomerDetailsByEID(value);
                        break;
                    case "cif":
                        break;
                    case "account":
                        break;
                    default:
                        return BadRequest("Invalid type format. Only entid, cif and account are accepted.");
                }
            }
            catch (Exception ex)
            {
                ILogger logger = _logger;
                if (logger != null)
                {
                    LoggerExtensions.LogError(logger, ex, "Error fetching customer data.");
                }
                return Problem(ex.ToString());
            }
            return Ok(string.Format("Type: {0}, value: {1}", type, value));
        }
    }
}

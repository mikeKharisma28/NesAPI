using CodeID.BOP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public object HelloWorld()
        {
            return Ok("Hello World");
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetCustomer(string type, string value)
        {

            return Ok(string.Format("Type: {0}, value: {1}", type, value));
        }
    }
}

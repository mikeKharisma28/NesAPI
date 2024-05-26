using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace NesAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpPost]
        public object Login()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing authorization header");

            string str1;
            string str2;

            try
            {
                string? authHeaders = Request.Headers["Authorization"];
                string[] strArray = new string[2];

                if (!string.IsNullOrEmpty(authHeaders))
                {
                    strArray = Encoding.UTF8.GetString(Convert.FromBase64String(AuthenticationHeaderValue.Parse(authHeaders).Parameter)).Split(new char[1]
                    {
                        ':'
                    }, 2);
                }

                str1 = strArray[0];
                str2 = strArray[1];
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid authorization header");
            }

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return BadRequest("Invalid request");
            if (!(str1 == "apitest") || !(str2 == "Mandiri@123"))
                return Unauthorized();

            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF32.GetBytes("Keepcalmdomore@123")), "HS256");
            List<Claim> claims = new List<Claim>();
            DateTime? expires = new DateTime?(DateTime.Now.AddMonths(1));
            DateTime? notBefore = new DateTime?();

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken("mandiri", "mandiriAPI", (IEnumerable<Claim>)claims, notBefore, expires, signingCredentials))
            });
        }
    }
}

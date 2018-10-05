using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace server.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/api/values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("/api/jwt")]
        public ActionResult<string> Jwt()
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity("shaun"),
                Issuer = "my-auth-server",
                Audience = "my-resource-server",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this-is-the-secret")),
                    SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1)
            };

            var jwtHandler = new JwtSecurityTokenHandler();

            // var jwt = jwtHandler.CreateEncodedJwt(descriptor);
            var token = jwtHandler.CreateToken(descriptor);
            var jwt = jwtHandler.WriteToken(token);

            return jwt;
        }
    }
}

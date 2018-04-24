//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Text;
//using System.Web.Routing;
//using System.Web.Mvc;
//using System.Web.Http;
////using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
////using System.Data.Entity;
////using System.Data.Entity.Infrastructure;
////using System.Net.Http;
////using System.Web.Http;
////using System.Web.Http.Description;

//namespace GithubWebApp.Controllers
//{
//    [Route("api/[controller]")]

//    public class AuthController : ApiController
//    {
//        //[HttpPost("token")]
//        [HttpPost]
//        public IActionResult Token()
//        {
//            var header = Request.Headers["Authorization"];
//            if (header.ToString().StartsWith("Basic"))
//            {
//                var credvalue = header.ToString().Substring("Basic".Length).Trim();
//                var usernameAndpassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credvalue));
//                var usernameAndpass = usernameAndpassenc.Split(':');
//                //check
//                if (usernameAndpass[0] == "Admin" && usernameAndpass[1] == "pass")
//                {
//                    //string tokenString = "test";
//                    //user info
//                    var claimsdata = new[] { new Claim(ClaimTypes.Name, "username") };
//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jhkfmndhggtjdjshdggjsllhshggmjjd"));
//                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
//                    //user info
//                    var token = new JwtSecurityToken(
//                         issuer: "mysite.com",
//                         audience: "mysite.com",
//                         expires: DateTime.Now.AddMinutes(1),
//                         claims: claimsdata,
//                         signingCredentials: signInCred


//                         );
//                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
//                    return Ok(tokenString);
//                }
//                else
//                {
//                    return null;
//                }

//            }
//        }
//    }
//}
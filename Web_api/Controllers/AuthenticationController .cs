
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_api.Model;

namespace Web_api.Controllers

{
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController  : ControllerBase
    {
[HttpPost("login")]
public  IActionResult Login([FromBody] Login user) {
            if (user is null) {
                return BadRequest("Invalid user request!!!");
            }
            if (user.Username == "Alex" && user.Password == "Pass1") {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List < Claim > (), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }
    }
}

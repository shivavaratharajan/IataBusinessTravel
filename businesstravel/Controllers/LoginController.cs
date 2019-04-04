using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using businesstravel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelApp.Model;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace businesstravel.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            var user = Validate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new LoginResponse { AuthToken = tokenString, CompanyId = Guid.NewGuid().ToString(), ProfileId = Guid.NewGuid().ToString()  });
                new SmsService().SendAsync(login.UserId);

            }

            return response;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Confirm(int verificationCode, string userName)
        {
            IActionResult response = Unauthorized();
            var user = userName.GetHashCode() == verificationCode;

            if (user)
            { 
                response = Ok(new LoginResponse {  CompanyId = Guid.NewGuid().ToString(), ProfileId = Guid.NewGuid().ToString() });
              
            }

        return response;
    }
        private string BuildToken(UserModel user)
        {


            var claims = new[] {
                 new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:ExpiresIn"])),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Validate(LoginRequest login)
        {
            UserModel user = null;

            // if (login.Username == "mario" && login.Password == "secret")
            {
                user = new UserModel { Name = "Shiva Varatharajan", Email = "shiva.varatharajan@united.com" };
            }
            return user;
        }
        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                //new Claim(ClaimTypes.Pol, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
    public class SmsService 
    {
        public bool SendAsync(string username)
        {

            //var twilio = new TwilioRestClient("AC3d7868b8a5470afa2bb005a53d3b85c1", "1d71f736e536a8c45f7d48b34f8465cf");

            //var message = twilio.SendMessage(
            //    "+15017122661", "+15558675310",
            //    "This is the ship that made the Kessel Run in fourteen parsecs?"
            //);

            TwilioClient.Init("AC3d7868b8a5470afa2bb005a53d3b85c1", "1d71f736e536a8c45f7d48b34f8465cf");


            var message11 = MessageResource.Create(
            body: "This is the ship that made the Kessel Run in fourteen parsecs?",
            from: new Twilio.Types.PhoneNumber("+15017122661"),
            to: new Twilio.Types.PhoneNumber("+15558675310"),
            pathAccountSid: "AC3d7868b8a5470afa2bb005a53d3b85c1"
            );

            var to = new PhoneNumber("+3124019890");
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("15017122661"),
                body: $"{username.GetHashCode()} is your BizGo Verification Code");


            return true;
        }
    }
}
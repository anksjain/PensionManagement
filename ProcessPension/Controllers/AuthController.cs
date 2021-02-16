using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProcessPension.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Authoristaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string jsonUserFile = @"C:\Users\ankit\Desktop\PensionManagementUIAlmostDoneLatest\PensionManagementUIAlmostDone\PensionManagement\ProcessPension\Users.json";
        
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       private string readFromJson()
        {
            string jsonResult;
            using (StreamReader streamReader = new StreamReader(jsonUserFile))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }
        private void writeToJson(string jsonString)
        {
            using (StreamWriter streamWriter = new StreamWriter(jsonUserFile))
            {
                streamWriter.Write(jsonString);
            }            
        }
        [HttpPost]
        [Route("login")]
        public  IActionResult Login([FromBody] LoginModel model)
        {           
            List<RegisterModel> users = new List<RegisterModel>();
            users= JsonConvert.DeserializeObject<List<RegisterModel>>(readFromJson());
            foreach (RegisterModel x in users)
            {
                
                if (model.Username.ToLower().Equals(x.Username.ToLower()) && model.Password.Equals(x.Password) && model.Aadhar.Equals(x.Aadhar))
                {
                    return Ok(GenerateJSONWebToken(model.Username, "user"));
                }
            }
            
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            List<RegisterModel> people = new List<RegisterModel>();
            people = JsonConvert.DeserializeObject<List<RegisterModel>>(readFromJson());
            var user = people.FirstOrDefault(x => x.Username.ToLower().Equals(model.Username) || x.Email.Equals(model.Email) || x.Aadhar.Equals(model.Aadhar));
            if (user == null)
            {
                people.Add(model);
                string jsonString = JsonConvert.SerializeObject(people);
                writeToJson(jsonString);
                return Ok("User created successfully!");
            }
            return BadRequest("Already Exist");
        }
        private string GenerateJSONWebToken(string userId, string userRole)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(
             issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

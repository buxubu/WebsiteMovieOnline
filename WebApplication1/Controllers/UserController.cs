using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.SMTPEmail;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly ISMTPEmail _smtpEmailServices;
        private readonly IConfiguration _configuration;
        public UserController(DbwebsiteMovieOnlineContext db, ISMTPEmail smtpEmailServices, IConfiguration configuration)
        {
            _db = db;
            _smtpEmailServices = smtpEmailServices;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountModelView model)
        {
            var user = Authenticate(model);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("user or pass not found.");
        }

        private string Generate(Account user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.LastName),
                new Claim(ClaimTypes.Surname, user.FirstName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                             _configuration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(15),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Account Authenticate(AccountModelView model)
        {
            var check = _db.Accounts.FirstOrDefault(x => x.Username.ToLower() == model.UserName.ToLower() && x.Password == model.Password);
            if(check != null)
            {
                return check;
            }

            return null;
        }

        [HttpGet("getUser")]
        [Authorize]
        public async Task<IActionResult> GetAllUser()
        {
            var get = await _db.Accounts.ToListAsync();
            return Ok(get);
        }

        /* [HttpPost("register")]
         public async Task<IActionResult> Register(Account model)
         {
             try
             {
                 await _db.Accounts.AddAsync(model);
                 await _db.SaveChangesAsync();
                 Random randomNum = new Random();
                 await _smtpEmailServices.Send(model.Email, "OTP Number", randomNum.Next().ToString());
                 return Ok(model);

             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message.ToString());
             }
         }*/


    }
}

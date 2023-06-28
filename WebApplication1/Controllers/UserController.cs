using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.Services.SMTPEmail;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly ISMTPEmail _smtpEmailServices;
        public UserController(DbwebsiteMovieOnlineContext db, ISMTPEmail smtpEmailServices)
        {
            _db = db;
            _smtpEmailServices = smtpEmailServices;
        }

        [HttpPost("register")]
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
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(Account model)
        {
            return Ok();
        }
    }
}

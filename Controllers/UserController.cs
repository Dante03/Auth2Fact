using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication9.Entities;
using WebApplication9.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        public UserController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: api/<User>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<User>
        [HttpPost("/login")]
        public async Task<IActionResult> Post([FromBody] UserLoginRequest userRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRequest.email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userRequest.password))
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
                // Aquí envía el código 2FA al usuario
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("/verify")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FAModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.Code);
                if (result)
                {
                    // Genera y devuelve el token JWT
                    await _userManager.ConfirmEmailAsync(user, model.Code);
                    return Ok("Autenticación exitosa.");
                }
            }

            return Unauthorized("Código 2FA inválido.");
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Habilitar 2FA
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            //await _emailSender.SendEmailAsync(model.Email, "Verification Code", $"Your code is: {token}");

            return Ok();
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

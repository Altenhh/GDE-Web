using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GDE.Web.Models;
using GDE.Web.Services;

namespace GDE.Web.API.v1
{
    [Route("api/v1/[controller]"), ApiController, Authorize]  
    public class LoginController : ControllerBase
    {
        private IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous, HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { error = "username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll() =>
            Ok(userService.GetAll());
    }
}
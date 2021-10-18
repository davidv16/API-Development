using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("/register")]
        public IActionResult RegisterUser()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/profile")]
        public IActionResult Profile()
        {
            return Ok();
        }

        [HttpPut]
        [Route("/profile")]
        public IActionResult UpdateProfile()
        {
            return Ok();
        }


    }
}

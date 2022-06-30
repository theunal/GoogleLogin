using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost("add")]
        public IActionResult Add(string name, string email, string password)
        {
            userService.Add(name, email, password);
            //var result = userService.CreateAccessToken(user);
            return Ok("Kullanıcı oluşturuldu.");
        }



        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(userService.GetAll());
        }


        [HttpPost("login")]
        public ActionResult Login(string email, string password)
        {
            var userToLogin = userService.Login(email, password);
            if (userToLogin.User is null)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = userService.CreateAccessToken(userToLogin.User);
            return Ok(result);

        }

    }
}

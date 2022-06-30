using Business.Abstract;
using Core.Dtos;
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
        public IActionResult Add(UserRegisterDto dto)
        {
            var result = userService.Add(dto.Name, dto.Email, dto.Password);
            return result.Success ? Ok(result) : BadRequest(result.Message);
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

using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Const;
using Cleverbit.CodingTask.Core.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.Login(userLoginDto);

                if (user == null)
                    return BadRequest();

                return Ok(user);
            }

            return BadRequest();
        }
    }
}

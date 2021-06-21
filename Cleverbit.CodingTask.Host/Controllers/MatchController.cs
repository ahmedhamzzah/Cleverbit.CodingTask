using Cleverbit.CodingTask.Bl.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService matchService;

        public MatchController(IMatchService matchService) 
        {
            this.matchService = matchService;
        }

       
        public async Task<IActionResult> GetAvailableMatch()
        {
            var userId = int.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier));

            var match = await matchService.GetAvailableMatch(userId);

            return Ok(match);
        }

    }
}

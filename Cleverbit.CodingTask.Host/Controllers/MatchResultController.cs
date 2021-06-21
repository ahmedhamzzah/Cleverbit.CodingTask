using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MatchResultController : ControllerBase
    {
        private readonly IMatchResultService matchResultService;

        public MatchResultController(IMatchResultService matchResultService)
        {
            this.matchResultService = matchResultService;

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] SubmitMatchResultDto submitMatchResultDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await matchResultService.Submit(submitMatchResultDto, userId);
            
            return Ok(result);
        }

        public async Task<IActionResult> GetMatchResult()
        {
            var result = await matchResultService.GetMatchResult();

            return Ok(result);
        }
    }
}

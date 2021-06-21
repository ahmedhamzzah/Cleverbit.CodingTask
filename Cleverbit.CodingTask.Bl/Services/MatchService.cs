using AutoMapper;
using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Cleverbit.CodingTask.Bl.Services
{
    public class MatchService : IMatchService
    {
        private readonly CodingTaskContext context;
        private readonly IMapper mapper;
        public MatchService(CodingTaskContext context
                          , IMapper mapper
                           )
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<GetAvailableMatchDto> GetAvailableMatch(int userId)
        {
            var result = await context.Matches.FirstOrDefaultAsync(match => match.StartDate <= DateTime.Now 
                                                                         && match.ExpireDate >= DateTime.Now
                                                                         && !match.MatchResult.Any(x=>x.UserId == userId)
                                                                   );
            return mapper.Map<GetAvailableMatchDto>(result);
        }

    }
}

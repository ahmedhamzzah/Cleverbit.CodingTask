using AutoMapper;
using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Bl.Services
{
    public class MatchResultService : IMatchResultService
    {
        private readonly CodingTaskContext context;
        private readonly IMapper mapper;
        public MatchResultService(CodingTaskContext context
                                 , IMapper mapper
                                )
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<int> Submit(SubmitMatchResultDto submitMatchResultDto, int userId)
        {
            var result = new MatchResult()
            {
                UserId = userId,
                GeneratedNumber = submitMatchResultDto.Value,
                MatchId = submitMatchResultDto.MatchId
            };

            context.MatcheResults.Add(result);

            return await context.SaveChangesAsync();
        }


        public async Task<List<GetMatchResultDto>> GetMatchResult()
        {
            var result = await context.WinnerOfMatchViews.ToListAsync();

            return mapper.Map<List<GetMatchResultDto>>(result);
        }

    }
}

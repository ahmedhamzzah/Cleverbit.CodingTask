using Cleverbit.CodingTask.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Bl.Interfaces
{
    public interface IMatchResultService
    {
        Task<int> Submit(SubmitMatchResultDto submitMatchResultDto, int userId);
        Task<List<GetMatchResultDto>> GetMatchResult();
    }
}

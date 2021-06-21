
using Cleverbit.CodingTask.Core.Dto;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Bl.Interfaces
{
    public interface IMatchService
    {
        Task<GetAvailableMatchDto> GetAvailableMatch(int userId);
    }
}

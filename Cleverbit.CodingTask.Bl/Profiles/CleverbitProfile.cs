using AutoMapper;
using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data.Models;

namespace Cleverbit.CodingTask.Bl.Profiles
{
    public class CleverbitProfile : Profile
    {
        public CleverbitProfile()
        {
            CreateMap<Match, GetAvailableMatchDto>();

            CreateMap<UserLoginDto, GetUserDto>();

            CreateMap<WinnerOfMatchView, GetMatchResultDto>();
        }
    }
}

using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data.Models;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Bl.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(UserLoginDto userLoginDto);
        Task<User> GetUser(GetUserDto userLoginDto);
    }
}

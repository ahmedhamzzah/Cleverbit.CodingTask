using AutoMapper;
using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Bl.Services
{
    public class UserService : IUserService
    {
        private readonly CodingTaskContext context;
        private readonly IHashService hashService;
        private readonly IMapper mapper;
        public UserService(CodingTaskContext context
                         , IHashService hashService
                         , IMapper mapper
                            )
        {
            this.context = context;
            this.hashService = hashService;
            this.mapper = mapper;
        }

        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            var mappedUser = mapper.Map<GetUserDto>(userLoginDto);

            mappedUser.Password = await hashService.HashText(userLoginDto.Password);

            var user = await GetUser(mappedUser);

            return user;
        }

        public async Task<User> GetUser(GetUserDto userLoginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userLoginDto.UserName 
                                                                 && u.Password == userLoginDto.Password
                                                               );

            return user;
        }



    }
}

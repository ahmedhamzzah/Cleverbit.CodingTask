using AutoMapper;
using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Bl.Profiles;
using Cleverbit.CodingTask.Bl.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cleverbit.CodingTask.Bl.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CleverbitProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchResultService, MatchResultService>();

            return services;
        }
    }
}

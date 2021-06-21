using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data
{
    public static class CodingTaskContextExtensions
    {
        public static async Task Initialize(this CodingTaskContext context, IHashService hashService)
        {
            await context.Database.EnsureCreatedAsync();

            var currentUsers = await context.Users.ToListAsync();

            bool anyNewUser = false;

            if (!currentUsers.Any(u => u.UserName == "User1"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User1",
                    Password = await hashService.HashText("Password1")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User2"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User2",
                    Password = await hashService.HashText("Password2")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User3"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User3",
                    Password = await hashService.HashText("Password3")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User4"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User4",
                    Password = await hashService.HashText("Password4")
                });

                anyNewUser = true;
            }

            if (anyNewUser)
            {
                await context.SaveChangesAsync(); 
            }


            var currentMatches = await context.Matches.ToListAsync();

            var anyNewMatch = false;

            if (!currentMatches.Any(u => u.MatchName == "Match1"))
            {
                context.Matches.Add(new Models.Match
                {
                    MatchName = "Match1",
                    StartDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddMinutes(20)
                });

                anyNewMatch = true;
            }

            if (!currentMatches.Any(u => u.MatchName == "Match2"))
            {
                context.Matches.Add(new Models.Match
                {
                    MatchName = "Match2",
                    StartDate = DateTime.Now.AddMinutes(20),
                    ExpireDate = DateTime.Now.AddMinutes(40)
                });

                anyNewMatch = true;
            }

            if (anyNewMatch)
            {
                await context.SaveChangesAsync();
            }


            InjectSqlView(context, "WinnerOfMatchView");

        }

        private static void InjectSqlView(CodingTaskContext context, string viewName)
        {
            var file = Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault();

            var assembly = typeof(CodingTaskContextExtensions).Assembly;

            var resource = assembly.GetManifestResourceStream(file);

            var sqlQuery = new StreamReader(resource).ReadToEnd();
            //we always delete the old view, in case the sql query has changed
            context.Database.ExecuteSqlRaw($"IF OBJECT_ID('{viewName}') IS NOT NULL BEGIN DROP VIEW {viewName} END");
            //creating a view based on the sql query
            context.Database.ExecuteSqlRaw($"CREATE VIEW {viewName} AS {sqlQuery}");
        }
    }
}

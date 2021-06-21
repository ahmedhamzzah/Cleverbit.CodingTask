using Cleverbit.CodingTask.Bl.Interfaces;
using Cleverbit.CodingTask.Core.Const;
using Cleverbit.CodingTask.Core.Dto;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Auth
{
    /// <summary>
    /// Authentication handler which handles BasicAuthentication header for API methods
    /// </summary>
    /// <seealso cref="AuthenticationHandler{AuthenticationSchemeOptions}" />
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
                return AuthenticateResult.Fail(AuthenticationConst.MissingAuthorizationHeaderError);

            string userName;
            string password;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[HeaderNames.Authorization]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                userName = credentials[0];
                password = credentials[1];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occured when reading authorization header");
                return AuthenticateResult.Fail(AuthenticationConst.InvalidAuthorizationHeaderError);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return AuthenticateResult.Fail(AuthenticationConst.InvalidUserNameOrPassword);
            }


            User user;

            try
            {
                user = await userService.GetUser(new GetUserDto()
                {
                    UserName = userName
                   ,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occured when querying user");
                return AuthenticateResult.Fail(AuthenticationConst.InternalAuthorizationError);
            }

            if (user == null)
                return AuthenticateResult.Fail(AuthenticationConst.InvalidUserNameOrPassword);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

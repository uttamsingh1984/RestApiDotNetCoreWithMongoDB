using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AspIdentityAuth.IdentityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestApiDotNetCoreWithMongoDB.Helpers;
using RestApiDotNetCoreWithMongoDB.IdentityModels;
using RestApiDotNetCoreWithMongoDB.Models;

namespace RestApiDotNetCoreWithMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private JwtIssuerOptions _jwtIssuerOptions;
        public AuthController(UserManager<AppUser> userManager, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtIssuerOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userName = credentials.UserName;
            var password = credentials.Password;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {                
                var identity = new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, userToVerify.Id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
                });

                var jwt = await Tokens.GenerateJwt(identity, credentials.UserName, _jwtIssuerOptions,  new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }

            // Credentials are invalid, or account doesn't exist
            return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));            
        }
    }
}
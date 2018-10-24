using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Starter.API.Authentication;
using Starter.Data.Context;
using Starter.Data.Entities;
using Starter.API.Models;

namespace Starter.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly IDataContext _context;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountsController(
            UserManager<UserAccount> userManager,
            IDataContext context,
            IJwtFactory jwtFactory, 
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _context = context;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Account model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new UserAccount
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return BadRequest(ModelState.AddErrors(result));

            await _context.UserProfiles.AddAsync(new UserProfile { IdentityId = userIdentity.Id });
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Account credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                return BadRequest(ModelState.AddError("login_failure", "Invalid username or password."));
            }

            var jwt = await Tokens.GenerateJwt(
                identity, 
                _jwtFactory, 
                credentials.Email, 
                _jwtOptions, 
                new JsonSerializerSettings { Formatting = Formatting.Indented });

            return Ok(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var roles = (await _userManager.GetRolesAsync(userToVerify)).ToList();
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, roles));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
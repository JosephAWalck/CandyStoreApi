using CandyStoreApi.Models;
using CandyStoreApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace CandyStoreApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CandyStoreApiContext _candyStoreApiContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityService _identityService;

        public AccountsController(CandyStoreApiContext candyStoreApiContext, UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, 
            IdentityService identityService)
        {
            _candyStoreApiContext = candyStoreApiContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityService = identityService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var identity = new IdentityUser { Email = registerUser.Email, UserName = registerUser.Email };
            var createdIdentity = await _userManager.CreateAsync(identity, registerUser.Password);

            if (!_roleManager.RoleExistsAsync("Administrator").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

                var newClaims = new List<Claim>
            {
                new("FirstName", registerUser.FirstName),
                new("LastName", registerUser.LastName)
            };

            await _userManager.AddClaimsAsync(identity, newClaims);

            if (registerUser.Role == Role.Administrator)
            {
                var role = await _roleManager.FindByNameAsync("Administrator");
                if (role != null)
                {
                    role = new IdentityRole("Administrator");
                    await _roleManager.CreateAsync(role);
                }

                await _userManager.AddToRoleAsync(identity, "Administrator");

                newClaims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else
            {
                var role = await _roleManager.FindByNameAsync("User");
                if (role == null)
                {
                    role = new IdentityRole("User");
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(identity, "User");
                newClaims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, identity.Email ?? throw new InvalidOperationException()),
                new(JwtRegisteredClaimNames.Email, identity.Email ?? throw new InvalidOperationException()),
            });

            claimsIdentity.AddClaims(newClaims);

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            var response = new AuthenticationResult(_identityService.WriteToken(token));
            return Ok(response);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user is null) return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);
            if (!result.Succeeded) return BadRequest("Couldn't sign in");

            var claims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Email ?? throw new InvalidOperationException()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException()),
            });

            claimsIdentity.AddClaims(claims);

            foreach(var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var taken = _identityService.CreateSecurityToken(claimsIdentity);

            var response = new AuthenticationResult(_identityService.WriteToken(taken));
            return Ok(response);
        }
    }

    public enum Role
    {
        Administrator,
        User
    }

    public record RegisterUser(string Email, string Password, string FirstName, string LastName, Role Role);

    public record LoginUser(string Email, string Password);

    public record AuthenticationResult(string Token);
}

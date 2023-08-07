namespace AuthApi.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using ViewModels;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ITokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("User was not registered due to failed model validation.");
                return BadRequest(ModelState);
            }

            var existed = await _userManager.FindByNameAsync(model.UserName);
            if (existed != null)
            {
                _logger.LogError("User was not registered due to existing similar name in db.");
                return BadRequest("User with this name already exists");
            }

            var user = new ApplicationUser(){
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errorString = result.Errors.Aggregate("", (s, error) => s + error.Description);
                _logger.LogError("User was not registered. Reasons: \n" + errorString);
                return BadRequest("User registration failed\n" + errorString);
            }

            if (await _roleManager.FindByNameAsync(UserRoles.User) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            
            _logger.LogInformation("User was registered");
            return Ok("User registered");
        }
        
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existed = await _userManager.FindByNameAsync(model.UserName);
            if (existed != null)
            {
                return BadRequest("User with this name already exists");
            }

            var admin = new ApplicationUser(){
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            var result = await _userManager.CreateAsync(admin, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("User registration failed");
            }
            
            if (await _roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            
            await _userManager.AddToRoleAsync(admin, UserRoles.Admin);

            return Ok("User registered");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                _logger.LogError("User was not found");
                return NotFound("User was not found");
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
            {
                _logger.LogError($"User login failed during signing in\nUser's input: UserName: {request.UserName}\n");
                return BadRequest("User login failed");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = _tokenService.CreateClaims(new ClaimsModel{
                Id = Guid.Parse(user.Id),
                UserName = user.UserName!,
                Roles = roles,
            });
            var token = _tokenService.GenerateToken(claims);
            
            _logger.LogInformation("User login finished with success, cookies were sent. Roles: " + 
                                   roles.Aggregate("", (x, s)=>x + s));
            return Ok
                ( new {username = user.UserName, roles = roles, token = token} );
        }

        [HttpGet ("SignOut")]
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

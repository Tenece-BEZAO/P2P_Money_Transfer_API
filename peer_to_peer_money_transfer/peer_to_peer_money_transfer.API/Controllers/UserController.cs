using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.Interfaces;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJwtConfig _jwtConfig;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper,
                               ILogger<UserController> logger, IJwtConfig jwtConfig)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _jwtConfig = jwtConfig;
        }  

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            _logger.LogInformation($"Registration Attempt for {register.Email}");

            var userExists = await _userManager.FindByEmailAsync(register.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            if (userExists != null)
            {
                return BadRequest("Email Already Exist");
            }

            try
            {
                var user = _mapper.Map<ApplicationUser>(register);
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                //var token = _jwtConfig.GenerateJwtToken(user);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            _logger.LogInformation($"Login Attempt for {login.UserName}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            try
            {
                if (!await _jwtConfig.ValidateUser(login))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken() } );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
} 

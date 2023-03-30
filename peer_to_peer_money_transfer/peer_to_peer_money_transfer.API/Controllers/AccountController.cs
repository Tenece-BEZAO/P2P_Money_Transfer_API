﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;
using peer_to_peer_money_transfer.Shared.Interfaces;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJwtConfig _jwtConfig;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper,
                               ILogger<AccountController> logger, IJwtConfig jwtConfig)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _jwtConfig = jwtConfig;
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO admin)
        {
            _logger.LogInformation($"Registration Attempt for {admin.Email}");

            var userExists = await _userManager.FindByEmailAsync(admin.Email);

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

                var user = _mapper.Map<ApplicationUser>(admin);
                user.Active = true;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterAdmin)}");
                return Problem($"Something went wrong in the {nameof(RegisterAdmin)}", statusCode: 500);
            }
        }

        [HttpPost("register/individual")]
        public async Task<IActionResult> RegisterIndividual([FromBody] RegisterIndividualDTO register)
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
                user.Active = true;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterIndividual)}");
                return Problem($"Something went wrong in the {nameof(RegisterIndividual)}", statusCode: 500);
            }
        }

        [HttpPost("register/business")]
        public async Task<IActionResult> RegisterBusiness([FromBody] RegisterBusinessDTO business)
        {
            _logger.LogInformation($"Registration Attempt for {business.Email}");

            var userExists = await _userManager.FindByEmailAsync(business.Email);

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

                var user = _mapper.Map<ApplicationUser>(business);
                user.Active = true;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterBusiness)}");
                return Problem($"Something went wrong in the {nameof(RegisterBusiness)}", statusCode: 500);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            _logger.LogInformation($"Login Attempt for {login.UserName}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (await _jwtConfig.ValidateUser(login))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}

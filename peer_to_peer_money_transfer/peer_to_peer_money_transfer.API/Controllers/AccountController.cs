using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.BLL.Infrastructure;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.Shared.DataTransferObject;
using peer_to_peer_money_transfer.Shared.Interfaces;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("CashMingle/[controller]")]
    public partial class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GenerateAccountNumber _accountNumber;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJwtConfig _jwtConfig;
        private readonly IEmailSender _emailSender;
        private readonly ISendSms _sendSms;
        private readonly IAdmin _admin;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GenerateAccountNumber accountNumber,
                                 IMapper mapper, ILogger<AccountController> logger, IJwtConfig jwtConfig,  IEmailSender emailSender, ISendSms sendSms, IAdmin admin)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountNumber = accountNumber;
            _mapper = mapper;
            _logger = logger;
            _jwtConfig = jwtConfig;
            _emailSender = emailSender;
            _sendSms = sendSms;
            _admin = admin;
        }

        /*[Authorize(Policy = "SuperAdmin")]*/
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

            var user = _mapper.Map<ApplicationUser>(admin);
            user.UserTypeId = UserType.Admin;
            await _userManager.AddToRoleAsync(userExists, "Admin");

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                return BadRequest("Admin Registration Failed");
            }

            await VerifyMailAsync(user);

            return Accepted(new { Message = "A confirmation link has been sent to you. Please verify your EmailAddress to proceed",
                                  Token = await _jwtConfig.GenerateJwtToken(user) } );
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

            var user = _mapper.Map<ApplicationUser>(register);
            user.UserTypeId = UserType.Indiviual;
            user.AccountNumber = await _accountNumber.GenerateAccount();
            await _userManager.AddToRoleAsync(userExists, "User");

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                return BadRequest("User Registration Failed");
            }

            await VerifyMailAsync(user);

            return Accepted(new { Message = "A confirmation link has been sent to you. Please verify your EmailAddress to proceed",
                                  Token = await _jwtConfig.GenerateJwtToken(user) });
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

            var user = _mapper.Map<ApplicationUser>(business);
            user.UserTypeId = UserType.Corporate;
            user.AccountNumber = await _accountNumber.GenerateAccount();
            await _userManager.AddToRoleAsync(userExists, "User");

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                return BadRequest("User Registration Failed");
            }

            await VerifyMailAsync(user);

            return Accepted(new { Message = "A confirmation link has been sent to you. Please verify your EmailAddress to proceed",
                                  Token = await _jwtConfig.GenerateJwtToken(user) });
        }

        private async Task VerifyMailAsync(ApplicationUser user)
        {
            //Add Token to verify email
            var token = _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            await SendMailAsync(user.Email, "CashMingle- ResetPassword", $"Click the link to verify your email {confirmationLink}");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { error = "User does not exist...register to continue" });
            }

            var confirmEmail = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirmEmail.Succeeded)
            {
                return BadRequest(new { error = "Email verification failed" });
            }

            return Ok(new { result = "Email verification successfull" });

        }
    }
}

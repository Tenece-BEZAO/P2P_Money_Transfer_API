using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;
using peer_to_peer_money_transfer.Shared.Interfaces;
using peer_to_peer_money_transfer.Shared.SmsConfiguration;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJwtConfig _jwtConfig;
        private readonly IEmailSender _emailSender;
        private readonly ISendSms _sendSms;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper,
                                 ILogger<AccountController> logger, IJwtConfig jwtConfig,  IEmailSender emailSender, ISendSms sendSms)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _jwtConfig = jwtConfig;
            _emailSender = emailSender;
            _sendSms = sendSms;
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

                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                await _userManager.AddToRoleAsync(user, "Administrator");

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken(user) });
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
          
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                await _userManager.AddToRoleAsync(user, "User");

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken(user) });
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

                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                {
                    return BadRequest("User Registration Failed");
                }

                await _userManager.AddToRoleAsync(user, "User");

                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken(user) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterBusiness)}");
                return Problem($"Something went wrong in the {nameof(RegisterBusiness)}", statusCode: 500);
            }
        }

        /* private async Task VerifyMailAsync(ApplicationUser user)
         {
             //Add Token to verify email
             var token = _userManager.GenerateEmailConfirmationTokenAsync(user);
             var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
             var message = new Message(new string[] { user.Email }, "CashMingle -- EMAIL CONFIRMATION LINK", confirmationLink);
             await _emailSender.SendEmailAsync(message);

             if (user.EmailConfirmed == true)
             {
                 user.Activated = true;
             }
         }*/

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

        [HttpGet("verify-phoneNo")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPhoneNumberAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return BadRequest(new { error = "user does not exist" });
            }

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

            if (token == null)
            {
                return BadRequest(new { error = "Could not send verification token...please try again" });
            }
            var model = new SmsModel() { Receiver = user.PhoneNumber, MessageBody = token };

            await _sendSms.SendSmsAsync(model);
            return Ok(new { result = "Please check your phone for your verification code" });
        }

        [HttpPost("verify-phoneNo")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPhoneNumberAsync(string userName, string token)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(new { error = "user does not exist" });
            }

            var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);
            if (result == null)
            {
                return BadRequest(new { error = "Verification failed...please try again" });
            }
            return Ok(new { result = "Phone number verification successful" });
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

            var existingUser = await _userManager.FindByNameAsync(login.UserName);

            if (existingUser == null)
            {
                return BadRequest(new { error = "User does not exist" });
            }

           /* if (existingUser.Activated != true)
            {
                await VerifyMailAsync(existingUser);
            }*/

            try
            {
                var signIn = await _userManager.CheckPasswordAsync(existingUser, login.PasswordHash);

                if (!signIn)
                {
                    return Unauthorized();
                }


                if (existingUser.TwoFactorEnabled)
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(existingUser, "CashMingle");
                    //await _smsSender.SendSmsAsync(existingUser.PhoneNumber, token);

                    return RedirectToAction("VerifyOTP", "AccountController", new VerifyOtpModel() { User = existingUser, Token = token });
                }


                return Accepted(new { Token = await _jwtConfig.GenerateJwtToken(existingUser) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        [HttpPost("verify-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyOTP(VerifyOtpModel model)
        {
            var result = await _signInManager.TwoFactorSignInAsync("CashMingle", model.Token, false, false);

            if (result == null)
            {
                return BadRequest(new { error = "signIn failed" });
            }

            return Accepted(new { Token = await _jwtConfig.GenerateJwtToken(model.User) });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { error = "User does not exist...register to continue" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (token == null)
            {
                return BadRequest(new { error = "Password reset link failed...please try again" });
            }

           /* var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "CashMingle -- RESET PASSWORD LINK", forgotPasswordLink);
            await _emailSender.SendEmailAsync(message);*/

            return Ok(new { result = "Password reset link sent...please check your mail" });
        }

        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPassword { Token = token, Email = email };

            return Ok(new { model });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            var reset = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!reset.Succeeded)
            {
                return BadRequest(new { error = "Password reset failed" });
            }

            return Ok(new { result = "Password reset successfull" });
        }

        [HttpPost("send-text")]
        [AllowAnonymous]
        public async Task<IActionResult> SendText(SmsModel model)
        {
            await _sendSms.SendSmsAsync(model);

            return Ok(new { result = "sms sent successfully" });

        }

        [HttpPost("e--mail")]
        public async Task<IActionResult> EmailMe(string emailAdress, string message)
        {
            await _emailSender.SendEmailAsync(emailAdress, message);
            return Ok(new { result = "Email sent Successfully" });
        }
    }
}

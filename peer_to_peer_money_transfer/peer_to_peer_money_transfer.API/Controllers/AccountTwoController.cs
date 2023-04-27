using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.Shared.EmailConfiguration;
using peer_to_peer_money_transfer.Shared.SmsConfiguration;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace peer_to_peer_money_transfer.API.Controllers
{
    public partial class AccountController : ControllerBase
    {
        [HttpGet("verify-phone-no")]
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
            var model = new SmsModel() { ReceiversPhoneNumber = user.PhoneNumber, MessageBody = token };

            await _sendSms.SendSmsAsync(model);
            return Ok(new { result = "Please check your phone for your verification code" });
        }

        [HttpPost("verify-phone-no")]
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
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            string pattern = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$";
            Regex regex = new(pattern);

            _logger.LogInformation($"Login Attempt for {login.EmailAddressOrUserName}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isEmail = regex.IsMatch(login.EmailAddressOrUserName);

            var existingUser = isEmail == true ? await _userManager.FindByEmailAsync(login.EmailAddressOrUserName)
                               : await _userManager.FindByNameAsync(login.EmailAddressOrUserName);

            if (existingUser == null)
            {
                return BadRequest(new { error = "User does not exist" });
            }

            if (existingUser.EmailConfirmed == true)
            {
                existingUser.Activated = true;
                await _admin.ResetCount(existingUser.UserName);
            }

            if (existingUser.Activated != true)
            {
                await VerifyMailAsync(existingUser);
                return BadRequest(new
                {
                    error = "Email Address not verified, please verify your Email Address to proceed",
                    Message = "A confirmation link has been sent to you. Please verify your EmailAddress to proceed"
                });
            }

            if (existingUser.TwoFactorEnabled)
            {
                var token = await _userManager.GenerateTwoFactorTokenAsync(existingUser, "CashMingle");
                var model = new SmsModel() { ReceiversPhoneNumber = existingUser.PhoneNumber, MessageBody = token };
                await _sendSms.SendSmsAsync(model);

                return RedirectToAction("VerifyOTP", "AccountController", new VerifyOtpModel() { UserName = existingUser.UserName, Token = token });
            }

            // Deactivate user account if incorrect password is entered multiple times
            return await PasswordVerification(existingUser.UserName, login.Password);
        }

        [HttpPost("password-verification")]
        public async Task<IActionResult> PasswordVerification(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user.AccessFailedCount >= 3)
            {
                await _admin.LockCustomer(user.UserName);

                /*await VerifyMailAsync(existingUser);*/

                return BadRequest(new { error = "Password trial limit exceeded, please verify the link sent to your Mail to verify your identity",
                                        Message = "A confirmation link has been sent to you. Please verify to proceed" });
            }

            var signIn = await _userManager.CheckPasswordAsync(user, password);

            if (!signIn)
            {
                await _admin.AccessFailedCount(userName);
                return Unauthorized(new { error = "Incorrect Password, please enter your correct password" });
            }

            await _admin.ResetCount(userName);
            return Accepted(new { Message = "Login successful", Token = await _jwtConfig.GenerateJwtToken(user) });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP(VerifyOtpModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var result = await _signInManager.TwoFactorSignInAsync("CashMingle", model.Token, false, false);

            if (result == null)
            {
                return BadRequest(new { error = "signIn failed" });
            }

            return Accepted(new { Message = "Login successful", Token = await _jwtConfig.GenerateJwtToken(user) });
        }

        [HttpPost("forgot-password")]
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

            var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            await SendMailAsync(user.Email, "CashMingle- ResetPassword", $"Click the link to reset your password - {forgotPasswordLink}");

            return Ok(new { result = "Password reset link sent...please check your mail" });
        }

        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPassword { Token = token, Email = email };

            return Ok(new { model });
        }

        [HttpPost("reset-password")]
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
        public async Task<IActionResult> SendText(SmsModel model)
        {
            await _sendSms.SendSmsAsync(model);

            return Ok(new { result = "sms sent successfully" });

        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendMailAsync(string receipentEmailAdress, string subject, string message)
        {
            var mail = new Message(new string[] { $"{receipentEmailAdress}" }, $"{subject}", $"{message}");
            await _emailSender.SendEmailAsync(mail);
            return Ok(new { result = "Email sent Successfully" });
        }
    }
}

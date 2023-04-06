using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using peer_to_peer_money_transfer.Shared.Interfaces;
using System.Text;

namespace peer_to_peer_money_transfer.Shared.EmailConfiguration
{
    public class EmailSender : IEmailSender
    {
        private readonly IFluentEmail _email;
        private readonly ILogger _logger;

        public EmailSender(IFluentEmail email, ILogger<EmailSender> logger)
        {
            _email = email;
            _logger = logger;
        }

        public async Task SendEmailAsync(string emailAdress, string message)
        {
            StringBuilder emailTemplate = new();
            emailTemplate.AppendLine("<h2>cashMingle --Please click the link below to verify your email</h2>");
            emailTemplate.AppendLine("<p>@Model.Message</p>");
            emailTemplate.AppendLine("<p>from cashMingle</p>");

            var newEmail = _email
                //.SetFrom()
                .To(emailAdress)
                //.To(emailAdress, Name)
                .Subject("<h2>cashMingle --Please click the link below to verify your email</h2>")
                .UsingTemplate(emailTemplate.ToString(), new { Message = message });

            await newEmail.SendAsync();
            _logger.LogError($"{message} sent successfully to {emailAdress}");
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using peer_to_peer_money_transfer.Shared.EmailConfiguration;

namespace peer_to_peer_money_transfer.BLL.Extensions
{
    public static class EmailExtension
    {
        public static void ConfigureEmailServices(this IServiceCollection services)
        {
            IConfiguration config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }

            //var emailConfig = config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            var emailConfig = config.GetSection("EmailConfiguration");
            //var from = emailConfig.GetSection("From").Value;
            var from = "rabbiincode@gmail.com";
            //var server = emailConfig.GetSection("SmtpServer").Value;
            var server = "smtp.gmail.com";
            var port = emailConfig.GetSection("Port").Value;
            //var userName = emailConfig.GetSection("Username").Value;
            var userName = "rabbiincode@gmail.com";
            //var password = emailConfig.GetSection("Password").Value;
            var password = "ifchetxvlxumdenc";


            services.AddFluentEmail(from).AddRazorRenderer().AddSmtpSender(server, 587, userName, password);

            /*services.AddSingleton(emailConfig);

            services.AddControllers();*/
        }
    }
}

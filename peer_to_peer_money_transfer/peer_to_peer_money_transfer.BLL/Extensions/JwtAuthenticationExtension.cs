using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace peer_to_peer_money_transfer.BLL.Extensions
{
    public static class JwtAuthenticationExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            IConfiguration config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }

            var jwtValues = config.GetSection("Jwt");


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(jwt =>
                 {

                     var key = jwtValues.GetSection("Key").Value;
                     var issuer = jwtValues.GetSection("Issuer").Value;
                     var encodeKey = Encoding.UTF8.GetBytes(key);

                     jwt.SaveToken = true;
                     jwt.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateIssuerSigningKey = true,
                         ValidateLifetime = true,
                         ValidIssuer = issuer,
                         IssuerSigningKey = new SymmetricSecurityKey(encodeKey),
                         ValidateAudience = false, //dev env
                         RequireExpirationTime = true,

                     };
                 });
        }
    }
}

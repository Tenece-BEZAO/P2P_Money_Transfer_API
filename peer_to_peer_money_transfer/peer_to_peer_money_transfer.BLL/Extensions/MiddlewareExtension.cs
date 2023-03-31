using peer_to_peer_money_transfer.DAL.Context;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.DAL.Implementation;

using Microsoft.Extensions.DependencyInjection;
using peer_to_peer_money_transfer.BLL.Implementation;
using peer_to_peer_money_transfer.BLL.Interfaces;

using Microsoft.AspNetCore.Identity;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.Interfaces;
using peer_to_peer_money_transfer.Shared.JwtConfigurations;

namespace peer_to_peer_money_transfer.BLL.Extensions
{
    public static class MiddlewareExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
           
            services.AddTransient<IUnitOfWork, UnitOfWork<ApplicationDBContext>>();
          
            services.AddTransient<ITransactionServices, TransactionServices>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IJwtConfig, JwtConfig>();
            services.AddScoped<IAdmin, Admin>();
            services.AddScoped<IFundingService, FundingService>();
            
        }
    }
}

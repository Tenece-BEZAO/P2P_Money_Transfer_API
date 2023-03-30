using peer_to_peer_money_transfer.DAL.Context;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.DAL.Implementation;

using Microsoft.Extensions.DependencyInjection;
using peer_to_peer_money_transfer.BLL.Implementation;
using peer_to_peer_money_transfer.BLL.Interfaces;

using Microsoft.AspNetCore.Identity;


namespace peer_to_peer_money_transfer.BLL.Extensions
{
    public static class MiddlewareExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
           
            services.AddTransient<IUnitOfWork, UnitOfWork<ApplicationDBContext>>();
            //services.AddTransient<IServiceFactory, ServiceFactory>();
          
            //services.AddTransient<Interfaces.IAuthenticationService, Implementation.AuthenticationService>();
            services.AddTransient<ITransactionServices, TransactionServices>();
           
            services.AddScoped<IFundingService, FundingService>();
            
        }
    }
}

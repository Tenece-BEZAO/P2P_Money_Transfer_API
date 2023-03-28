using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using peer_to_peer_money_transfer.DAL.Context;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.BLL.Implementation;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.DAL.Implementation;

namespace peer_to_peer_money_transfer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IFundingService, FundingService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDBContext>>();
            builder.Services.AddAutoMapper(Assembly.Load("peer_to_peer_money_transfer.DAL"));
            string connectionString = builder.Configuration.GetConnectionString("DefaultConn");
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConn")));
            //builder.Services.AddScoped < IPayStackApi,PayStackApi(builder.Configuration.GetSection("ApiSecret")["SecretKey"])>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
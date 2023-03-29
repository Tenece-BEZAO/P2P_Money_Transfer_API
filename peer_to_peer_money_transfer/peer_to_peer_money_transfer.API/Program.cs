
using peer_to_peer_money_transfer.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using peer_to_peer_money_transfer.DAL.Entities;
using System.Text;
using Microsoft.OpenApi.Models;
using peer_to_peer_money_transfer.Shared.Interfaces;
using peer_to_peer_money_transfer.Shared.JwtConfigurations;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Extensions;
//using peer_to_peer_money_transfer.DAL.Context;
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
            var jwtValues = builder.Configuration.GetSection("Jwt");

            var connectionString = builder.Configuration.GetConnectionString("DefaultConn") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings");

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));

            /*builder.Services.Configure<JwtConfig>(Key);*/

            /*builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDBContext>()
                    .AddDefaultTokenProviders();*/
              
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

           
            builder.Services.AddAutoMapper(Assembly.Load("peer_to_peer_money_transfer.DAL"));
            builder.Services.AddHttpContextAccessor();// Ben added

            builder.Services.RegisterServices();// Ben added

            builder.Services.AddDatabaseConnection();// Ben added

            //builder.Services.AddScoped < IPayStackApi,PayStackApi(builder.Configuration.GetSection("ApiSecret")["SecretKey"])>();

            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped<IJwtConfig, JwtConfig>();

            builder.Services.AddAuthentication(options =>
             {
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
                 .AddJwtBearer(jwt =>
                 {
                     /*var key = Environment.GetEnvironmentVariable("Key");
                       var issuer = Environment.GetEnvironmentVariable("Issuer");*/
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

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the bearer scheme
                                    Please Enter 'Bearer' [space] and then your token
                                    Example: Bearer 123456",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new string[]{}
                    }
                });

                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "CashMingle", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
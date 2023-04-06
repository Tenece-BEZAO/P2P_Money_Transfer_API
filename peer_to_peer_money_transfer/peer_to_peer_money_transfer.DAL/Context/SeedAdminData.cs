using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Context
{
    public class SeedAdminData
    {
        public class SeedAdmin
        {
            private const string _adminPassword = "AdminUser$%0@admin6<o>";
            private static readonly string _userId = Guid.NewGuid().ToString();
            public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
            {
                IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
                ApplicationDBContext context = serviceProvider.GetRequiredService<ApplicationDBContext>();
                UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
                if (!await context.Users.AnyAsync())
                {
                    var createUser = await userManager.CreateAsync(Admin(), _adminPassword);
                    string? role = Admin().UserRole.GetStringValue();
                    if (!createUser.Succeeded)
                    {
                        string errorMessage = createUser.Errors.FirstOrDefault().Description;
                        Console.WriteLine($"Error occured while trying to create the user. {errorMessage}");
                    }
                    bool roleExist = await roleManager.RoleExistsAsync(role);
                    if (roleExist)
                        await userManager.AddToRoleAsync(Admin(), role);
                    else
                        await roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine("User created successfully.");
                }
            }

            private static ApplicationUser Admin()
            {
                return new ApplicationUser
                {
                    Id = "super-admin",
                    FirstName = "cashMingleAdministrator",
                    LastName = "cashMingleAdministrator",
                    UserName = "superAdministrator",
                    PasswordHash = "123@Aa",
                    Email = "cashmingle@gmail.com",
                    PhoneNumber = "+2348080000000",
                    Address = "cashmingleaddress",
                    NIN = "12345678900",
                    UserTypeId = UserType.SuperAdmin,
                    UserRole = UserRole.SuperAdmin
                };
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.DAL.Context
{
    public static class SeededData
    {
        //An extension method on the ModelBuilder type
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
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
                  
                    UserTypeId = Enums.UserType.Admin
                }
            );

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole{ Name = "Administrator" },
                new ApplicationRole{ Name = "User" }
            );

           /* modelBuilder.Entity<Claim>().HasData(
                new Claim { Type = "AdminType", Value = "SuperAdmin" }
            );*/
        }

    }
    
}

using Microsoft.EntityFrameworkCore;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Context
{
    public static class SeedAdminData
    {
        //An extension method on the ModelBuilder type
        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "administrator",
                    FirstName = "cashMingleAdministrator",
                    LastName = "cashMingleAdministrator",
                    UserName = "superAdministrator",
                    PasswordHash = "123@Aa",
                    Email = "cashmingle@gmail.com",
                    PhoneNumber = "+2348080000000",
                    Address = "cashmingleaddress",
                    NIN = "12345678900",
                    UserTypeId = UserType.SuperAdmin
                }
           );
        }
    }
}

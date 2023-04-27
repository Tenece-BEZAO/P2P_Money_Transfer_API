using Microsoft.EntityFrameworkCore;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Context
{
    public static class SeedUsersData
    {

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "user1",
                    FirstName = "user1",
                    LastName = "user1",
                    UserName = "user1",
                    PasswordHash = "123@Aa",
                    Email = "cashmingle1@gmail.com",
                    PhoneNumber = "+2348080000000",
                    Address = "cashmingleaddress",
                    NIN = "12345678900",
                    AccountNumber = "1234567890",
                    UserTypeId = UserType.Indiviual
                },
                new ApplicationUser
                {
                    Id = "user2",
                    FirstName = "user2",
                    LastName = "user2",
                    UserName = "user2",
                    PasswordHash = "123@Aa",
                    Email = "cashmingle2@gmail.com",
                    PhoneNumber = "+2348080000000",
                    Address = "cashmingleaddress",
                    NIN = "12345678900",
                    AccountNumber = "1234567891",
                    UserTypeId = UserType.Corporate
                },
                new ApplicationUser
                {
                    Id = "user3",
                    FirstName = "user3",
                    LastName = "user3",
                    UserName = "user3",
                    PasswordHash = "123@Aa",
                    Email = "cashmingle3@gmail.com",
                    PhoneNumber = "+2348080000000",
                    Address = "cashmingleaddress",
                    NIN = "12345678900",
                    AccountNumber = "1234567892",
                    UserTypeId = UserType.Admin
                }
           );
        }
    }
}

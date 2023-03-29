using System;
using Microsoft.AspNetCore.Identity;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;

namespace peer_to_peer_money_transfer.BLL.Infrastructure
{
    public class GenerateAccountNumber
    {
        private readonly IRepository<ApplicationUser> _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateAccountNumber(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }
        
        public async Task<string> GenerateAccount()
        {
            start:
            const string Number = "37";
            Random random = new Random();

            var randomNumber = random.Next(10000000, 20000000);

            string accountNumber = Number + randomNumber.ToString() ;

            var accountExists = await _userRepo.AnyAsync(a => a.AccountNumber == accountNumber);

            switch (accountExists)
            {
                case true:
                    goto start;
                default:
                    return accountNumber;
            }

           

        }

        


       
    }
}


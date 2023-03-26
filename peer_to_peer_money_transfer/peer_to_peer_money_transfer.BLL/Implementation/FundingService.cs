using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;

using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Interfaces;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    public class FundingService : IFundingService
    {
        private IPayStackApi _payStack;
        public FundingService(IPayStackApi payStackApi)
        {
                _payStack = payStackApi;
        }
        public Task FundAccount()
        {
        
           string connectionString = (string) ConfigurationManager.GetSection("ApiSecret/SecretKey");
            PayStackApi payStack = new(connectionString);
           
            throw new NotImplementedException();
        }

        public Task<bool> MakePayment()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateWallet(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPayment()
        {
            throw new NotImplementedException();
        }
    }
}

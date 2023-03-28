using System;
using System.Collections.Generic;
using System.Configuration;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    public class FundingService : IFundingService
    {
        static IConfiguration _configuration;
       
        IMapper _mapper;
        IRepository<ApplicationUser> _userRepoService;
        IUnitOfWork _unitOfWork;
        public FundingService(IConfiguration configuration,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepoService = _unitOfWork.GetRepository<ApplicationUser>();
        }
        public async Task FundAccount(DepositRequest depositRequest)
        {

           var  user = await _userRepoService.GetByIdAsync(depositRequest.CurrentUserId);
            if (user == null)
            {

            }
            if (!user.Active)
            {

            }
            

           
            throw new NotImplementedException();
        }

        public TransactionInitializeResponse MakePayment(DepositRequest depositRequest)
        {
             string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionInitializeRequest initializeRequest = _mapper.Map<TransactionInitializeRequest>(depositRequest);
           var result = payStack.Transactions.Initialize(initializeRequest);
            return result;
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

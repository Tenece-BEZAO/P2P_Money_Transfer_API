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
        public async Task<string> FundAccount(DepositRequest depositRequest,string reference)
        {

           var  user = await _userRepoService.GetByIdAsync(depositRequest.CurrentUserId);
            if (user == null)
            {
                throw new InvalidOperationException("User Not Found");
            }
            if (!user.Activated)
            {
                throw new InvalidOperationException("User has been deactivated");
            }
            if (!user.Verified)
            {
                throw new InvalidOperationException("Account Not Verifed, Verify account to continue");
            }
            if (!VerifyPayment(reference))
            {
                throw new InvalidOperationException("Payment Wasn't successful");
            }
            var amount = depositRequest.AmountInKobo / 100 - depositRequest.TransactionCharge;
            user.Balance += amount;
            await _unitOfWork.SaveChangesAsync();
            return $"Account funded with {amount} Account balance";
        }

        public TransactionInitializeResponse MakePayment(DepositRequest depositRequest)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionInitializeRequest initializeRequest = _mapper.Map<TransactionInitializeRequest>(depositRequest);
            var result = payStack.Transactions.Initialize(initializeRequest);
            return result;
        }

       

        public bool VerifyPayment(string referenceCode)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);
            return result.Status;
          
        }
    }
}

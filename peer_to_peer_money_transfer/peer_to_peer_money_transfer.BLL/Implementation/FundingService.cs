using AutoMapper;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;
using Response = peer_to_peer_money_transfer.DAL.Dtos.Responses.ResponseStatus;

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
        public async Task<Response> FundAccount(string currentUserId,string reference)
        {
            var verification = VerifyPayment(reference);

           var  user = await _userRepoService.GetByIdAsync(currentUserId);
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
            if (!verification.Status)
            {
                throw new InvalidOperationException("Payment Wasn't successful");
            }
            var amount = verification.Data.Amount / 100 - (int)verification.Data.Fees;
            user.Balance += amount;
            await _unitOfWork.SaveChangesAsync();
            return new Response { Success = true, Data = $"Account funded with {amount} Account balance is {user.Balance}" };
        }

        public TransactionInitializeResponse MakePayment(DepositRequest depositRequest)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionInitializeRequest initializeRequest = _mapper.Map<TransactionInitializeRequest>(depositRequest);
            var result = payStack.Transactions.Initialize(initializeRequest);
            return result;
        }  

        public TransactionVerifyResponse VerifyPayment(string referenceCode)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);
            return result;
          
        }
    }
}

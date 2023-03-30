using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.DAL.Extensions;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _unitOfWork;
       
        private readonly IRepository<TransactionHistory> _transactionHistoryRepo;
        private readonly IRepository<Complains> _complainRepo;
        private readonly IRepository<ApplicationUser> _userProfileRepo;
        private readonly IHttpContextAccessor _contextAccessor;
      
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionServices( IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
          
            _transactionHistoryRepo = _unitOfWork.GetRepository<TransactionHistory>();
            _complainRepo = _unitOfWork.GetRepository<Complains>();
            _userProfileRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<Response> FileComplainAsync(ComplainRequest complainRequest)
        {
            string? _userId = _contextAccessor.HttpContext?.User.GetUserId();

            var User = await _userProfileRepo.GetSingleByAsync(a => a.Id == _userId);

            if (User == null) throw new InvalidOperationException("Account not found");

            var complain = new Complains
            {
                UserId = _userId,
                TransationId = complainRequest.TransationId,
                ComplainSubject = complainRequest.ComplainSubject,
                ComplainDescription = complainRequest.ComplainDescription,
                Isrevised = false
            };

            await _complainRepo.AddAsync(complain);
            var Check = await _unitOfWork.SaveChangesAsync();
            return Check > 0 ? new Response { Success = true, Data = $"Task: Complain was Sent" } : throw new InvalidOperationException(" Complain was not Sent!");
        }

        public async Task<Response> GetBalanceAsync(AccountNumberRequest AccountNumber)
        {
            var User = await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == AccountNumber.AccountNumber);

            if (User != null)
                return new Response { Success = true, Data = User.Balance };

            throw new InvalidOperationException("Account not found");
        }

        public async Task<ReceiverNameResponse> GetReceiverNameAsync(AccountNumberRequest AccountNumber)
        {
            var User =  await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == AccountNumber.AccountNumber);

            if (User != null)
                return new ReceiverNameResponse{            		    
		        ReceiverFullName = $"{ User.FirstName} {User.MiddleName} {User.LastName}"
		        };

            throw new InvalidOperationException("Account not Found");
            
        }

        public async Task<TransactionHistoryResponse> GetTransactionHistoriesAsync()
        {
            string? _userId = _contextAccessor.HttpContext?.User.GetUserId();

            var Transactions = await _transactionHistoryRepo.GetByAsync(a => a.UserId == _userId);

            TransactionHistoryResponse transactionreponse = new TransactionHistoryResponse();

            if (Transactions == null) throw new InvalidOperationException("No Transaction found");

            foreach (var transactionHistory in Transactions)
            {
                 transactionreponse = new TransactionHistoryResponse
                {
                    Date = transactionHistory.DateStamp,
                    Amount = transactionHistory.Amount,
                    TransactionType = transactionHistory.TransactionType,
                    Description = transactionHistory.Description,
                };
               
            }

            return transactionreponse;
        }

        public decimal GetTranscationFee(UserType userType, decimal Amount)
        {
            switch (userType)
            {
                case UserType.Corporate:
                    return CorporateTranscationFees(Amount);
                default:
                    return IndiviualTranscationFees(Amount);
            }


        }

        private static decimal IndiviualTranscationFees(decimal Amount)
        {
            if (Amount < 1000)
                return 10;
            if (Amount < 10000)
                return 20;
            if (Amount < 50000)
                return 30;
            if (Amount < 100000)
                return 40;
            if (Amount < 200000)
                return 50;
            if (Amount < 300000)
                return 70;
            if (Amount < 400000)
                return 80;
            if (Amount < 500000)
                return 90;
            if (Amount < 600000)
                return 100;
            if (Amount < 700000)
                return 110;
            if (Amount < 800000)
                return 120;
            if (Amount < 900000)
                return 130;
            //if (Amount < 1000000)
            //    return 140;
            return 150;
        }

        private static decimal CorporateTranscationFees(decimal Amount)
        {
            if (Amount < 1000)
                return 15;
            if (Amount < 10000)
                return 25;
            if (Amount < 50000)
                return 35;
            if (Amount < 100000)
                return 45;
            if (Amount < 200000)
                return 55;
            if (Amount < 300000)
                return 75;
            if (Amount < 400000)
                return 85;
            if (Amount < 500000)
                return 95;
            if (Amount < 600000)
                return 105;
            if (Amount < 700000)
                return 115;
            if (Amount < 800000)
                return 125;
            if (Amount < 900000)
                return 135;
            if (Amount < 1000000)
                return 150;
            if (Amount < 2000000)
                return 300;
            if (Amount < 3000000)
                return 450;
            if (Amount < 4000000)
                return 600;
            if (Amount < 5000000)
                return 750;
            if (Amount < 6000000)
                return 900;
            if (Amount < 7000000)
                return 1050;
            if (Amount < 8000000)
                return 1200;
            if (Amount < 9000000)
                return 1350;
            return 1500;
        }

        public async Task<Response> SetTransferAsync(TransactionModel transactionModel)
        {


            decimal Fee = GetTranscationFee(transactionModel.UserType, transactionModel.Amount);

            transactionModel.Sender.Balance = transactionModel.Sender.Balance - transactionModel.Amount - Fee;
            transactionModel.Receiver.Balance += transactionModel.Amount;

            await _userProfileRepo.UpdateAsync(transactionModel.Sender);
            await _userProfileRepo.UpdateAsync(transactionModel.Receiver);

            var TransactionHistory = new List<TransactionHistory>() { 
            new TransactionHistory
            {
                UserId = transactionModel.Sender.UserId,
                TransactionType = TransactionType.Debit,
                DateStamp = DateTime.Now,
                Amount = transactionModel.Amount,
                Description = $"Sent {transactionModel.Amount} to {transactionModel.Receiver.FirstName} {transactionModel.Receiver.MiddleName} {transactionModel.Receiver.LastName}  on {DateTime.Now.ToLongDateString} at {DateTime.Now.ToShortTimeString}"
            },
            new TransactionHistory
            {
                UserId = transactionModel.Sender.UserId,
                TransactionType = TransactionType.Debit,
                DateStamp = DateTime.Now,
                Amount = Fee,
                Description = $"TransactionFee for ₦{transactionModel.Amount} to {transactionModel.Receiver.LastName} {transactionModel.Receiver.FirstName} {transactionModel.Receiver.MiddleName} is ₦ {Fee}"
            },
	         new TransactionHistory
             { 
                 UserId = transactionModel.Receiver.UserId,
                 TransactionType = TransactionType.Credit,
                 DateStamp = DateTime.Now,
                 Amount = transactionModel.Amount,
                 Description = $"Received {transactionModel.Amount} from {transactionModel.Sender.FirstName} {transactionModel.Sender.MiddleName} {transactionModel.Sender.LastName}"

	          } };

            await _transactionHistoryRepo.AddRangeAsync(TransactionHistory);
            var Check = await _unitOfWork.SaveChangesAsync();
            return Check > 0 ? new Response {Success = true, Data = $"Task: Transfer was successfully!" } : throw new InvalidOperationException("Transfer failed!");
        }

        public async Task<Response> TransferMoneyAsync(TransferRequest transferRequest)
        {
            var Receiver = await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == transferRequest.AccountNumber);

            string? _userId = _contextAccessor.HttpContext?.User.GetUserId();

            //var User = await _userManager.FindByIdAsync(_userId);

            var Sender = await _userProfileRepo.GetSingleByAsync(a => a.Id == _userId);

            

            if (Receiver == null)
            {
                throw new InvalidOperationException("User Not Found"); 
	        }
            if(Sender.Password != transferRequest.SenderPassword)
            {
                throw new InvalidOperationException("Password Incorrect"); 
	        }

		    if( Sender.Balance <= transferRequest.Amount)
            {
                throw new InvalidOperationException("Insufficent Fund"); 
	        }
            if (transferRequest.Amount < 0)
            {
                throw new InvalidOperationException("Invalid Amount"); 
	        }
            if (Sender.UserTypeId == UserType.Indiviual && transferRequest.Amount > ((decimal)TransactionLimit.Indiviual) )
            {
                throw new InvalidOperationException("Amount over Limit");
	        }
            if (Sender.UserTypeId == UserType.Corporate && transferRequest.Amount > ((decimal)TransactionLimit.Corporate))
            {
                throw new InvalidOperationException("Amount over Limit");
            }

            var transactionModel = new TransactionModel
            {
                Receiver = Receiver,
                Sender = Sender,
                Amount = transferRequest.Amount
            };

            Response TransactionCheck = await SetTransferAsync(transactionModel);

            return new Response {Success = TransactionCheck.Success,Data = TransactionCheck.data };            
        }
    }
}


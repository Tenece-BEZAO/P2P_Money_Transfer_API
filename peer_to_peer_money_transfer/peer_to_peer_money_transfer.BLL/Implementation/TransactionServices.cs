using System;
using Microsoft.AspNetCore.Identity;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.DAL.Interfaces;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IServiceFactory _serviceFactory;
        private readonly IRepository<TransactionHistory> _transactionHistoryRepo;
        private readonly IRepository<Complains> _complainRepo;
        private readonly IRepository<UserProfile> _userProfileRepo;
        //private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionServices( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_serviceFactory = serviceFactory;
            //_mapper = _serviceFactory.GetService<IMapper>();
            //_userManager = _serviceFactory.GetService<UserManager<ApplicationUser>>();
            _transactionHistoryRepo = _unitOfWork.GetRepository<TransactionHistory>();
            _complainRepo = _unitOfWork.GetRepository<Complains>();
            _userProfileRepo = _unitOfWork.GetRepository<UserProfile>();
        }

        public Task<bool> FileComplainAsync(ComplainRequest complainRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool successful, decimal amount)> GetBalanceAsync(AccountNumberRequest AccountNumber)
        {
            var User = await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == AccountNumber.AccountNumber);

            if (User != null)
                return (true, User.Balance);

            return (false, 0);
        }

        public async Task<ReceiverNameResponse> GetReceiverNameAsync(AccountNumberRequest AccountNumber)
        {
            var User =  await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == AccountNumber.AccountNumber);

            if (User != null)
                return new ReceiverNameResponse{            		    
		        ReceiverFullName = $"{ User.FirstName} {User.MiddleName} {User.LastName}"
		        };

            return null;
            
        }

        public Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesAsync(LoginVerifyRequest loginVerify)
        {
            throw new NotImplementedException();
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

        public async Task<(bool successful, string msg)> SetTransferFeeAsync(TransactionModel transactionModel)
        {


            decimal Fee = GetTranscationFee(transactionModel.UserType, transactionModel.Amount);
            var SenderTransactionHistory = new List<TransactionHistory>() { 
            new TransactionHistory
            {
                UserId = transactionModel.Sender.Id,
                TransactionType = TransactionType.Transfer,
                DateStamp = DateTime.Now,
                Amount = transactionModel.Amount,
                Description = $"Sent {transactionModel.Amount} to {transactionModel.Receiver.LastName} {transactionModel.Receiver.FirstName} {transactionModel.Receiver.MiddleName} on {DateTime.Now.ToLongDateString} at {DateTime.Now.ToShortTimeString}"
            },
            new TransactionHistory
            {
                UserId = transactionModel.Sender.Id,
                TransactionType = TransactionType.Transfer,
                DateStamp = DateTime.Now,
                Amount = Fee,
                Description = $"TransactionFee for ₦{transactionModel.Amount} to {transactionModel.Receiver.LastName} {transactionModel.Receiver.FirstName} {transactionModel.Receiver.MiddleName} is ₦{Fee}"
            }};

            await _transactionHistoryRepo.AddRangeAsync(SenderTransactionHistory);
            var Check = await _unitOfWork.SaveChangesAsync();
            return Check > 0 ? (true, $"Task: Transfer was successfully!") : (false, "Transfer failed!");
        }

        public async Task<(bool successful,string msg)> TransferMoneyAsync(TransferRequest transferRequest)
        {
            var Receiver = await _userProfileRepo.GetSingleByAsync(a => a.AccountNumber == transferRequest.AccountNumber); 

            var User = await _userManager.FindByLoginAsync(transferRequest.Provider, transferRequest.Key);

            var Sender = await _userProfileRepo.GetSingleByAsync(a => a.Email == User.Email);

            

            if (Receiver == null)
            {
                return (false, "User Not Found"); 
	        }
            if(Sender.Password != transferRequest.SenderPassword)
            {
                return (false, "Password Incorrect"); 
	        }

		    if( Sender.Balance <= transferRequest.Amount)
            {
                return (false, "Insufficent Fund"); 
	        }
            if (transferRequest.Amount < 0)
            {
                return (false, "Invalid Amount"); 
	        }
            if (User.UserTypeId == UserType.Indiviual && transferRequest.Amount > ((decimal)TransactionLimit.Indiviual) )
            {
                return (false, "Amount over Limit");
	        }
            if (User.UserTypeId == UserType.Corporate && transferRequest.Amount > ((decimal)TransactionLimit.Corporate))
            {
                return (false, "Amount over Limit");
            }

            var transactionModel = new TransactionModel
            {
                Receiver = Receiver,
                Sender = Sender,
                Amount = transferRequest.Amount
            };

            var (TransactionCheck, msg) = await SetTransferFeeAsync(transactionModel);

            return (TransactionCheck, msg);            
        }
    }
}


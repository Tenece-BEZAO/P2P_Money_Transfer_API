using System;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface ITransactionServices
    {
        Task<(bool successful, string msg)> GetReceiverNameAsync(AccountNumberRequest AccountNumber);

        Task<(bool successful, string msg)> TransferMoneyAsync(TransferRequest transferRequest, string loginProvider, string providerKey);

        Task<bool> FileComplainAsync(ComplainRequest complainRequest, string loginProvider, string providerKey);

        Task<(bool successful, string msg)> SetTransferFeeAsync(TransactionModel transactionModel);

        Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesAsync(string loginProvider, string providerKey);

        Task<(bool successful, decimal amount)> GetBalanceAsync(AccountNumberRequest AccountNumber);

        Task<decimal> GetTranscationFee(UserType userType, decimal Amount);

        
    }
}


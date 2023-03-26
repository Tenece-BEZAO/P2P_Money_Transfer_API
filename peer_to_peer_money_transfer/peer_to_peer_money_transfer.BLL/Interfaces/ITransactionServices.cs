using System;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface ITransactionServices
    {
        Task<ReceiverNameResponse> GetReceiverNameAsync(AccountNumberRequest AccountNumber);

        Task<(bool successful, string msg)> TransferMoneyAsync(TransferRequest transferRequest);

        Task<bool> FileComplainAsync(ComplainRequest complainRequest);

        Task<(bool successful, string msg)> SetTransferFeeAsync(TransactionModel transactionModel);

        Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesAsync(LoginVerifyRequest loginVerify);

        Task<(bool successful, decimal amount)> GetBalanceAsync(AccountNumberRequest AccountNumber);

        decimal GetTranscationFee(UserType userType, decimal Amount);

        
    }
}


﻿using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.BLL.Models;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using Response = peer_to_peer_money_transfer.DAL.Dtos.Responses.ResponseStatus;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface ITransactionServices
    {
        Task<ReceiverNameResponse> GetReceiverNameAsync(AccountNumberRequest AccountNumber);

        Task<Response> TransferMoneyAsync(TransferRequest transferRequest);

        Task<Response> FileComplainAsync(ComplainRequest complainRequest);

        Task<Response> SetTransferAsync(TransactionModel transactionModel);

        Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesAsync();

        Task<Response> GetBalanceAsync(AccountNumberRequest AccountNumber);

        decimal GetTranscationFee(UserType userType, decimal Amount);
    }
}

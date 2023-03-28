using PayStack.Net;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IFundingService
    {
        Task<bool> ValidateWallet(string accountNumber);
        TransactionInitializeResponse MakePayment(DepositRequest depositRequest);
        Task<bool> VerifyPayment();
        Task FundAccount(DepositRequest depositRequest);

    }
}

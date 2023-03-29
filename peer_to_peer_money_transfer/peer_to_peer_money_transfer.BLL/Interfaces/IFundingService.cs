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
  
        TransactionInitializeResponse MakePayment(DepositRequest depositRequest);
        bool VerifyPayment(string referenceCode);
        Task<string> FundAccount(DepositRequest depositRequest, string reference);

    }
}

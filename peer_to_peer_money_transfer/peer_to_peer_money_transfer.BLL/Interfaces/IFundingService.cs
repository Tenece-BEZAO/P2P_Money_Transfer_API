using PayStack.Net;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Response = peer_to_peer_money_transfer.DAL.Dtos.Responses.ResponseStatus;
namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IFundingService
    {
  
        TransactionInitializeResponse MakePayment(DepositRequest depositRequest);
        TransactionVerifyResponse VerifyPayment(string referenceCode);
        Task<Response> FundAccount(string currentUserId, string reference);

    }
}

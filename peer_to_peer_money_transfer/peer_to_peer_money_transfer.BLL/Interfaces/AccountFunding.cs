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
        Task<bool> MakePayment();
        Task<bool> VerifyPayment();
        Task FundAccount();

    }
}

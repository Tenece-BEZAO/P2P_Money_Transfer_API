using System;
using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.BLL.Models
{
    public class TransactionModel
    {
        public UserProfile Receiver { get; set; }

        public UserProfile Sender { get; set; }

        public decimal Amount { get; set; }
    }
}


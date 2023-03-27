using System;
using System.Transactions;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class TransactionHistory
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateStamp { get; set; }

        public TransactionType TransactionType { get; set; } 

        public decimal Amount { get; set; }

        public string Description { get; set; } = null!;
    }
}


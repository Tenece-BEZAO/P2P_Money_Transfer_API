using System;
using System.Transactions;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class TransactionHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateStamp { get; set; }

        public TransactionType TransactionType { get; set; } 

        public string Description { get; set; } = null!;
    }
}


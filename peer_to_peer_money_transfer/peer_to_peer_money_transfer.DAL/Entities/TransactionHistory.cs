using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class TransactionHistory
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }
        public DateTime DateStamp { get; set; }

        public TransactionType TransactionType { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public string Description { get; set; } = null!;
    }
}


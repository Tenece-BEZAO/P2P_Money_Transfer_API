using System;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.BLL.Models
{
    public class TransactionModel
    {
        public ApplicationUser Receiver { get; set; }

        public ApplicationUser Sender { get; set; }

        public UserType UserType { get; set; }

        public decimal Amount { get; set; }
    }
}


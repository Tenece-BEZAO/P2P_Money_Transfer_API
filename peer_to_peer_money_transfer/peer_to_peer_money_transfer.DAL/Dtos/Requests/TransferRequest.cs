using System;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.Dtos.Requests
{
    public class TransferRequest
    {
        [Required]
        public string AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public string SenderPassword { get; set; }
    }
}


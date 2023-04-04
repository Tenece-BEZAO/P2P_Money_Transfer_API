using System;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.Dtos.Requests
{
    public class TransferRequest 
    {
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string SenderPassword { get; set; }
    }
}


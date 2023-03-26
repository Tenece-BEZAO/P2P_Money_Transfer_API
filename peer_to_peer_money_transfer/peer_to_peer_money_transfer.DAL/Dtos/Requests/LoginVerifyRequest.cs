using System;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.Dtos.Requests
{
    public class LoginVerifyRequest
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        public string Key{ get; set; }
        [Required]
        public string AccountNumber { get; set; }
    }
}


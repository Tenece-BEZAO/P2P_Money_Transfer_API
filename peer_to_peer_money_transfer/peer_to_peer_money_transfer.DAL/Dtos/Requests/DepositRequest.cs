using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.Dtos.Requests
{
    public class DepositRequest
    {
        [Required]
        public string CurrentUserId { get; set; }
        [Required]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        [Required]
        public int AmountInKobo { get; set; }
        [Required]
        public string Email { get; set; }

        public string Plan { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("subaccount")]
        public string SubAccount { get; set; }
        [Required]
        [JsonProperty("transaction_charge")]
        public int TransactionCharge { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "NGN";

        public string Bearer { get; set; }
    }
}

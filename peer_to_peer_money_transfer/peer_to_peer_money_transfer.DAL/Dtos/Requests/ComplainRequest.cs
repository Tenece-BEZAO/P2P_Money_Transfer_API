using System;
using System.Security.Cryptography.X509Certificates;

namespace peer_to_peer_money_transfer.DAL.Dtos.Requests
{
    public class ComplainRequest
    {
        public long? TransactionId { get; set; } 

        public string Subject {get; set;}

        public string Description { get; set;}
    }
}


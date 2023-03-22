﻿using System;
namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class BaseEntities
    {
        public string PhoneNumber { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public decimal Balance { get; set; } = 0;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string BVN { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool Verified { get; set; } = false;

        public bool Activated { get; set; } = false;

        public bool Lien { get; set; } = false;
    
        public TransactionHistory? transactionHistory { get; set; } 

        public Complains? Complains { get; set; } 
    }
}


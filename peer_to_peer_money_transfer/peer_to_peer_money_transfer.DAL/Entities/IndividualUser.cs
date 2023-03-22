using System;
namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class IndividualUser : BaseEntities
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Middle { get; set; }

        public string NIN { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
    }
}


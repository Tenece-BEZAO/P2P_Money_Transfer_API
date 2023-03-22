using System;
namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class Complains
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TransationId { get; set; }

        public string ComplainDescription { get; set; } = null!;
    }
}


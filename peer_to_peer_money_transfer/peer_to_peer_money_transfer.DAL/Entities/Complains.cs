using System;
namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class Complains
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long TransationId { get; set; }

        public string ComplainSubject { get; set; } = null!;

        public string ComplainDescription { get; set; } = null!;
    }
}


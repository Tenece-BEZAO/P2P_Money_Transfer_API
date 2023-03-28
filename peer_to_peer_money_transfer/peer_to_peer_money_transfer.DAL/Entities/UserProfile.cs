using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class UserProfile : BaseEntities
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public ProfileName ProfileName { get; set; }

        public DocumentType DocumentType{ get; set; } 

        public JobType JobType { get; set; }

        public DateTime DateOfBirth { get; set; }  

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}


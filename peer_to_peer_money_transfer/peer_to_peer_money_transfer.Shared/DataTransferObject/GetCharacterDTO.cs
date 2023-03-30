using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.Shared.DataTransferObject
{
    public class GetCharacterDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public UserType AccountType { get; set; }

        public DateTime? Birthday { get; set; }
    }
}

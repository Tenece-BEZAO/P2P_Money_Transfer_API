using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.Shared.DataTransferObject
{
    public class RegisterIndividualDTO : LoginDTO
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone No is Required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Your Address is Required")]
        public string Address { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string? RecoveryMail { get; set; }

        [Required(ErrorMessage = "Your Profession is Required")]
        [Display(Name = "Profession")]
        public string? Profession { get; set; }

        public string? NIN { get; set; }

        public string BVN { get; set; }

        public UserType UserTypeId { get; set; } = UserType.Indiviual;

        public string AccountNumber { get; set; } = "123";

    }
}

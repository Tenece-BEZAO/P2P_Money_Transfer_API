using peer_to_peer_money_transfer.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.DataTransferObject
{
    public class RegisterDTO : LoginDTO
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

        [Required(ErrorMessage = "Account Type is Required")]
        [Display(Name = "Account Type")]
        public UserType AccountType { get; set; }
    }
}

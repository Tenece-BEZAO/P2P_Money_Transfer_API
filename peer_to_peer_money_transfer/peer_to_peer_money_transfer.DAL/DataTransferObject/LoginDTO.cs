using System.ComponentModel.DataAnnotations;

namespace peer_to_peer_money_transfer.DAL.DataTransferObject
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "UserName name Must be Unique")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 5)]
        public string PasswordHash { get; set; }
    }
}

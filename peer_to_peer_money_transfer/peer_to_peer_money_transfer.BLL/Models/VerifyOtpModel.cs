using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.BLL.Models
{
    public class VerifyOtpModel
    {
        public ApplicationUser User { get; set; }

        public string Token { get; set; }
    }
}

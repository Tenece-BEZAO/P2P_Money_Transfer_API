namespace peer_to_peer_money_transfer.Shared.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailAdress, string message);
    }
}

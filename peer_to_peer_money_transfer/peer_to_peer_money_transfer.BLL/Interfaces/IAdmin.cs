using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<ApplicationUser>> GetAll();

        Task<GetCharacterDTO> GetAllCustomers();

        Task<bool> GetCustomerByName(string name);

        Task<ApplicationUser> GetCustomerByAccountNumber(long number);

        Task<TransactionHistory> GetTransactionById(long Id);

        Task<bool> RegisterAdmin();

        Task<ApplicationUser> EditCustomerDetails(ApplicationUser user);

        Task<bool> DeactivateCustomer();

        Task<ApplicationUser> Delete(ApplicationUser user); //implement soft delete

        Task DeleteCustomer(long Id); //implement soft delete
    }
}

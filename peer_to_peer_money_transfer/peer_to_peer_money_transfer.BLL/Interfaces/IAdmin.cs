using Microsoft.AspNetCore.JsonPatch;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;
using peer_to_peer_money_transfer.Shared.DataTransferObject;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<ApplicationUser>> GetAll();

        Task<IEnumerable<GetCharacterDTO>> GetAllCustomers();

        Task<IEnumerable<ApplicationUser>> GetAllCustomersByCategory(UserType userType);

        Task<GetCharacterDTO> GetCustomerByUserName(string userName);

        Task<ApplicationUser> GetCustomerByUserNameAll(string userName);

        Task<ApplicationUser> GetCustomerByAccountNumber(string accountNumber);

        Task<TransactionHistory> GetTransactionById(long Id);

        Task<ApplicationUser> EditCustomerDetails(string userName, JsonPatchDocument<ApplicationUser> user);

        Task<string> DeactivateCustomer(string userName);

        Task AccessFailedCount(string userName);

        Task ResetCount(string userName);

        Task LockCustomer(string userName);

        Task<string> SoftDelete(string userName); //soft delete

        Task<string> DeleteCustomer(string userName); //hard delete
    }
}

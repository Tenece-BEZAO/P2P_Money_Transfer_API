using Microsoft.AspNetCore.JsonPatch;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;

namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<ApplicationUser>> GetAll();

        Task<IEnumerable<GetCharacterDTO>> GetAllCustomers();

        Task<GetCharacterDTO> GetCustomerByUserName(string userName);

        Task<ApplicationUser> GetCustomerByUserNameAll(string userName);

        Task<ApplicationUser> GetCustomerByAccountNumber(string accountNumber);

        Task<TransactionHistory> GetTransactionById(long Id);

        Task<ApplicationUser> EditCustomerDetails(string userName, JsonPatchDocument<ApplicationUser> user);

        /*Task<ApplicationUser> EditSingleCustomerDetail(string userName);*/

        Task<ApplicationUser> DeactivateCustomer(string userName);

        Task<ApplicationUser> Delete(string userName); //soft delete

        Task<ApplicationUser> DeleteCustomer(string userName); //hard delete
    }
}

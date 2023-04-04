using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;


namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<ApplicationUser>> GetAll();

        Task<GetCharacterDTO> GetAllCustomers();

        Task<ResponseStatus> GetCustomerByName(string name);

        Task<ApplicationUser> GetCustomerByAccountNumber(long number);

        Task<TransactionHistory> GetTransactionById(long Id);

        Task<ResponseStatus> RegisterAdmin();

        Task<ApplicationUser> EditCustomerDetails(ApplicationUser user);

        Task<ResponseStatus> DeactivateCustomer(AccountNumberRequest accountNumber);

        Task<ResponseStatus> Delete(AccountNumberRequest accountNumber); //implement soft delete

        Task DeleteCustomer(AccountNumberRequest accountNumber); //implement hard delete
    }
}

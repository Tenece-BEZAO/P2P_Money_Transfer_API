namespace peer_to_peer_money_transfer.BLL.Interfaces
{
    internal interface IAdmin
    {
        Task<bool> GetAllCustomers();

        Task<bool> GetCustomerByName(string name);

        Task<bool> GetCustomerByAccountNumber(long number);

        Task<bool> GetTransactionById(long number);

        Task<bool> EditCustomerDetails();

        Task<bool> DeleteCustomer(string name); //implement soft delete

        Task <bool> RegisterAdmin();
    }
}

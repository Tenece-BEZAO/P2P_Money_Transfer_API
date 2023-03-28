using peer_to_peer_money_transfer.BLL.Interfaces;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    internal class Admin : IAdmin
    {
        public Task<bool> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetCustomerByAccountNumber(long number)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTransactionById(long number)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditCustomerDetails()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomer(string name)
        {
            throw new NotImplementedException();
        }
    }
}

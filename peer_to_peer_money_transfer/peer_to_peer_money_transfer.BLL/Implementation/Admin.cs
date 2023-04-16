using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.Shared.DataTransferObject;

namespace peer_to_peer_money_transfer.BLL.Implementation
{
    public class Admin : IAdmin
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationUser> _userRepoService;
        private readonly IRepository<TransactionHistory> _transactionHistoryRepo;

        public Admin(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepoService = _unitOfWork.GetRepository<ApplicationUser>();
            _transactionHistoryRepo = _unitOfWork.GetRepository<TransactionHistory>();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _userRepoService.GetAllAsync();
        }

        public async Task<IEnumerable<GetCharacterDTO>> GetAllCustomers()
        {
            var allUser = await _userRepoService.GetAllAsync();

            var select = _mapper.Map<IEnumerable<GetCharacterDTO>>(allUser);

            return select;
        }

        public async Task<ApplicationUser> GetCustomerByUserNameAll(string userName)
        {
            return await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
        }

        public async Task<GetCharacterDTO> GetCustomerByUserName(string userName)
        {
            var user = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            var select = _mapper.Map<GetCharacterDTO>(user);
            return select;
        }

        public async Task<ApplicationUser> GetCustomerByAccountNumber(string accountNumber)
        {
            var number = await _userRepoService.GetSingleByAsync(x => x.AccountNumber == accountNumber);

            return number;
        }

        public async Task<TransactionHistory> GetTransactionById(long Id)
        {
            return await _transactionHistoryRepo.GetByIdAsync(Id);
        }

        public async Task<ApplicationUser> EditCustomerDetails(string userName, JsonPatchDocument<ApplicationUser> user)
        {
            var update = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);

            user.ApplyTo(update);

            return await _userRepoService.UpdateAsync(update);
        }

        public async Task<ApplicationUser> DeactivateCustomer(string userName)
        {
            var deactivateUser = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            deactivateUser.Activated = false;

            return await _userRepoService.UpdateAsync(deactivateUser);
        }

        public async Task<ApplicationUser> Delete(string userName)
        {
            var deleteUser = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);

            deleteUser.Deleted = true;

            return await _userRepoService.UpdateAsync(deleteUser);
        }

        public async Task<ApplicationUser> DeleteCustomer(string userName)
        {
            var delete = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);

            await _userRepoService.DeleteByIdAsync(delete.Id);
            return null;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;
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

        public async Task<IEnumerable<ApplicationUser>> GetAllCustomersByCategory(UserType userType)
        {
            return await _userRepoService.GetByAsync(x => x.UserTypeId == userType);
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
            if (update == null) return null;
            user.ApplyTo(update);
            return await _userRepoService.UpdateAsync(update);
        }

        public async Task<string> DeactivateCustomer(string userName)
        {
            var deactivateUser = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            if (deactivateUser == null) return null;
            deactivateUser.Activated = false;
            await _userRepoService.UpdateAsync(deactivateUser);
            return "success";
        }

        public async Task AccessFailedCount(string userName)
        {
            var count = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            count.AccessFailedCount += 1;
            await _userRepoService.UpdateAsync(count);
        }

        public async Task ResetCount(string userName)
        {
            var resetCount = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            resetCount.AccessFailedCount = 0;
            await _userRepoService.UpdateAsync(resetCount);
        }

        public async Task LockCustomer(string userName)
        {
            var lockedUser = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            lockedUser.Activated = false;
            lockedUser.EmailConfirmed = false;
            await _userRepoService.UpdateAsync(lockedUser);
        }

        public async Task<string> SoftDelete(string userName)
        {
            var deleteUser = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            if (deleteUser == null) return null;
            deleteUser.Deleted = true;
            await _userRepoService.UpdateAsync(deleteUser);
            return "success";
        }

        public async Task<string> DeleteCustomer(string userName)
        {
            var delete = await _userRepoService.GetSingleByAsync(x => x.UserName == userName);
            if (delete == null) return null;
            await _userRepoService.DeleteByIdAsync(delete.Id);
            return "success";
        }
    }
}

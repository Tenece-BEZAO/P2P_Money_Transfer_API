using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
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
        public async Task<GetCharacterDTO> GetAllCustomers()
        {
            var allUser = await _userRepoService.GetAllAsync();
            var select = _mapper.Map<GetCharacterDTO>(allUser);

            return select;
        }

        public async Task<bool> GetCustomerByName(string name)
        {
            throw new NotImplementedException();
            //return await _userRepoService.GetByAsync(name);
        }

        public async Task<ApplicationUser> GetCustomerByAccountNumber(long number)
        {
            return await _userRepoService.GetByIdAsync(number);
        }

        public async Task<TransactionHistory> GetTransactionById(long Id)
        {
            return await _transactionHistoryRepo.GetByIdAsync(Id);

        }

        public async Task<ApplicationUser> EditCustomerDetails(ApplicationUser user)
        {
            return await _userRepoService.UpdateAsync(user);
        }

        public async Task<bool> RegisterAdmin()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeactivateCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> Delete(ApplicationUser user)
        {
            return await _userRepoService.UpdateAsync(user);

        }
        public async Task DeleteCustomer(long Id)
        {
           await _userRepoService.DeleteByIdAsync(Id);
        }
    }
}

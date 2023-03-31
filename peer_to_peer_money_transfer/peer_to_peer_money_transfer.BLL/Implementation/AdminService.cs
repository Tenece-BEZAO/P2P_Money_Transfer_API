using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.Shared.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using Microsoft.VisualBasic;

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

        public async Task<ResponseStatus> GetCustomerByName(string name)
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

        public async Task<ResponseStatus> RegisterAdmin()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStatus> DeactivateCustomer(AccountNumberRequest accountNumber)
        {
            var User = await _userRepoService.GetSingleByAsync(a => a.AccountNumber == accountNumber.AccountNumber);
            if(User == null)  return new ResponseStatus { Success = false, Data = "Account not Deactivated" };
            User.Activated = false;
            await _userRepoService.UpdateAsync(User);
            return new ResponseStatus { Success =true , Data = "Account Deactivated" };
        }

        public async Task<ResponseStatus> Delete(AccountNumberRequest accountNumber)
        {
            var User = await _userRepoService.GetSingleByAsync(a => a.AccountNumber == accountNumber.AccountNumber);
            if (User == null) return new ResponseStatus { Success = false, Data = "Account not Deactivated" };

            User.Deleted = true;

            return new ResponseStatus { Success = true, Data =  "Soft Delete successful"};

        }
        public async Task DeleteCustomer(AccountNumberRequest accountNumber)
        {
           await _userRepoService.DeleteAsync(a => a.AccountNumber == accountNumber.AccountNumber);
        }
    }
}

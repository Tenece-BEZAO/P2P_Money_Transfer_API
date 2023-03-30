using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.Interfaces;
using peer_to_peer_money_transfer.Shared.JwtConfigurations;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJwtConfig _jwtConfig;

        public AdminController(IAdmin admin, UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<AccountController> logger, IJwtConfig jwtConfig)
        {
            _admin = admin;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _jwtConfig = jwtConfig;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _admin.GetAll());
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _admin.GetAllCustomers());
        }

        [HttpGet("GetCustomerByUserName")]
        public async Task<IActionResult> GetCustomerByUserName(string userName)
        {
            return Ok(await _admin.GetCustomerByUserName(userName));
        }

        [HttpGet("GetCustomerByUserNameAll")]
        public async Task<IActionResult> GetCustomerByUserNameAll(string userName)
        {
            return Ok(await _admin.GetCustomerByUserNameAll(userName));
        }

        [HttpGet("GetCustomerByAccountNumber")]
        public async Task<IActionResult> GetCustomerByAccountNumber(long id)
        {
            return Ok(await _admin.GetCustomerByAccountNumber(id));
        }

        [HttpGet("getTransactionById")]
        public async Task<IActionResult> GetTransactionById(long id)
        {
            return Ok(await _admin.GetTransactionById(id));
        }


        [HttpPatch("editCustomerDetails/{userName}")]
        public async Task<IActionResult> EditCustomerDetails(string userName, [FromBody] JsonPatchDocument<ApplicationUser> user)
        {
            return Ok(await _admin.EditCustomerDetails(userName, user));
        }

        [HttpPatch("deactivateCustomer/{userName}")]
        public async Task<IActionResult> DeactivateCustomer(string userName, [FromBody] JsonPatchDocument<ApplicationUser> user)
        {
            return Ok(await _admin.DeactivateCustomer(userName, user));
        }

        [HttpPatch("deleteCustomer/{userName}")]
        public async Task<IActionResult> Delete(string userName, JsonPatchDocument<ApplicationUser> user)
        {
            return Ok(await _admin.Delete(userName, user));
        }

        [HttpDelete("delete/deleteCustomer/{userName}")]
        public async Task<IActionResult> DeleteCustomer(string userName)
        {
            return Ok(await _admin.DeleteCustomer(userName));
        }
    }
}

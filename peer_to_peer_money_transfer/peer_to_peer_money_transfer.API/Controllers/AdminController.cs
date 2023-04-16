using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, User") ]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AdminController(IAdmin admin, UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<AccountController> logger)
        {
            _admin = admin;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _admin.GetAll());
        }

        [HttpGet("get-all-customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _admin.GetAllCustomers());
        }

        [HttpGet("get-customer-by-userName")]
        public async Task<IActionResult> GetCustomerByUserName(string userName)
        {
            return Ok(await _admin.GetCustomerByUserName(userName));
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("get-customer-by-userNameAll")]
        public async Task<IActionResult> GetCustomerByUserNameAll(string userName)
        {
            return Ok(await _admin.GetCustomerByUserNameAll(userName));
        }

        [HttpGet("get-customer-by-accountNumber")]
        public async Task<IActionResult> GetCustomerByAccountNumber(string accountNumber)
        {
            return Ok(await _admin.GetCustomerByAccountNumber(accountNumber));
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet("get-transaction-by-id")]
        public async Task<IActionResult> GetTransactionById(long id)
        {
            return Ok(await _admin.GetTransactionById(id));
        }


        [HttpPatch("edit-customer-details")]
        public async Task<IActionResult> EditCustomerDetails(string userName, [FromBody] JsonPatchDocument<ApplicationUser> user)
        {
            return Ok(await _admin.EditCustomerDetails(userName, user));
        }


        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("deactivate-customer")]
        public async Task<IActionResult> DeactivateCustomer(string userName)
        {
            return Ok(await _admin.DeactivateCustomer(userName));
        }

        [HttpPost("delete-customer")]
        public async Task<IActionResult> Delete(string userName)
        {
            return Ok(await _admin.Delete(userName));
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("delete/delete-customer")]
        public async Task<IActionResult> DeleteCustomer(string userName)
        {
            return Ok(await _admin.DeleteCustomer(userName));
        }
    }
}

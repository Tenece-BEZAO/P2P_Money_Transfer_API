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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User") ]
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

        [HttpGet("GetAll")]
        [Authorize(Policy = "SuperAdmin")]
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

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("GetCustomerByUserNameAll")]
        public async Task<IActionResult> GetCustomerByUserNameAll(string userName)
        {
            return Ok(await _admin.GetCustomerByUserNameAll(userName));
        }

        [HttpGet("GetCustomerByAccountNumber")]
        public async Task<IActionResult> GetCustomerByAccountNumber(string accountNumber)
        {
            return Ok(await _admin.GetCustomerByAccountNumber(accountNumber));
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

        [HttpPost("editSingleField/{userName}")]
        public async Task<IActionResult> EditCustomerDetailsSingle(string userName)
        {
            /*return Ok(await _admin.EditSingleCustomerDetail(userName));*/
            return Ok();
        }

        [HttpPost("deactivateCustomer/{userName}")]
        public async Task<IActionResult> DeactivateCustomer(string userName)
        {
            return Ok(await _admin.DeactivateCustomer(userName));
        }

        [HttpPost("deleteCustomer/{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            return Ok(await _admin.Delete(userName));
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("delete/deleteCustomer/{userName}")]
        public async Task<IActionResult> DeleteCustomer(string userName)
        {
            return Ok(await _admin.DeleteCustomer(userName));
        }
    }
}

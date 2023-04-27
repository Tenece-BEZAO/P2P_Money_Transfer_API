using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Enums;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    /*[Authorize(Roles = "SuperAdmin, Admin, User")]*/
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        /*[Authorize(Policy = "SuperAdmin")]*/
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _admin.GetAll());
        }

        [HttpGet("get-all-customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _admin.GetAllCustomers());
        }

        [HttpGet("get-all-customers-by-category")]
        public async Task<IActionResult> GetAllCustomersByCategory(UserType category)
        {
            return Ok(await _admin.GetAllCustomersByCategory(category));
        }

        [HttpGet("get-customer-by-userName")]
        public async Task<IActionResult> GetCustomerByUserName(string userName)
        {
            var user = await _admin.GetCustomerByUserName(userName);
            if (user == null) return NotFound(new { error = "user does not exists" });
            return Ok(user);
        }

        /*[Authorize(Policy = "SuperAdmin")]*/
        [HttpGet("get-customer-by-userName-all")]
        public async Task<IActionResult> GetCustomerByUserNameAll(string userName)
        {
            var user = await _admin.GetCustomerByUserNameAll(userName);
            if (user == null) return NotFound(new { error = "user does not exists" });
            return Ok(user);
        }

        [HttpGet("get-customer-by-accountNumber")]
        public async Task<IActionResult> GetCustomerByAccountNumber(string accountNumber)
        {
            var user = await _admin.GetCustomerByAccountNumber(accountNumber);
            if (user == null) return NotFound(new { error = "user does not exists" });
            return Ok(user);
        }

        /*[Authorize(Roles = "SuperAdmin, Admin")]*/
        [HttpGet("get-transaction-by-id")]
        public async Task<IActionResult> GetTransactionById(long id)
        {
            var transaction = await _admin.GetTransactionById(id);
            if (transaction == null) return NotFound(new { error = "transaction does not exists" });
            return Ok(transaction);
        }

        [HttpPatch("edit-customer-details")]
        public async Task<IActionResult> EditCustomerDetails(string userName, [FromBody] JsonPatchDocument<ApplicationUser> user)
        {
            var edited = await _admin.EditCustomerDetails(userName, user);
            if (edited == null) return NotFound(new { error = "user does not exists" });
            return Ok(new { Message = "Edited Successfully" });
        }

        /*[Authorize(Roles = "SuperAdmin, Admin")]*/
        [HttpPost("deactivate-customer")]
        public async Task<IActionResult> DeactivateCustomer(string userName)
        {
            var deactivated = await _admin.DeactivateCustomer(userName);
            if (deactivated == null) return NotFound(new { error = "user does not exists" });
            return Ok(new { Message = "User Deactivated Successfully" });
        }

        [HttpPost("delete-customer")]
        public async Task<IActionResult> SoftDelete(string userName)
        {
            var deleted = await _admin.SoftDelete(userName);
            if (deleted == null) return NotFound(new { error = "user does not exists" });
            return Ok(new { Message = "User Deleted Successfully" });
        }


        /*[Authorize(Policy = "SuperAdmin")]*/
        [HttpDelete("delete/delete-customer")]
        public async Task<IActionResult> DeleteCustomer(string userName)
        {
            var deleted = await _admin.DeleteCustomer(userName);
            if (deleted == null) return NotFound(new { error = "user does not exists" });
            return Ok(new { Message = "User Deleted Successfully"});
        }
    }
}

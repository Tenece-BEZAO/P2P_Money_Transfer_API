using Microsoft.AspNetCore.Mvc;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
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

        [HttpGet("GetCustomerByName")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            return Ok(await _admin.GetCustomerByName(name));
        }

        [HttpGet("GetCustomerByAccountNumber")]
        public async Task<IActionResult> GetCustomerByAccountNumber(long id)
        {
            return Ok(await _admin.GetCustomerByAccountNumber(id));
        }

        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(long id)
        {
            return Ok(await _admin.GetTransactionById(id));
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin()
        {
            return Ok();
        }

        [HttpPut("EditCustomerDetails")]
        public async Task<IActionResult> EditCustomerDetails(ApplicationUser user)
        {
            return Ok(await _admin.EditCustomerDetails(user));
        }

        [HttpPut("DeactivateCustomer")]
        public async Task<IActionResult> DeactivateCustomer(long number)
        {
            return Ok();
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            return Ok(await _admin.Delete(user));
        }

        [HttpDelete("Delete/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            return Ok(_admin.DeleteCustomer(id));
        }
    }
}

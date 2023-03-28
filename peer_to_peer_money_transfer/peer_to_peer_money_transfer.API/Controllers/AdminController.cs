using Microsoft.AspNetCore.Mvc;

namespace peer_to_peer_money_transfer.API.Controllers
{
    public class AdminController : ControllerBase
    {
        public async Task<IActionResult> GetAllCustomer()
        {
            return Ok();
        }
    }
}

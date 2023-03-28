using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Implementation;
using peer_to_peer_money_transfer.BLL.Infrastructure;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [Route("CashMingle/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        IFundingService _fundingService;
        public FundController(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }
        [AllowAnonymous]
        [HttpPost("fund-account")]
        [SwaggerOperation(Summary = "Funds account using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Funding successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Transaction failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<TransactionInitializeResponse>> Deposit(DepositRequest depositRequest)
        {
            var response = _fundingService.MakePayment(depositRequest);
            return response;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Response = peer_to_peer_money_transfer.DAL.Dtos.Responses.ResponseStatus;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using peer_to_peer_money_transfer.BLL.Infrastructure;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [Route("CashMingle/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        IFundingService _fundingService;
        IHttpContextAccessor _contextAccessor;
        public FundController(IFundingService fundingService, IHttpContextAccessor contextAccessor)
        {
            _fundingService = fundingService;
            _contextAccessor = contextAccessor;
        }
        [Authorize(Roles = "User")]
        [HttpPost("make-deposit")]
        [SwaggerOperation(Summary = "Funds account using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Funding successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Transaction failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public ActionResult<TransactionInitializeResponse> Deposit(DepositRequest depositRequest)
        {
            var response = _fundingService.MakePayment(depositRequest);
            return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpPost("verify-payment")]
        [SwaggerOperation(Summary = "Verifying deposits using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Verification successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Verification failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> Verify(string reference)
        {
            string? userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new Response { Success = false, Data = "User not found" };
            }
            var response = await _fundingService.FundAccount(userId,reference);
            return Ok(response);
        }
    }
}

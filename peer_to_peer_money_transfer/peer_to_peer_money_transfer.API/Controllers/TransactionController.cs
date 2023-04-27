using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using peer_to_peer_money_transfer.BLL.Infrastructure;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;
using Response = peer_to_peer_money_transfer.DAL.Dtos.Responses.ResponseStatus;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace peer_to_peer_money_transfer.API.Controllers
{
    [Authorize(Roles = "User")]
    [Route("CashMingle/[controller]")]
    public class TransactionController : Controller
    {
        private readonly  ITransactionServices _transactionsServices;
        public TransactionController(ITransactionServices transactionsServices)
        {
            _transactionsServices = transactionsServices;
        }

        // GET: api/values
        [HttpGet("get-transaction-histories-name")]
        [SwaggerOperation(Summary = "Gets name with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets transaction history with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<TransactionHistory>>> User_Transaction_History()
        {
            var model = await _transactionsServices.GetTransactionHistoriesAsync();
            return Ok(model);
        }


        // GET api/values/5

        [AllowAnonymous]
        [HttpGet("get-receiver-name")]
        [SwaggerOperation(Summary = "Gets name with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets name with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ReceiverNameResponse>> Receiver_Name(AccountNumberRequest User)
        {
            ReceiverNameResponse model = await _transactionsServices.GetReceiverNameAsync(User) ;          
            return Ok(model);
        }

        [HttpGet("get-balance")]
        [SwaggerOperation(Summary = "Gets balance with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets name with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> Balance(AccountNumberRequest accountNo)
        {
            Response model = await _transactionsServices.GetBalanceAsync(accountNo);
            return Ok(model);
        }

        [HttpPost("file-complains")]
        [SwaggerOperation(Summary = "Sends complains to the db")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sends complains to the db", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NU\\\\MBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> UserComplains(ComplainRequest complain)
        {
            var model = await _transactionsServices.FileComplainAsync(complain);
            return Ok(model);
        }

        // PUT api/values/5
        [HttpPut("transfer-money-to-another-user")]
        [SwaggerOperation(Summary = "Gets name with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Performs transfer with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> Transfer(TransferRequest transfer)
        {
            Response model = await _transactionsServices.TransferMoneyAsync(transfer);
            return Ok(model);
        }
    }
}


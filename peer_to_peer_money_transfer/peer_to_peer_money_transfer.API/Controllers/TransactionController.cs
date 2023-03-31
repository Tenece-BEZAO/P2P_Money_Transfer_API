using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using peer_to_peer_money_transfer.BLL.Infrastructure;
using peer_to_peer_money_transfer.DAL.Dtos.Responses;
using Microsoft.AspNetCore.Identity;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.DAL.Interfaces;
using peer_to_peer_money_transfer.BLL.Interfaces;
using peer_to_peer_money_transfer.DAL.Dtos.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace peer_to_peer_money_transfer.API.Controllers
{
    [Route("CashMingle/[controller]")]
    public class TransactionController : Controller
    {
        private readonly  ITransactionServices _transactionsServices;
        public TransactionController(ITransactionServices transactionsServices)
        {
            _transactionsServices = transactionsServices;
        }

        // GET: api/values
        [HttpGet("get-transactionHistories-name")]
        [SwaggerOperation(Summary = "Gets name with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets transaction history with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<TransactionHistoryResponse>> User_Transaction_History()
        {
            TransactionHistoryResponse model = await _transactionsServices.GetTransactionHistoriesAsync();

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

        [AllowAnonymous]
        [HttpGet("get-balance-name")]
        [SwaggerOperation(Summary = "Gets balance with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets name with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> Balance(AccountNumberRequest accountNumber)
        {
            Response model = await _transactionsServices.GetBalanceAsync(accountNumber);

            return Ok(model);
        }

      
        [HttpPost("file-complains")]
        [SwaggerOperation(Summary = "Sends complains to the db")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sends complains to the db", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Response>> User_Complains(ComplainRequest complain)
        {
            var model = await _transactionsServices.FileComplainAsync(complain);

            return Ok(model);
        }
        // PUT api/values/5
        [AllowAnonymous]
        [HttpPut("transfer-money-to-Another-User")]
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


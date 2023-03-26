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
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets name with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<TransactionHistoryResponse>>> Get(LoginVerifyRequest loginVerify)
        {
            var model = await _transactionsServices.GetTransactionHistoriesAsync(loginVerify);

            return Ok(model);
        }

        // GET api/values/5
 
        [AllowAnonymous]
        [HttpGet("get-receiver-name")]
        [SwaggerOperation(Summary = "Gets name with AccountNumber")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets name with AccountNumber", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "ACCOUNT NUMBER NOT FOUND", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ReceiverNameResponse>> GetReceiverName(AccountNumberRequest User)
        {
            

            ReceiverNameResponse model = await _transactionsServices.GetReceiverNameAsync(User) ;
            
            return Ok(model);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


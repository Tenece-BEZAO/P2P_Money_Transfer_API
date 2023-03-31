using System;
namespace peer_to_peer_money_transfer.DAL.Dtos.Responses
{
    public class ResponseStatus
    {
        public bool Success { get; set;}
        
        public object Data { get; set; }
    }
}


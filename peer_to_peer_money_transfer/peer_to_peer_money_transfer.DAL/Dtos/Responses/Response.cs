using System;
namespace peer_to_peer_money_transfer.DAL.Dtos.Responses
{
    public class Response
    {
        public bool success { get; set;}

        public object data { get; set; }
    }
}


using System;
namespace peer_to_peer_money_transfer.DAL.Entities
{
    public class BusinessUser : BaseEntities
    {
        public string BusinessName { get; set; } = null!; 

        public string CAC_NO { get; set; } = null!;

        public string Businesstype { get; set; } = null!;

    }
}


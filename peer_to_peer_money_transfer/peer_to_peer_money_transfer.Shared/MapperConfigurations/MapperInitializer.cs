using AutoMapper;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;

namespace peer_to_peer_money_transfer.Shared.MapperConfigurations
{
    internal class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApplicationUser, RegisterDTO>().ReverseMap();
        }
    }
}

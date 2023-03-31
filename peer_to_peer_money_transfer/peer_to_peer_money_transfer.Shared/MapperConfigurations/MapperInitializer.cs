using AutoMapper;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.DataTransferObject;

namespace peer_to_peer_money_transfer.Shared.MapperConfigurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApplicationUser, RegisterAdminDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterIndividualDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterBusinessDTO>().ReverseMap();
            CreateMap<ApplicationUser, GetCharacterDTO>().ReverseMap();
        }
    }
}

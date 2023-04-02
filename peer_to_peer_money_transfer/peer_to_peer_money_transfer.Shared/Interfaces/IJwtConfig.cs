using Microsoft.IdentityModel.Tokens;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace peer_to_peer_money_transfer.Shared.Interfaces
{
    public interface IJwtConfig
    {
        Task<bool> ValidateUser(LoginDTO loginDTO);
        Task<string> GenerateJwtToken(ApplicationUser user);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using peer_to_peer_money_transfer.DAL.DataTransferObject;
using peer_to_peer_money_transfer.DAL.Entities;
using peer_to_peer_money_transfer.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace peer_to_peer_money_transfer.Shared.JwtConfigurations
{
    public class JwtConfig : IJwtConfig
    {
        private ApplicationUser _user;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtConfig(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken()
        {
            var signInCredentials = GetSignInCredentials();
            var claims = await GetClaims();
            var jwtToken = GenerateToken(signInCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private SigningCredentials GetSignInCredentials()
        {
            //var Key = Environment.GetEnvironmentVariable("Key");
            var Key = _configuration.GetSection("Key").Value;
            var encodeKey = Encoding.UTF8.GetBytes(Key);
            var signInCredential = new SymmetricSecurityKey(encodeKey);

            return new SigningCredentials(signInCredential, SecurityAlgorithms.HmacSha256);

        }

        private async Task<List<Claim>> GetClaims()
        {
            // Create some claims for the token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signInCredentials, List<Claim> claims)
        {
            /*var issuer = Environment.GetEnvironmentVariable("Issuer");
            var lifetime = Environment.GetEnvironmentVariable("Lifetime");*/
            var issuer = _configuration.GetSection("Issuer").Value;
            var lifetime = _configuration.GetSection("Lifetime").Value;
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(lifetime));

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: expires,
                signingCredentials: signInCredentials
            );

            return token;
        }


        public async Task<bool> ValidateUser(LoginDTO loginDTO)
        {
            _user = await _userManager.FindByNameAsync(loginDTO.UserName);

            return _user != null && await _userManager.CheckPasswordAsync(_user, _user.PasswordHash);
        }
    }
}

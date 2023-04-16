using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peer_to_peer_money_transfer.DAL.Entities;
using System.Security.Claims;

namespace peer_to_peer_money_transfer.API.Controllers
{
    [ApiController]
    [Route("CashMingle/[controller]")]
    [Authorize(Policy = "SuperAdmin")]
    public class Roles_And_ClaimsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;

        public Roles_And_ClaimsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<Roles_And_ClaimsController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var rolesExist = await _roleManager.RoleExistsAsync(name);

            if (rolesExist) return BadRequest(new { error = "Role already exists" });

            var createRole = await _roleManager.CreateAsync(new ApplicationRole { Name = name });

            if (!createRole.Succeeded)
            {
                _logger.LogInformation($"Attempt to create {name} role failed");
                return BadRequest(new { error = $"Attempt to create {name} role failed" });
            }

            _logger.LogInformation($"The Role {name} was created successfully");
            return Ok(new { result = $"The Role {name} was created successfully" });
        }

        [HttpPost("add-user-to-role")]
        public async Task<IActionResult> AddUserToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return BadRequest(new { error = "User does not exists" });

            var role = await _roleManager.RoleExistsAsync(roleName);

            if (!role)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new { error = $"The role {roleName} does not exist" });
            }

            var addUserToRole = await _userManager.AddToRoleAsync(user, roleName);

            if (!addUserToRole.Succeeded)
            {
                _logger.LogInformation("User addition to Role failed");
                return BadRequest(new { error = "User addition to Role failed" });
            }

            return Ok(new { result = "Success" });
        }

        [HttpGet("get-user-roles")]
        public async Task<IActionResult> GetUserRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                _logger.LogInformation("User does not exists");
                return BadRequest(new { error = "User does not exists" });
            }
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost("remove-user-from-role")]
        public async Task<IActionResult> RemoveUserFromRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                _logger.LogInformation("User does not exists");
                return BadRequest(new { error = "User does not exists" });
            }

            var role = await _roleManager.RoleExistsAsync(roleName);

            if (!role)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new { error = $"The role {roleName} does not exist" });
            }

            var removeUserFromRole = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!removeUserFromRole.Succeeded)
            {
                _logger.LogInformation($"User removal from Role {roleName} failed");
                return BadRequest(new { error = $"User removal from Role {roleName} failed" });
            }

            return Ok(new { result = $"User {userName} has been removed from role {roleName}" });
        }

        [HttpGet("get-all-claims")]
        public async Task<IActionResult> GetAllClaims(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                _logger.LogInformation("User does not exists");
                return BadRequest(new { error = "User does not exists" });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return Ok(userClaims);
        }

        [HttpPost("add-claim-to-user")]
        public async Task<IActionResult> AddClaimToUser(string userName, string claimName, string claimValue)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                _logger.LogInformation("User does not exists");
                return BadRequest(new { error = "User does not exists" });
            }

            var userClaim = new Claim(claimName, claimValue);

            var addClaimToUser = await _userManager.AddClaimAsync(user, userClaim);

            if (!addClaimToUser.Succeeded)
            {
                return BadRequest(new { error = $"Attempt to add claim {claimName} to {user} failed" });
            }

            return Ok(new { result = $"claim {claimName} has been added to {user}" });
        }

        [HttpPost("remove-user-claim")]
        public async Task<IActionResult> RemoveUserClaim(string userName, string claimName, string claimValue)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                _logger.LogInformation("User does not exists");
                return BadRequest(new { error = "User does not exists" });
            }

            var userClaim = new Claim(claimName, claimValue);

            var removeUserClaim = await _userManager.RemoveClaimAsync(user, userClaim);

            if (!removeUserClaim.Succeeded)
            {
                return BadRequest(new { error = $"Attempt to remove claim {claimName} from {user} failed" });
            }

            return Ok(new { result = $"claim {claimName} has been removed from {user}" });
        }
    }
}

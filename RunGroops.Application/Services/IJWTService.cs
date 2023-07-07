using Microsoft.AspNetCore.Identity;
using RunGroops.Application.Models;
using System.Security.Claims;

namespace RunGroops.Application.Services
{
    public interface IJWTService
    {
        string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims);
    }
}

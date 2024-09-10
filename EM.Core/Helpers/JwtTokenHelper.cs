using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.Helpers
{
    public static class JwtTokenHelper
    {
        public static int GetOrganizerIdFromToken(string authHeader)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = authHeader.Substring("Bearer ".Length).Trim();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var organizerClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int organizerId = int.Parse(organizerClaim);
            return organizerId;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SAE401_API.Models.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SAE401_API.Models.DataMethods
{
    public class JwtManager
    {
        public static List<Claim> GetClaims(Client client)
        {
            var claims = new List<Claim>()
            {
                 // #claims1#
                 new Claim(JwtRegisteredClaimNames.Sub, client.Idclient.ToString()),
                 new Claim("id", client.Idclient.ToString()),
                 new Claim("email", client.Emailclient.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }

        public static string GenerateJwtToken(Client client)
        {
            string defaultToken = "NeverGonnaGiveYouUpNeverGonnaLetYouDownNeverGonnaRunAroundOrHurtYou";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? defaultToken));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetClaims(client);
            var token = new JwtSecurityToken
            (
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ControllerContext CreateControllerContext(Client client)
        {
            var identity = new ClaimsIdentity(GetClaims(client), "Bearer");
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext();
            httpContext.User = principal;
            var ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            return ControllerContext;
        }
    }
}

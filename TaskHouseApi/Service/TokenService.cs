using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace TaskHouseApi.Service
{
    public class TokenService : ITokenService
    {
        private IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        // Generates a access token with claims
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //config["Jwt:Issuer"],
                //config["Jwt:Issuer"],
                audience: "Anyone",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Generates a refresh token, this is use when the access token is no longer valid
        public string GenerateRefreshToken()
        {
            // Generates a random byte array
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                // Encodes byte array to base64 string,
                // so it can be send as a token
                return Convert.ToBase64String(randomNumber);
            }
        }

        // Extracts the claims from expired token
        // Return null if token is not expired
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            var expireTimeInMilli = principal.Claims.Where(c => c.Type == "exp").Select(c => c.Value).SingleOrDefault();

            // Checks if token is expired
            if (CalculateExpireTime(expireTimeInMilli) > DateTime.UtcNow)
            {
                return null;
            }

            // Checks if token is valid
            if (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
            )
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        // Calculates datatime of expire time
        private DateTime CalculateExpireTime(string milli) 
        {
            long ticks = long.Parse(milli + "000");

            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            var time = posixTime.AddMilliseconds(ticks);

            return time;
        }
    }
}
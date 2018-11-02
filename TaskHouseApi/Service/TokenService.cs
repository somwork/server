using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(10),
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
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken)
        {
            var t = isAccessTokenValid(expiredToken);
            if (!t.result)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return t.principal;
        }

        public (bool result, ClaimsPrincipal principal) isAccessTokenValid(string expiredToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if
            (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
            )
            {
                return (false, null);
            }

            return (true, principal);
        }

        public bool isAccessTokenExpired(string token)
        {
            if (!isAccessTokenValid(token).result)
            {
                return false;
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null)
            {
                return false;
            }

            return true;
        }
    }
}
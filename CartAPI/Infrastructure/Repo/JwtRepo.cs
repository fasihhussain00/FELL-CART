using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class JwtRepo : IJwtRepo
    {
        private readonly JwtSettings _jwtSettings;
        private readonly DefaultDBManager _connection;

        public JwtRepo(IOptions<JwtSettings> jwtSettings, DefaultDBManager connection)
        {
            _jwtSettings = jwtSettings.Value;
            _connection = connection;
        }
        public async Task<Tokens> GenerateToken(Customer customer)
        {
            var key = _jwtSettings.Key;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", customer.ID.ToString()),
                    new Claim("name", customer.Name),
                    new Claim("email", customer.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.JwtTimeSpan),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256)
            };

            var Securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var accesstoken = tokenHandler.WriteToken(Securitytoken);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenInp = new RefreshToken
            {
                ID = Guid.NewGuid(),
                UserId = customer.ID,
                Token = refreshToken,
                JwtId = Securitytoken.Id,
                IsUsed = false,
                IsRevoked = false,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                CreateDate = DateTime.UtcNow
            };

            await AddUserRefreshTokens(refreshTokenInp);
            return new Tokens { Token = accesstoken, RefreshToken = refreshToken };
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public bool ValidateAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            var tokenVerification =
                tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);

                if (!result) return false;
            }
            return true;
        }
        public bool ValidateRefreshToken(string accessToken, RefreshToken refreshToken)
        {
            if (refreshToken == null) return false;
            if (refreshToken.IsUsed) return false;
            if (refreshToken.IsRevoked) return false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtId = tokenHandler.ReadJwtToken(accessToken).Payload.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            return jwtId == refreshToken.JwtId;
        }
        public async Task<RefreshToken> AddUserRefreshTokens(RefreshToken refreshToken)
        {
            var parameters = new Dictionary<string, object>
            {
                {"id",  refreshToken.ID },
                {"customerid", refreshToken.UserId },
                {"token", refreshToken.Token },
                {"jwtid", refreshToken.JwtId },
                {"isused", refreshToken.IsUsed },
                {"isrevoked", refreshToken.IsRevoked },
                {"expirydate", refreshToken.ExpiryDate },
                {"createdate", refreshToken.CreateDate },
            };
            var createdRefreshToken = await _connection.QueryAsync<RefreshToken>(StoredProcedures.SaveRefreshToken, parameters);
            return createdRefreshToken;

        }
        public void DeleteUserRefreshTokens(string email, string refreshToken)
        {
        }//TODO
        public RefreshToken GetSavedRefreshTokens(string email, string refreshToken)
        {
            return null;
        }//TODO
    }
}

using CartAPI.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace CartAPI.Application.IRepo
{
    public interface IJwtRepo
    {
        Task<RefreshToken> AddUserRefreshTokens(RefreshToken user);
        void DeleteUserRefreshTokens(string email, string refreshToken);
        string GenerateRefreshToken();
        Task<Tokens> GenerateToken(Customer customer);
        RefreshToken GetSavedRefreshTokens(string email, string refreshToken);
        bool ValidateAccessToken(string accessToken);
        bool ValidateRefreshToken(string accessToken, RefreshToken refreshToken);
    }
}

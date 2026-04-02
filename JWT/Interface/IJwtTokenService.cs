using ECommerce.Models;

namespace ECommerce.JWT.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, List<string> roles);
    }
}

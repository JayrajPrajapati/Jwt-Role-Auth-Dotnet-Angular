using ECommerce.DTOs;

namespace ECommerce.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}

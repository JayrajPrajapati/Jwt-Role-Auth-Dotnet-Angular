using ECommerce.Database;
using ECommerce.DTOs;
using ECommerce.JWT.Interface;
using ECommerce.Models;
using ECommerce.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJwtTokenService _jwtService;
        public AuthService(AppDbContext context, IConfiguration config, IJwtTokenService jwtService)
        {
            _context = context;
            _config = config;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserDto dto)
        {
            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                throw new Exception("User already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Address=dto.Address,
                IsActive=dto.IsActive,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 🔥 Handle dynamic roles
            var roles = new List<Role>();

            foreach (var roleName in dto.Roles)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

                if (role != null)
                    roles.Add(role);
            }

            if (!roles.Any())
                throw new Exception("Invalid roles provided");

            foreach (var role in roles)
            {
                _context.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                });
            }

            await _context.SaveChangesAsync();

            var roleNames = roles.Select(r => r.Name).ToList();

            var token = _jwtService.GenerateToken(user, roleNames);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:ExpiryMinutes"])
                ),
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Roles = roleNames,
                    Address = dto.Address,
                    IsActive = dto.IsActive,
                    PhoneNumber = dto.PhoneNumber,
                    CreatedAt = user.CreatedAt
                }
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var roles = user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();

            var token = _jwtService.GenerateToken(user, roles);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:ExpiryMinutes"])
                ),
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Roles = roles,
                    Address = user.Address,
                    IsActive = user.IsActive,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAt = user.CreatedAt
                }
            };
        }
    }
}

using RunGroops.Application.Models;

namespace RunGroops.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(UserLoginRequest userLoginModel);
        Task<bool> Logout();
        Task<AuthResponse> Register(UserRegisterRequest userRegisterModel);
    }
}

using Microsoft.AspNetCore.Identity;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<AuthResponse> Login(UserLoginRequest userLoginModel)
        {
            if (!await UserExists(userLoginModel.Email))
            {
                return new AuthResponse
                {
                    Message = "User does not exist.",
                    StatusCode = 404,
                    isSuccess = false
                };
            }
            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);

            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, userLoginModel.Password, false, false);

                return result.Succeeded ? 
                new AuthResponse
                {
                    Message = "Login successful!",
                    StatusCode = 200,
                    isSuccess = true
                }
                : new AuthResponse
                {
                    Message = "Something went wrong",
                    StatusCode = 500,
                    isSuccess = false
                };

            }
            return new AuthResponse
            {
                Message = "Incorrect email or password.",
                StatusCode = 400,
                isSuccess = false
            };
        }
        public async Task<AuthResponse> Register(UserRegisterRequest userRegisterModel)
        {
            if (await UserExists(userRegisterModel.Email))
            {
                return new AuthResponse
                {
                    Message = "User already exists.",
                    StatusCode = 403,
                    isSuccess = false
                };
            }

            var newUser = new AppUser()
            {
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.Email
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, userRegisterModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return new AuthResponse
                {
                    Message = "Registration successful!",
                    StatusCode = 200,
                    isSuccess = true
                };
            }
            return new AuthResponse
            {
                Message = "Registration Failed",
                StatusCode = 500,
                isSuccess = false
            };
        }
        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
        private async Task<bool> UserExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null ? true : false;
        }
    }
}


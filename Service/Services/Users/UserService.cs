﻿using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using DB_CSharp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signinManager;
        private readonly IConfiguration configuration;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
            this.configuration = configuration;
        }
        public async Task<ApiResult<bool>> Register(UserRegisterRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Account already exists");
            }
            if (await userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email already exists");
            }
            user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>(string.Join("|", result.Errors.Select(x => x.Code).ToArray()));
            }
        }
        public async Task<ApiResult<string>> Login(UserLoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Account does not exists");
            var result = await signinManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Wrong User Name or PassWord");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Tokens:Issuer"],
                configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
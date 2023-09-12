using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountService(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<UserDTO> Register(RegisterDTO registerDTO)
        {
            if(await UserExists(registerDTO.UserName)) return null;
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                EmailAddress = registerDTO.EmailAddress,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                PhoneNumber = registerDTO.PhoneNumber,
                Address = registerDTO.Address
            };
            Console.WriteLine(user.UserName + " " + user.EmailAddress + " " + user.PasswordHash + " " + user.PasswordSalt);
            _context.Users.Add(user);
            _context.ShoppingCarts.Add(new ShoppingCart { 
                AppUser = user,
                AppUserId = user.Id,
                Status = "Active",
             });
            await _context.SaveChangesAsync();
            
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        
    }
}
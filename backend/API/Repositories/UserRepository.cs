using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public AppUser GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public AppUser GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(user => user.UserName == username);
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
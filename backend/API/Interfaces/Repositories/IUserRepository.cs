using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        AppUser GetUser(int id);
        IEnumerable<AppUser> GetUsers();
        AppUser GetUserByUsername(string username);
    }
}
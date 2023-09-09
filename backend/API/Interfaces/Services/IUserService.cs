using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserInfoDTO> GetUserInfo(string username);
        Task<IEnumerable<UserInfoDTO>> GetUsersInfo();

    }
}
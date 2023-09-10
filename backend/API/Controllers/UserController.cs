using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.DTOs;
using API.Interfaces.Services;
using System.Security.Claims;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _service.GetUsersInfo());
        }
        [HttpGet("info")]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _service.GetUserInfo(username);
        }
        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForUser()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _service.GetReviewsForUser(username));
        }
    }
}
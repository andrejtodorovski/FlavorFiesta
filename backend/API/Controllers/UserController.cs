using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.DTOs;
using API.Interfaces.Services;
using System.Security.Claims;
using ExcelDataReader;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _service;
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;
        public UserController(IUserService service, IAccountService accountService, IMailService mailService)
        {
            _service = service;
            _accountService = accountService;
            _mailService = mailService;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserInfoDTO>>> GetUsers()
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
        [HttpPost("import")]
        public async Task<IActionResult> ImportUsers()
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
            {           
                return BadRequest();
            }
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0).ToString() == "Email") continue;
                        var dto = new RegisterDTO
                        {
                            EmailAddress = reader.GetValue(0).ToString(),
                            UserName = reader.GetValue(1).ToString(),
                            Password = reader.GetValue(2).ToString(),
                        };
                        var userCheck = await _service.UsernameExists(dto.UserName);
                        if (!userCheck)
                        {
                            await _accountService.Register(dto);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return Ok();
        }
        [HttpPost("contact")]
        public async Task<ActionResult> SendEmail(EmailMessage mail)
        {
            await _mailService.SendEmailAsync(mail);
            return Ok();
        }
    }
}
using API.DTOs;

namespace API.Interfaces.Services
{
    public interface IMailService
    {
        public Task SendEmailAsync(EmailMessage mail);

    }
}
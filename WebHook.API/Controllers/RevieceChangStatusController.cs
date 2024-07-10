using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebHook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RevieceChangStatusController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public RevieceChangStatusController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail([FromBody] WebhookPayload payload)
        {
            // Xử lý dữ liệu từ webhook tại đây
            await _emailSender.SendEmailAsync("phantheviet2906@gmail.com", "Test Subject", "This is a test email.");
            return Ok("Email sent.");

        }
    }

    public class WebhookPayload
    {
        public string EventName { get; set; }
        public string Data { get; set; }
    }
}

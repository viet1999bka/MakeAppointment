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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail([FromBody] WebhookPayload payload)
        {
            // Xử lý dữ liệu từ webhook tại đây
            Console.WriteLine("Bắn email tới người dùng");
            return Ok();
        }
    }

    public class WebhookPayload
    {
        public string EventName { get; set; }
        public string Data { get; set; }
    }
}

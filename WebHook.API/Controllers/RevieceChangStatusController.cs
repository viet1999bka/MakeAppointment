using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WebHook.API.Contract;

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
            //// Xử lý dữ liệu từ webhook tại đây
            //var botToken = "7444504488:AAFG9XteLb0wlNC0YImjtCXJ2DiAny8EfbU"; // Thay thế bằng API Token của bot
            //var chatId = 123456789; // Thay thế bằng chat ID của người nhận hoặc nhóm
            //var message = "Hello from .NET!";

            //var telegramMessageSender = new TelegramMessageSender(botToken);
            //await telegramMessageSender.SendMessageAsync(chatId, message);
            using HttpClient client = new HttpClient();

            // Đặt URL của API hoặc trang web bạn muốn gửi yêu cầu GET
            string url = $@"https://api.telegram.org/bot7057647874:AAH_Oj6fQxMmPtUHaG43xVSYN3gbey51WCk/sendMessage?chat_id=@webhookviet&text={payload.EventName.ToString()}";

            try
            {
                // Gửi yêu cầu GET và nhận phản hồi
                HttpResponseMessage response = await client.GetAsync(url);

                // Đảm bảo yêu cầu thành công trước khi xử lý phản hồi
                response.EnsureSuccessStatusCode();

                // Đọc nội dung phản hồi như một chuỗi
                string responseBody = await response.Content.ReadAsStringAsync();

                // In nội dung phản hồi ra màn hình console
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                // Bắt và xử lý các lỗi liên quan đến yêu cầu HTTP
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Ok();
        }
    }

    public class WebhookPayload
    {
        public string EventName { get; set; }
        public string Data { get; set; }
    }

}

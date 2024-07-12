using Newtonsoft.Json;
using System.Text;

namespace WebHook.API.Contract
{
    public class TelegramMessageSender
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string botToken;
        private readonly string apiUrl;

        public TelegramMessageSender(string botToken)
        {
            this.botToken = botToken;
            this.apiUrl = $"https://api.telegram.org/bot{botToken}";
        }

        public async Task SendMessageAsync(long chatId, string message)
        {
            var messagePayload = new
            {
                chat_id = chatId,
                text = message
            };

            var content = new StringContent(JsonConvert.SerializeObject(messagePayload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiUrl}/sendMessage", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Message sent successfully.");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error sending message: {responseContent}");
            }
        }
    }
}

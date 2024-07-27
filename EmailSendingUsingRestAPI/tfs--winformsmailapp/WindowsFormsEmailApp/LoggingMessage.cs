using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsEmailApp
{
    public class LoggingMessage
    {
        public DateTime datetime;
        public string method;

        public LoggingMessage()
        {
            datetime = DateTime.Now;
            method = "";
        }

        public async Task LogMethodCallAsync(LoggingMessage message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                var json = JsonConvert.SerializeObject(message);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("Loggings/method", byteContent).Result;

            }
        }
    }
}

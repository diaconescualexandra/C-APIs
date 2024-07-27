using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MailMethodsApi
{
    public class LoggingException
    {
        public DateTime date;
        public string method;
        public Exception exception;

        public LoggingException ()
        {
            date = DateTime.Now;
            method = "";
            exception = null;
        }
        public async Task LogExceptionCall(LoggingException exception)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                var json = JsonConvert.SerializeObject(exception);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("Loggings/exception", byteContent).Result;

            }
        }
    }
}

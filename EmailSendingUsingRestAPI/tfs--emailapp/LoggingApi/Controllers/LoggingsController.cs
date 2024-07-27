using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoggingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingsController : ControllerBase
    {
        private readonly string logFilePath;
        private readonly string cleanedPath;
        private readonly string fullPath;
        private List<string> logMessages;

        public LoggingsController()
        {
            logFilePath = Assembly.GetEntryAssembly().Location;
            cleanedPath = Path.GetDirectoryName(logFilePath);
            fullPath = Path.Combine(cleanedPath, "log.json");
            //logMessages = new List<string>();

        }
        private void LoadLogMessages()
        {
            string fullPath = Path.Combine(cleanedPath, "log.json");

            if (System.IO.File.Exists(fullPath))
            {
                var existingJson = System.IO.File.ReadAllText(fullPath);
                logMessages = JsonConvert.DeserializeObject<List<string>>(existingJson) ?? new List<string>();
            }
            else
            {
                logMessages = new List<string>();
            }
        }

        private async Task SaveLogMessagesAsync()
        {
            string fullPath = Path.Combine(cleanedPath, "log.json");
            var updatedJson = JsonConvert.SerializeObject(logMessages, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(fullPath, updatedJson);
        }

        [HttpPost("method")]
        public async Task<IActionResult> LogMethodCallAsync()
        {

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var logMessage = JsonConvert.DeserializeObject<LoggingMessage>(body);
                string logMessageString = $"{logMessage.datetime}: Method : {logMessage.method} was called";
                LoadLogMessages();
                logMessages.Add(logMessageString);

                await SaveLogMessagesAsync();
            }
                return Ok();
        }

        [HttpPost("exception")]
        public async Task<IActionResult> LogExceptionCall()
        {

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var logException = JsonConvert.DeserializeObject<LoggingException>(body);           
                string logExceptionString = $"{logException.date}: Error {logException.exception} from method : {logException.method} ";
                LoadLogMessages();
                logMessages.Add(logExceptionString);
                await SaveLogMessagesAsync();
            }
            return Ok();
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;


namespace MailMethodsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {

        public EmailData2 emaildata;
        public LoggingMessage message = new LoggingMessage();
        public LoggingException exception = new LoggingException();
        //recipient pass: RecipientAddress509!StRoNg

        [HttpPost("retrieveData")]
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    var emaildata = JsonConvert.DeserializeObject<EmailData2>(body);
                    if (emaildata == null)
                    {
                        return BadRequest();
                    }
                    // Process emailData

                    message.datetime = DateTime.Now;
                    message.method = "retrieve data";
                    await message.LogMethodCallAsync(message);
                    await SendEmail(emaildata);

                }
                return Ok();

            }
            catch (Exception ex)
            {
                exception.date = DateTime.Now;
                exception.exception = ex;
                exception.method = "retrieve data";
                await exception.LogExceptionCall(exception);
                return StatusCode(500, "error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailData2 emaildata)
        {
            try
            {
                // Set up the SMTP client
                SmtpClient smtpClient = new SmtpClient(emaildata.smtpServer)
                {
                    Credentials = new NetworkCredential(emaildata.smtpUser, emaildata.smtpPass),
                    EnableSsl = true,
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(emaildata.to, "Your Name");
                mailMessage.Subject = emaildata.subject;
                mailMessage.CC.Add(emaildata.cc);
                mailMessage.Bcc.Add(new MailAddress(emaildata.bcc));
                mailMessage.Body = emaildata.emailContent;
                mailMessage.IsBodyHtml = true;


                // Add recipient
                mailMessage.To.Add(emaildata.toAddress);

                // Send the email
                smtpClient.Send(mailMessage);

                message.datetime = DateTime.Now;
                message.method = "email sent ";
                await message.LogMethodCallAsync(message);
                return Ok();
            }
            catch (Exception ex)
            {
                exception.date = DateTime.Now;
                exception.exception = ex;
                exception.method = "retrieve data";
                await exception.LogExceptionCall(exception);
                return StatusCode(500, "error");
            }
        }

       

        
    }

}


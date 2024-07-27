using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WindowsFormsEmailApp
{
    public partial class Form1 : Form
    {
        public LoggingException exception;
        public LoggingMessage message;
        public Form1()
        {
            InitializeComponent();
            exception = new LoggingException();
            message = new LoggingMessage();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            EmailData emaildata = new EmailData();
            emaildata.cc = textBox2.Text;
            emaildata.to = textBox1.Text;
            emaildata.bcc = textBox3.Text;
            emaildata.emailContent = richTextBox1.Text;
            emaildata.subject = textBox4.Text;

            if (string.IsNullOrEmpty(emaildata.to) && string.IsNullOrEmpty(emaildata.cc) && string.IsNullOrEmpty(emaildata.bcc))
            {
                exception.date = DateTime.Now;
                exception.exception = "email data is incomplete";
                exception.method = "send button";
                await exception.LogExceptionCall(exception);
                MessageBox.Show("email data is incomplete");

            }
            else
            {
                message.datetime = DateTime.Now;
                message.method = "send email button";
                await message.LogMethodCallAsync(message);
                SendDataToApi(emaildata);
            }

        }

        private async void SendDataToApi (EmailData emaildata)
        {
            using ( var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44368/");
                var json = JsonConvert.SerializeObject(emaildata);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("api/Mails/retrieveData", byteContent).Result;
                //HttpResponseMessage response = await client.PostAsJsonAsync("api/Mails/retrieveData", content);
                //bool returnValue = await response.Content.ReadAsAsync<bool>();
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Email data sent successfully!");
                }
                else
                {
                    MessageBox.Show("Error sending email data.");
                }
            }
        }

       
    }
}

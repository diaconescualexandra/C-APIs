using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMethodsApi
{
    public class EmailData2
    {
        public string to;
        public string cc;
        public string bcc;
        public string emailContent;
        public string subject;
        public string smtpServer = "smtp.gmail.com";
        public int smtpPort = 587;
        public string smtpUser = "fromaddress6@gmail.com"; // Adresa ta de Gmail
        public string smtpPass = "gkga zrkt kuxv owvu"; // Parola ta de Gmail sau parola aplicației
        public string fromAddress = "fromaddress6@gmail.com"; // Adresa ta de Gmail
        public string toAddress = "recipientaddress509@gmail.com"; // Adresa destinatarului
        public string body = "Acesta este corpul email-ului.";
    }
}

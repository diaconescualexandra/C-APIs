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
        public string smtpUser = ""; // Adresa ta de Gmail
        public string smtpPass = ""; // Parola ta de Gmail sau parola aplicației
        public string fromAddress = ""; // Adresa ta de Gmail
        public string toAddress = ""; // Adresa destinatarului
        public string body = "Acesta este corpul email-ului.";
    }
}

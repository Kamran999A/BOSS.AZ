using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;

namespace Bossaz.Network
{
    public static class Mail
    {
        public static bool IsEnable { get; set; }
        private static SmtpClient SmtpClient { get; set; }
        public static string SenderAddress { get; }
        private static string SenderPassword { get; }
        static Mail()
        {
            var file = @"Data\Config.json";

            if (File.Exists(file))
            {
                var json = string.Empty;

                using (var fs = File.OpenRead(file))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = sr.ReadToEnd();

                var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

                SenderAddress = configJson.Mail;
                SenderPassword = configJson.Password;

                IsEnable = true;

                SmtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(SenderAddress, SenderPassword),
                    EnableSsl = true
                };
            }
        }
        public static void SendMail(in string recipient, in string subject, in string body, in string body2)
        {
            // string html = $@"<h1 style='background-color:#00bfff;padding:25px;border-radius:15px;border:5px solid red'>" + body + "</h1>";
            string html = $@"<h1 style='background-color:#ffd700;padding:25px;border-radius:15px;border:5px solid red'>" + body + "</h1>" +
                "<h2 style='background-color:#98f5ff;padding:25px;border-radius:15px;border:5px solid red'>" + body2 + "</h2>";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SenderAddress);
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = html;
                SmtpClient.Send(mail);
            }
        }
    }
}
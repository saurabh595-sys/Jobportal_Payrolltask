using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DevRequired.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string to,  StringBuilder builder, string Subject, string bcc,string CC)
        {
           await Execute(to,  builder, Subject, bcc,CC);
            return true;
        }
        public async Task Execute(string to, StringBuilder builder, string Subject , string bcc,string CC)
        {
            try
            {
                
                    MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress(_configuration["MailSettings:From"], _configuration["MailSettings:DisplayName"])
                    };
                    to.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(t => mail.To.Add(new MailAddress(t)));
                     
                if (bcc != "" )
                {
                    mail.Bcc.Add(bcc);
                }
                CC.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(t => mail.CC.Add(new MailAddress(t)));

                    mail.Subject = Subject;
                    mail.Body = builder.ToString();
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    using (SmtpClient smtp = new SmtpClient(_configuration["MailSettings:Host"], Convert.ToInt32(_configuration["MailSettings:Port"])))
                    {
                        smtp.Credentials = new NetworkCredential(_configuration["MailSettings:From"], _configuration["MailSettings:Password"]);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevRequired.Service
{
   public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, StringBuilder builder,string Subject, string bcc,string CC);
    }
}

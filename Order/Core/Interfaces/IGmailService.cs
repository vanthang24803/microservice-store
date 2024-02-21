using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Core.Utils;

namespace Order.Core.Interfaces
{
    public interface IGmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        
    }
}
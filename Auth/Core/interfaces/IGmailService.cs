using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Utils;

namespace Auth.Core.interfaces
{
    public interface IGmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
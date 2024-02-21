using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Core.Utils
{
    public class MailSettings
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string DisplayName { get; set; }

        public int Port { get; set; }
    }
}
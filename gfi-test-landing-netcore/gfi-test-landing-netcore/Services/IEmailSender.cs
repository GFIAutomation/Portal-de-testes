using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gfi_test_landing_netcore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BaseProject.Areas.Identity.Services
{
    public class TwilioSmsSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}

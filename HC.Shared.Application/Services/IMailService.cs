using HC.Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Application.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}

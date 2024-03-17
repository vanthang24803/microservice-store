using Product.Core.Utils;

namespace Product.Core.Interfaces
{
    public interface IMailService
    {
         Task SendEmailAsync(MailRequest mailRequest);
    }
}
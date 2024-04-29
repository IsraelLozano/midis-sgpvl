using MIDIS.SGPVL.Utils.Dtos;

namespace MIDIS.SGPVL.Utils.Helpers.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailAsync(Message message);
        Task SendEmailRestauraClaveAsync(Message message);
    }
}
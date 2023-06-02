using Infrastructure.EventHandlers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service for sending mail and checking if it was sent successfully
    /// </summary>
    public class MailSendOkServices : IMailSendOkServices
    {
        /// <summary>
        /// Sends the mail and returns a boolean value indicating if any exception occurred
        /// </summary>
        public async Task<bool> SendMailOk()
        {
            return NotificationEmailAddedAssetEventHandler.SendMail;
        }
    }
}


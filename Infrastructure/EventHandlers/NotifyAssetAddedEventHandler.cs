using Domain.Assets.Aggregates.Events;
using Shared.DomainEvent.Handler;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Infrastructure.Exceptions;
using Domain.Assets.ValueObjectModels;

namespace Infrastructure.EventHandlers
{
    /// <summary>
    /// Event handler for notifying asset addition
    /// </summary>
    public class NotificationEmailAddedAssetEventHandler : IDomainEventHandler<NotifyAssetAdded>
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger<NotificationEmailAddedAssetEventHandler> _logger;
        public static bool SendMail = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEmailAddedAssetEventHandler"/> class
        /// </summary>
        /// <param name="emailConfiguration">The email configuration options</param>
        /// <param name="logger">The logger.</param>
        public NotificationEmailAddedAssetEventHandler(IOptions<EmailConfiguration> emailConfiguration, ILogger<NotificationEmailAddedAssetEventHandler> logger)
        {
            _emailConfiguration = emailConfiguration.Value;
            _logger = logger;
        }

        /// <summary>
        /// Handles the asset added event by sending a notification email
        /// </summary>
        /// <param name="domainEvent">The asset added event</param>
        public void Handle(NotifyAssetAdded domainEvent)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailConfiguration.UserName));
                email.To.Add(MailboxAddress.Parse(domainEvent.DepartmentMail));
                email.Subject = "New Asset in " + domainEvent.Department + " department.";
                email.Body = new TextPart(TextFormat.Text)
                {
                    Text = "A new asset has been registered: " + domainEvent.Name
                };
                using var smtp = new SmtpClient();
                smtp.Connect(
                        _emailConfiguration.Host,
                        _emailConfiguration.Port,
                        SecureSocketOptions.StartTls
                    );
                smtp.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                SendMail = false;
                _logger.LogError(ex, "An error occurred while sending the notification email.");
                throw new EmailNotificationException("An error occurred while sending the notification email.", ex);
            }
        }
    }
}

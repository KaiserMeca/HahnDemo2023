using Shared.Model;

namespace Domain.Assets.ValueObjectModels
{
    /// <summary>
    /// Represents the value object used to send email.
    /// </summary>
    public class EmailConfiguration : ValueObject
    {
        /// <summary>
        /// Gets or sets the host of the email configuration
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the email configuration
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username of the email configuration
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the email configuration
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Retrieves the atomic values of the email configuration.
        /// </summary>
        /// <returns>An enumeration of the atomic values.</returns>
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Host;
            yield return Port;
            yield return UserName;
            yield return Password;
        }
    }
}

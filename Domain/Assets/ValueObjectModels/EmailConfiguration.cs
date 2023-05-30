using Shared.Model;

namespace Domain.Assets.ValueObjectModels
{
    public class EmailConfiguration : ValueObject
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Host;
            yield return Port;
            yield return UserName;
            yield return Password;
        }
    }
}

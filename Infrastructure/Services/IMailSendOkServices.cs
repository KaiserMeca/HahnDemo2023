namespace Infrastructure.Services
{
    public interface IMailSendOkServices
    {
        Task<bool> SendMailOk();
    }
}

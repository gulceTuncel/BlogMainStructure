namespace BlogMainStructure.Business.Services.MailServices
{
    public interface IMailService
    {
        Task SendMailAsync(string mail, string subject, string message);
    }
}

namespace AnimalShelter.Web.Infrastructure.EmailSender
{
    using System.Threading.Tasks;

    using AnimalShelter.Web.Infrastructure.EmailSender.EmailViewModels;

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailToUsAsync(EmailSendToUsModel viewEmailModel);
    }
}

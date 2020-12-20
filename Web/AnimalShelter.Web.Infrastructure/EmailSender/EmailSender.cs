namespace AnimalShelter.Web.Infrastructure.EmailSender
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using AnimalShelter.Common;
    using AnimalShelter.Web.Infrastructure.EmailSender.EmailViewModels;
    using Microsoft.Extensions.Options;

    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings smtpSettings;
        private readonly IViewRenderService viewRender;

        public EmailSender(IOptions<SmtpSettings> smtpSettings, IViewRenderService viewRender)
        {
            this.smtpSettings = smtpSettings.Value;
            this.viewRender = viewRender;
        }

        // for confirmation
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var viewEmailModel = new EmailConfirmationModel()
            {
                FromAddress = this.smtpSettings.FromAddress,
                ToAddress = email,
                Subject = subject,
                SingleDate = DateTime.UtcNow,
                Message = message,
            };

            string viewEmailPath = "~/Views/Contact/EmailView.cshtml";

            await this.SendMessage<EmailConfirmationModel>(email, subject, viewEmailModel, viewEmailPath);
        }

        // from user to website
        public async Task SendEmailToUsAsync(EmailSendToUsModel viewEmailModel)
        {
            string viewEmailPath = "~/Views/Contact/EmailSendToUsView.cshtml";
            string email = GlobalConstants.PersonalEmail;

            viewEmailModel.FromAddress = this.smtpSettings.FromAddress;
            viewEmailModel.ToAddress = GlobalConstants.PersonalEmail;

            await this.SendMessage<EmailSendToUsModel>(email, viewEmailModel.Subject, viewEmailModel, viewEmailPath);
        }

        private async Task SendMessage<T>(string email, string subject, T viewEmailModel, string viewEmailPath)
        {
            var fromAddress = new MailAddress(this.smtpSettings.FromAddress, GlobalConstants.SystemName);
            var toAddress = new MailAddress(email, email);

            var smtp = new SmtpClient
            {
                Host = this.smtpSettings.Server,
                Port = this.smtpSettings.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, this.smtpSettings.Password),
            };

            var messageHtml = await this.viewRender.RenderToStringAsync(viewEmailPath, viewEmailModel);

            using (var newMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = messageHtml,
            })
            {
                newMessage.IsBodyHtml = true;
                await smtp.SendMailAsync(newMessage);
            }
        }
    }
}

namespace AnimalShelter.Web.Infrastructure.EmailSender.EmailViewModels
{
    using System;

    public class EmailConfirmationModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime SingleDate { get; set; }
    }
}

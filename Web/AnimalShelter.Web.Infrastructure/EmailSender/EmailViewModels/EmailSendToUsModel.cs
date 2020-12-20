namespace AnimalShelter.Web.Infrastructure.EmailSender.EmailViewModels
{
    using System;

    public class EmailSendToUsModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string SenderEmail { get; set; }

        public string SenderUsername { get; set; }

        public string SenderPhoto { get; set; }

        public string SenderSex { get; set; }

        public string SenderLiving { get; set; }

        public string SenderAge { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime SingleDate { get; set; }
    }
}

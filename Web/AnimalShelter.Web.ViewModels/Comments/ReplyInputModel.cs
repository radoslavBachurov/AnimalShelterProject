namespace AnimalShelter.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class ReplyInputModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Коментара е задължително поле")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Коментара трябва да е от 2 до 1000 символа")]
        public string Text { get; set; }

        public int? ParentId { get; set; }

        public string AnswerTo { get; set; }
    }
}

using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class Answer : BaseDeletableModel<int>
    {
        public bool ReplyToComment { get; set; }

        public string AnswerFromNickname { get; set; }

        public string AnswerToId { get; set; }

        public ApplicationUser AnswerTo { get; set; }

        public string PostName { get; set; }

        public int PostId { get; set; }
    }
}

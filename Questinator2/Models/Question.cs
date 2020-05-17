using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questinator2.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int UserOwnerId { get; set; }
        public string QuestionMessage { get; set; }
        public string CreateDate { get; set; }
        public string AnswerDeadline { get; set; }
        public int MaxCustomQuestions { get; set; }
        public int MaxAnswers { get; set; }
        public bool IsSaved { get; set; }
        public bool IsActive { get; set; }
        public Answer[] Answers { get; set; }

    }
}

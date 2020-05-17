using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questinator2.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerMsg { get; set; }
        public int UserOwnerId { get; set; }
        public int QuestionId { get; set; }
    }
}

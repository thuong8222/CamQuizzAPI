using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalmQuizzModels
{
    internal class Answer
    {
        public string QuestionID { get; set; }
        public string AnswerID { get; set; }
        public string AnswerText { get; set; }
        public string IsCorrect { get; set; }
    }
}

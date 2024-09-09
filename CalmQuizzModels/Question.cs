using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalmQuizzModels
{
    public class Question
    {
        public string? QuestionId { get; set; }
        public string? TopicId { get; set; }
        public int? Score { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}

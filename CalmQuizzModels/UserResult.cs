using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalmQuizzModels
{
    public class UserResult
    {
        public string? UserResultId { set; get; }
        public string? UserId { set; get; }
        public DateTime? FinishTime { set; get; }
        public int? Score { set; get; }
        public string? TopicId { set; get; }
        public DateTime? CreateTime { set; get; }
        public string? ResultJson { set; get; }
    }

    public class UserResultUpdateModel: UserResult
    {
        public List<QuestionAnswer> ListAnswer { get; set; }
    }
}

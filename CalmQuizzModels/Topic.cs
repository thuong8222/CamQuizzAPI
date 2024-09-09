using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalmQuizzModels
{
    public class Topic
    {
       public string? TopicId { get; set; }
       public string? TopicName { get; set; }
       public int? ToltalQuestion { get; set; }
       public DateTime? CreateTime { get; set; }
       public int? TotalTime { get; set; }

    }
}

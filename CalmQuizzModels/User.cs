using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalmQuizzModels
{
    public class User
    {
        public string? UserId { get; set; }
        public string? Acount { get; set; }
        public string? Email { get; set; }

        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? TotalScore { get; set; }
        public int? RankId { get; set; }
        public string? Token { get; set; }
        
        public User ToList()
        {
            throw new NotImplementedException();
        }
    }
}

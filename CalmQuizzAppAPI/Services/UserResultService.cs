using Dapper;
using CalmQuizzModels;
namespace CalmQuizzAppAPI.Services
{
    public class UserResultService:BaseService
    {
        public UserResultService() : base() { }
        public void UserResultCreate(UserResult model)
        {
            string query = "INSERT INTO [dbo].[UserResult] ([UserResultId],[UserId],[FinishTime],[Score],[TopicId],[CreateTime],[ResultJson]) VALUES (@UserResultId,@UserId,@FinishTime,@Score,@TopicId,@CreateTime,@ResultJson)";
            _connection.Execute(query, model);
        }
    }
}

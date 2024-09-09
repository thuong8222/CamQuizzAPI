using Dapper;
using CalmQuizzModels;
namespace CalmQuizzAppAPI.Services
{
    public class QuestionService : BaseService
    {
        public QuestionService():base(){}
        public List<Question> GetListQuestion(string? topicId)
        {
            string query = "select * from [dbo].[Question] where TopicId=@topicId";
            return _connection.Query<Question>(query, new {topicId}).ToList();
        }
        public Question GetQuestionById(string questionId)
        {
            string query = "select * from [dbo].[Question] where QuestionId= @questionId";
            return _connection.QueryFirstOrDefault<Question>(query, new { questionId });
        }
        public void QuestionCreate(Question model)
        {
            string query = "INSERT INTO [dbo].[Question] ([QuestionId],[TopicId],[Score],[Image],[Description]) VALUES (@QuestionId,@TopicId,@Score,@Image,@Description)";
            _connection.Query<Question>(query, model);
        }
        public void QuestionUpdate(string id)
        {
            string query = "UPDATE [dbo].[Question] SET [QuestionId] = @QuestionId,[TopicId] = @TopicId,[Score] = @Score,[Image] = @Image,[Description] = @Description where QuestionId=@id";
            _connection.Query<Question>(query, new { id });
        
        }

    }
}

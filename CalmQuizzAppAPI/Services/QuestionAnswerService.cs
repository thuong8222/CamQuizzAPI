using Dapper;
using CalmQuizzModels;
using System.Reflection;

namespace CalmQuizzAppAPI.Services
{
    public class QuestionAnswerService : BaseService
    {
        public QuestionAnswerService() : base() { }
        public List<QuestionAnswer> GetListQuestionAnswer(string id, string? nameSearch)
        {
            string query = "SELECT * FROM [dbo].[Question_Answer] where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                query += " and [QuestionId] = @id ";
            }
            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and Description like @nameSearch ";
            }
            return _connection.Query<QuestionAnswer>(query, new { nameSearch, id }).ToList();
        }

        public List<QuestionAnswer> GetListQuestionAnswerByTopicId(string topicId)
        {
            string query = "SELECT qa.* FROM [CalmQuiz].[dbo].[Question_Answer] qa join [Question] q on  q.QuestionId= qa.QuestionId where q.TopicId= @topicId";
            return _connection.Query<QuestionAnswer>(query, new { topicId }).ToList();
        }

        public QuestionAnswer GetQuestionAnswerById(string id)
        {
            string query = "SELECT * FROM [dbo].[Question_Answer] where [QuestionAnswerId] = @id";
            return (_connection.QueryFirstOrDefault<QuestionAnswer>(query, new { id }));
        }
        public void QuestionAnswerCreate(QuestionAnswer model)
        {
            string query = "INSERT INTO [dbo].[Question_Answer] ([QuestionAnswerId],[QuestionId],[Description],[IsCorrect]) VALUES (@QuestionAnswerId,@QuestionId,@Description,@IsCorrect)";
            _connection.Query<QuestionAnswer>(query, model);
        }
        public QuestionAnswer CountQuestionAnswer()
        {
            string query = "SELECT [QuestionId], COUNT(*) AS num_answers FROM [dbo].[Question_Answer] GROUP BY [QuestionId];";
            return _connection.QueryFirstOrDefault<QuestionAnswer>(query);
        }
        public QuestionAnswer GetCorrectAnswer(string questionId)
        {
            string query = "SELECT * FROM [dbo].[Question_Answer] where [IsCorrect]=1 and [QuestionId]=@questionId";
            return _connection.QueryFirstOrDefault<QuestionAnswer>(query, new { questionId });


                }
        public void QuestionAnswerUpdate(QuestionAnswer model)
        {
            string query = "UPDATE [dbo].[Question_Answer] SET [QuestionAnswerId]=@QuestionAnswerId,[QuestionId]=@QuestionId,[Description]=@Description,[IsCorrect]=@IsCorrect where 1=1 ";
            if (!string.IsNullOrEmpty(model.QuestionAnswerId))
            {
                query += " and [QuestionAnswerId] = @QuestionAnswerId ";
            }
            _connection.Query<QuestionAnswer>(query, model);
        }
        public void QuestionAnswerDelete(string? id)
        {
            string query = "DELETE FROM [dbo].[Question_Answer] where 1=1 ";
            if (!string.IsNullOrEmpty(id))
            {
                query += "and [QuestionAnswerId] = @id";
            }
            _connection.Query<QuestionAnswer>(query, new { id });
        }
    }
}

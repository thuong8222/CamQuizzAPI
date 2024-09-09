using System.Data.Common;
using System.Threading.Tasks;
using CalmQuizzModels;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace CalmQuizzAppAPI.Services
{
    public class TopicService:BaseService
    {

        public TopicService() : base() { }
        public List<Topic> GetListTopic(string? nameSearch)
        {
            string query = "select * from [topic] where 1=1";
           
            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and TopicName like @nameSearch ";
            }
            return _connection.Query<Topic>(query, new { nameSearch }).ToList();
        }
        public Topic GetListTopicByName(string name)
        {
            string query = "select * from [topic] where TopicName = @name";
            return _connection.QueryFirstOrDefault<Topic>(query, new { name });
        }
        public Topic GetTopicById(string id)
        {
            string query = "select * from [topic] where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                query += "and TopicId = @id ";
            }
            return _connection.QueryFirstOrDefault<Topic>(query, new { id });
        }
        public void CreateTopic(Topic model)
        {
            string query = "INSERT INTO [dbo].[Topic] ([TopicId],[TopicName],[ToltalQuestion],[TotalTime],[CreateTime]) VALUES (@TopicId,@TopicName,@ToltalQuestion,@TotalTime,GetDate())";
            _connection.Execute(query, model);
        }
        public void UpdateTopic(Topic model)
        {
            string query = "UPDATE [dbo].[Topic] SET [TopicId]= @TopicId,TopicName]=@TopicName,ToltalQuestion]=@ToltalQuestion,CreateTime]=@CreateTime,TotalTime]=@TotalTime where 1=1"  ;
            if (!string.IsNullOrEmpty(model.TopicId))
            {
                
                query += " and [TopicId] = @TopicId ";
            }
            _connection.Execute(query, model);
        }
        public void UpdateToltalQuestionTopic(string topicId, int quantity)
        {
            string query = "UPDATE [dbo].[Topic] SET [ToltalQuestion] += @quantity WHERE [TopicId] = @topicId ";
            _connection.Execute(query, new { topicId, quantity });
        }
        public void DeleteTopic(string id)
        {
            string query = "DELETE FROM [dbo].[Topic] where [TopicId] = @id";
            _connection.Execute(query, new {id});
        }
    }
}

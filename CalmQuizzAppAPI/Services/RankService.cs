using Dapper;

using CalmQuizzModels;
namespace CalmQuizzAppAPI.Services

{
    public class RankService:BaseService
    {
        public RankService() : base() { }
        public List<Rank> GetListRank(string? nameSearch)
        {
            string query = "SELECT * FROM [dbo].[Rank] where 1-1";
            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and Name like @nameSearch ";
            }

            return _connection.Query<Rank>(query, new { nameSearch }).ToList();
        }
        public Rank GetListRankByName(string name)
        {
            string query = "SELECT * FROM [dbo].[Rank] where Name= @name";
            return _connection.QueryFirstOrDefault<Rank>(query, new { name });
        }
        public void RankCreate(Rank model)
        {
            string query = "INSERT INTO [dbo].[Rank] ([RankId],[ScoreNeed],[Name]) VALUES (@RankId, @ScoreNeed, @Name)";
            _connection.Query<Rank>(query, model);
        }
    }
}

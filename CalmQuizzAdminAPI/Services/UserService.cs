using System.Data;
using Dapper;

using CalmQuizzModels;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace CalmQuizzAppAPI.Services
{
    public class UserService :BaseService
    {
      
        public UserService():base(){ }

        public User GetUserByName(string acount)
        {
            string query = "select * from [user] where Acount=@acount";
            return _connection.QueryFirstOrDefault<User>(query, new { acount });
        }
        public User GetUserById(string id)
        {
            string query = "select * from [user] where UserId=@id";
            return _connection.QueryFirstOrDefault<User>(query, new { id });
        }
        public List<User> GetListUser(string? nameSearch)
        {
            string query = "SELECT * FROM [dbo].[User] where 1=1";

            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and Acount like @nameSearch ";
            }
            return _connection.Query<User>(query, new { nameSearch }).ToList();
        } 
        public void UserRegister(User model)
            {
                string query = "INSERT INTO [dbo].[User] ([UserId],[Acount],[Name],[Password],[TotalScore],[RankId],[Token]) VALUEs (@UserId,@Acount,@Name,@Password,@TotalScore,@RankId,@Token)";
                _connection.Execute(query, model);                                                                                                                               
            }
        public void UserUpdate( User model)
            {
                string query = "UPDATE [dbo].[User] SET [UserId]=@UserId,[Acount]=@Acount,[Name]=@Name,[Password]=@Password,[TotalScore]=@TotalScore,[RankId]=@RankId,[Token]=@Token";
                _connection.Execute(query, model);
            }
        public void UserDelete(string id)
        {
            string query = "DELETE FROM [dbo].[User] WHERE UserId=@id";
            _connection.Execute(query, new { id });
        }
    }

}

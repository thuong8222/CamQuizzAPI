using Microsoft.Data.SqlClient;
using System.Data;

namespace CalmQuizzAdminAPI.Services
{
    public class BaseService
    {
       static string connectionString = "Data Source=DESKTOP-DA74FGD;Initial Catalog=CalmQuiz;User ID=sa;Password=123123;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        //string connectionString = "Data Source=103.75.185.128;Initial Catalog=Vd;User ID=sa;Password=!Sway@2710#;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        protected IDbConnection _connection;

        public BaseService()
        {
            string constr = connectionString;
            this._connection = new SqlConnection(constr);
        }

        public BaseService(IDbConnection _connection)
        {
            if (_connection == null)
            {
                string constr = connectionString;
                this._connection = new SqlConnection(constr);
            }
            else
            {
                this._connection = _connection;
            }
        }
        public static IDbConnection Connect()
        {
            string constr = connectionString;
            return new SqlConnection(constr);
        }
    }
}

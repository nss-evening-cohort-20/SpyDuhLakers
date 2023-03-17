using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace SpyDuhLakers.Repositories
{
    public class BaseRepository
    {
        private string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NashIRL.Repositories
{
    public abstract class BaseRepository
    {
        private string _connectionString;
        public BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection Connection => new SqlConnection(_connectionString);
    }
}

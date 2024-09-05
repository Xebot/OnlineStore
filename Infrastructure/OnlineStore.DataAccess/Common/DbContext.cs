using Npgsql;

namespace OnlineStore.DataAccess.Common
{
    /// <summary>
    /// Контекст для работы с БД.
    /// </summary>
    public class DbContext : IDisposable
    {
        private readonly NpgsqlConnection _connection;

        public DbContext(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection Connection => _connection;

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}

using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     Handles our Database Connection.
    /// </summary>
    internal static class DbConnection
    {
        private static readonly string _connectionString =
            @"Data Source=localhost\sqlexpress;Initial Catalog=wheretowatch_db;Integrated Security=True";

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Gets the db connection.
        /// </summary>
        /// <returns>A SqlConnection.</returns>
        public static SqlConnection GetDbConnection()
        {
            var conn = new SqlConnection(_connectionString);
            return conn;
        }
    }
}
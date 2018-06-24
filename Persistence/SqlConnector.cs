using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class SqlConnector
    {
        private static readonly string DEFAULT_DATABASE_PATH = Path.Combine( Directory.GetCurrentDirectory(), "Resources", "YouToubeDownloader.db");

        public static IDbConnection GetDefaultConnection()
        {
            string defaultConnectionString = string.Format($"Data Source={DEFAULT_DATABASE_PATH};Version=3;");
            IDbConnection connection = null;
            try
            {
                connection = new SQLiteConnection(defaultConnectionString);
            }
            catch(SqlException ex)
            {

            }
            finally
            {

            }
            return connection;
        }
    }
}

using System.Data.SQLite;
using System.Data.SqlClient;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class SqlConnector
    {
        private static readonly ILog LOG = LogManager.GetLogger(typeof(SqlConnector));

        private static readonly string DEFAULT_DATABASE_PATH = Path.Combine( 
            Directory.GetCurrentDirectory(),
            "Resources", "YouToubeDownloader.db");

        private static SQLiteConnection _connection;

        private static readonly string defaultConnectionString = 
            string.Format($"Data Source={DEFAULT_DATABASE_PATH};");

        public static IDbConnection GetDefaultConnection()
        {
            
            if (_connection == null)
            {
                try
                {
                    if (!File.Exists(DEFAULT_DATABASE_PATH))
                        throw new Exception($"No file in directory {defaultConnectionString}");

                    _connection = new SQLiteConnection(SqlConnector.defaultConnectionString);
                }
                catch (Exception ex)
                {
                    LOG.Debug(ex.Message);
                    _connection.Close();
                }
                finally
                {
                    
                }
            }
            return _connection;
        }
    }
}

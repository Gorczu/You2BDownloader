using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Persistence.Respositories
{
    public class PlaylistRepository : IRepository<PlayList>
    {
        private IDbConnection _sqlConnection;

        public PlaylistRepository(IDbConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public bool DeleteItem(int idx)
        {
            int result = _sqlConnection.Execute($"DELETE FROM PlayList WHERE Id={idx}");
            return result != 0;
        }

        public bool EditItem(int idx, PlayList newValue)
        {
            int result = _sqlConnection.Execute(" UPDATE PlayList " +
                                                $"SET FolderPath={newValue.FolderPath}, " +
                                                $"SET Description={newValue.Description}, " +
                                                $"SET Gerne={newValue.Gerne} " +
                                                $"SET StartGeneration={newValue.StartGeneration} " +
                                                $"WHERE Id={newValue.Id} ");
            return result != 0;
        }

        public PlayList GetItem(int idx)
        {
            throw new NotImplementedException();
        }

        public IList<PlayList> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            throw new NotImplementedException();
        }

        public bool InsertItem(PlayList item)
        {
            throw new NotImplementedException();
        }
    }
}

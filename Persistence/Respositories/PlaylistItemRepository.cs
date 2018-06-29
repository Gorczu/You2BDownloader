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
    public class PlaylistItemRepository : IRepository<PlaylistItem>
    {
        private IDbConnection _sqlConnection;

        public PlaylistItemRepository(IDbConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public bool DeleteItem(int idx)
        {
            int result = _sqlConnection.Execute($"DELETE FROM PlaylistItem WHERE Id={idx}");
            return result != 0;
        }

        public bool EditItem(int idx, PlaylistItem newValue)
        {
            int result = _sqlConnection.Execute(" UPDATE PlaylistItem " +
                                                $"SET Address={newValue.Address}, " +
                                                $"SET Description={newValue.Description}, " +
                                                $"SET NewName={newValue.NewName} " +
                                                $"WHERE Id={newValue.Id} ");
            return result != 0; 
        }

        public PlaylistItem GetItem(int idx)
        {
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlaylistItem " +
                                                           $"WHERE Id={idx}");

            return result.FirstOrDefault() ?? null;
        }

        public IList<PlaylistItem> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlaylistItem " +
                                                           $"ORDER BY {orderParameterName} " +
                                                           $"LIMIT {noPerPage}, {pageNo * noPerPage}");

            return result.ToArray();
        }

        public bool InsertItem(PlaylistItem item)
        {
            int result = _sqlConnection.Execute($"INSERT INTO PlaylistItem(Address, Description, NewName) " +
                                                $"VALUE ({item.Address}, {item.Description}, {item.NewName})");
            return result != 0;
        }
    }
}

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
            int result = _sqlConnection.Execute($"DELETE FROM PlayListItem WHERE Id={idx}");
            return result != 0;
        }

        public bool EditItem(int idx, PlaylistItem newValue)
        {
            int result = _sqlConnection.Execute(" UPDATE PlayListItem " +
                                                $"SET Address='{newValue.Address}', " +
                                                $"SET Description='{newValue.Description}', " +
                                                $"SET NewName='{newValue.NewName}' " +
                                                $"SET Data='{newValue.Data}' " +
                                                $"WHERE Id={newValue.Id} ");
            return result != 0; 
        }

        public PlaylistItem GetItem(int idx)
        {
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlayListItem " +
                                                           $"WHERE Id={idx}");

            return result.FirstOrDefault() ?? null;
        }

        public IList<PlaylistItem> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlayListItem " +
                                                           $"ORDER BY {orderParameterName} " +
                                                           $"LIMIT {noPerPage} OFFSET {(pageNo - 1) * noPerPage}");

            return result.ToArray();
        }

        public IList<PlaylistItem> GetItemsWhere(string value, string columnName)
        {
            
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlayListItem " +
                                                           $"WHERE {columnName}={value}");

            return result.ToArray();
        }

        public bool InsertItem(PlaylistItem item)
        {
            int result = _sqlConnection.Execute($"INSERT INTO PlayListItem(Address, Description, NewName, Data, " +
                                                $"PlayListId) " +
                                                $"VALUES ('{item.Address}', '{item.Description}', '{item.NewName}', " +
                                                $"'{item.Data}', '{item.PlayListId}')");
            return result != 0;
        }
    }
}

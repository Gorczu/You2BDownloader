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
            int result = _sqlConnection.Execute($"DELETE FROM PlayList WHERE Id = {idx};");
            return result != 0;
        }

        public bool EditItem(int idx, PlayList newValue)
        {
            int result = _sqlConnection.Execute(" UPDATE PlayList " +
                                                $"SET FolderPath='{newValue.FolderPath}', " +
                                                $"SET Description='{newValue.Description}', " +
                                                $"SET Name='{newValue.Name}', " +
                                                $"SET Gerne='{newValue.Gerne}' " +
                                                $"SET StartGeneration='{newValue.StartGeneration}' " +
                                                $"SET Image='{newValue.Image}' " +
                                                $"WHERE Id = {newValue.Id}; ");
            return result != 0;
        }

        public PlayList GetItem(int idx)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayList " +
                                                       $"WHERE Id={idx}");

            return result.FirstOrDefault() ?? null;
        }

        public PlayList GetItemByName(string name)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayList " +
                                                       $"WHERE Name='{name}'");

            return result.FirstOrDefault() ?? null;
        }

        public IList<PlayList> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayList " +
                                                       $"ORDER BY {orderParameterName} " +
                                                       $"LIMIT {noPerPage} OFFSET {(pageNo - 1) * noPerPage}");

            return result.ToArray();
        }

        public IList<PlayList> GetItemsWhere(string value, string columnName)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayListItem " +
                                                       $"WHERE {columnName}={value}");

            return result.ToArray();
        }

        public bool InsertItem(PlayList item)
        {
            int result = _sqlConnection.Execute($"INSERT INTO PlayList(Description, FolderPath, Gerne, Name, Image) " +
                                                $"VALUES ('{item.Description}', '{item.FolderPath}', '{item.Gerne}', " +
                                                $" '{item.Name}', '{item.Image}')");
            return result != 0;
        }
    }
}

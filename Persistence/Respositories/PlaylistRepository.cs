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
    public class PlaylistRepository : IRepository<PlayList, int>
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
            int result = _sqlConnection.Execute(
                " UPDATE PlayList " +
                " SET FolderPath=@FolderPath, " +
                " SET Description=@Description, " +
                " SET Name=@Name, " +
                " SET Gerne=@Gerne " +
                " SET StartGeneration=@StartGeneration " +
                " SET Image=@Image " +
                " WHERE Id = @Id; ",
                new {
                    newValue.FolderPath,
                    newValue.Description,
                    newValue.Name,
                    newValue.Gerne,
                    newValue.StartGeneration,
                    newValue.Image,
                    newValue.Id
                });
            return result != 0;
        }

        public PlayList GetItem(int idx)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayList " +
                                                        "WHERE Id=@idx", idx);

            return result.FirstOrDefault() ?? null;
        }

        public PlayList GetItemByName(string name)
        {
            var result = _sqlConnection.Query<PlayList>("SELECT * FROM PlayList " +
                                                        "WHERE Name=@name", name);

            return result.FirstOrDefault() ?? null;
        }

        public IList<PlayList> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            var result = _sqlConnection.Query<PlayList>(
                "SELECT * FROM PlayList " +
                "ORDER BY @OrderParameterName " +
                "LIMIT @NoPerPage OFFSET @PageNo; ",
              new
              {
                  OrderParameterName = orderParameterName,
                  NoPerPage = noPerPage,
                  PageNo = (pageNo - 1) * noPerPage
              });

            return result.ToArray();
        }

        public IList<PlayList> GetItemsWhere(string value, string columnName)
        {
            var result = _sqlConnection.Query<PlayList>(
                "SELECT * FROM PlayListItem " +
                "WHERE @columnName=@value",
               new { columnName, value});

            return result.ToArray();
        }

        public int InsertItem(PlayList item)
        {
            int result = _sqlConnection.QuerySingle<int>(
            "INSERT INTO PlayList(Description, FolderPath, Gerne, Name, Image) " +
            "VALUES (@Description, @FolderPath, @Gerne, @Name, @Image); " +
            "SELECT last_insert_rowid(); ",
            new
            {
                item.Description,
                item.FolderPath,
                item.Gerne,
                item.Name,
                item.Image
            });
            return result;
        }
    }
}

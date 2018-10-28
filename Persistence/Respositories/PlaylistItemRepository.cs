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
    public class PlaylistItemRepository : IRepository<PlaylistItem, int>
    {
        private IDbConnection _sqlConnection;

        public PlaylistItemRepository(IDbConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public bool DeleteItem(int idx)
        {
            int result = _sqlConnection.Execute("DELETE FROM PlayListItem WHERE Id=@idx;", idx);
            return result != 0;
        }

        public bool EditItem(int idx, PlaylistItem newValue)
        {
            int result = _sqlConnection.Execute( 
                "UPDATE PlayListItem " +
                "SET Address=@Address, " +
                "SET Description=@Description, " +
                "SET NewName=@NewName " +
                "SET Data=@Data " +
                "WHERE Id=@idx; ",
                new {
                    newValue.Address,
                    newValue.Description,
                    newValue.NewName,
                    newValue.Data,
                    idx
                });
            return result != 0; 
        }

        public PlaylistItem GetItem(int idx)
        {
            var result = _sqlConnection.Query<PlaylistItem>("SELECT * FROM PlayListItem " +
                                                            "WHERE Id=@idx;", idx);

            return result.FirstOrDefault() ?? null;
        }

        public IList<PlaylistItem> GetItems(int pageNo, int noPerPage, string orderParameterName)
        {
            var result = _sqlConnection.Query<PlaylistItem>(
                "SELECT * FROM PlayListItem " +
                "ORDER BY @OrderParameterName " +
                "LIMIT @NoPerPage OFFSET @Offset; ",
                new
                {
                    OrderParameterName = orderParameterName,
                    NoPerPage = noPerPage,
                    Offset =(pageNo - 1) * noPerPage 
                });
                                                           
            return result.ToArray();
        }

        public IList<PlaylistItem> GetItemsFromPlayList(int playListId)
        {
            
            var result = _sqlConnection.Query<PlaylistItem>(
                "SELECT * FROM PlayListItem " +
                "WHERE PlayListId=@PlayListId;",
                new { PlayListId = playListId } );

            return result.ToArray();
        }

        public IList<PlaylistItem> GetItemsWhere(string value, string columnName)
        {
            throw new NotImplementedException();
        }

        public int InsertItem(PlaylistItem item)
        {
            return InsertItem(item, null);
        }

        public int InsertItem(PlaylistItem item, string defaultPathToFile)
        {
            int result = _sqlConnection.QuerySingle<int>(
                "INSERT INTO PlayListItem(Address, Description, NewName, Data, PlayListId) " +
                "VALUES (@Address, @Description, @NewName, @Data, @PlayListId); " +
                "SELECT last_insert_rowid(); ",
                 new
                 {
                     item.Address,
                     item.Description,
                     item.NewName,
                     item.Data,
                     item.PlayListId,
                 });

            return result;
        }
    }
}

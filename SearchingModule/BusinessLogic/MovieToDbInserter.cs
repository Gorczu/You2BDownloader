using Persistence;
using Persistence.Respositories;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.BusinessLogic
{
    public class MovieToDbInserter<T> : ITaskToDbInserter<T> where T: YoutubeItem
    {
        private static PlaylistItemRepository _playlistItemRepository
            = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());

        public bool InsertAllElements(T element, int playListId)
        {
            bool result = -1 != _playlistItemRepository.InsertItem(new Persistence.Models.PlaylistItem()
            {
                NewName = element.Name,
                Address = element.Source,
                PlayListId = playListId
            });
            return result;

        }
    }
}

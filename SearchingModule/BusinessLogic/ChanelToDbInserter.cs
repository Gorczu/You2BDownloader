using Persistence;
using Persistence.Respositories;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.BusinessLogic
{
    public class ChanelToDbInserter<T> : ITaskToDbInserter<T> where T : YoutubeItem
    {
        private static PlaylistItemRepository _playlistItemRepository 
            = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
     
        public bool InsertAllElements(T element,int playListId)
        {
            bool result = false;
            foreach (var item in element.GetAllElements())
            {
                _playlistItemRepository.InsertItem(new Persistence.Models.PlaylistItem()
                {
                    NewName = item.Name,
                    Address = item.Source,
                    PlayListId = playListId,

                });
            }
            return result;
        }
    }
}

using CommonControls.Helpers;
using Persistence;
using Persistence.Respositories;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchingModule.BusinessLogic
{
    public class PlaylistToDbInserter<T> : ITaskToDbInserter<T> where T : YoutubeItem
    {
        private static PlaylistItemRepository _playlistItemRepository
            = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
        
        public bool InsertAllElements(T element, int playListId)
        {
            bool result = false;
            ProgressHelper.SetProgress("Loading playlist info...", 0);
            var playListInfo = element.GetAllElements();
            int idx = 0;
            foreach (var item in playListInfo)
            {
                double percent =  ((double)++idx / (double)playListInfo.Count) * 100.0;
                ProgressHelper.SetProgress($"Saving playlist info {idx + 1} of {playListInfo.Count}", (int)percent); 
                int id = -1;
                id  = _playlistItemRepository.InsertItem(new Persistence.Models.PlaylistItem()
                {
                    NewName = item.Name,
                    Address = item.Id,
                    PlayListId = playListId,
                });

                result |= id != -1;
            }
            ProgressHelper.SetProgress("Finished.", 0);
            return result;
        }
    }
}

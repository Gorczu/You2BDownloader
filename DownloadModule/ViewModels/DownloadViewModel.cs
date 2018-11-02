using CommonControls.VM;
using Persistence;
using Persistence.Respositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadModule.ViewModels
{
    public class DownloadViewModel : BindableBase, INavigationAware
    {
        public DownloadViewModel()
        {
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            _playlistItemRepository = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
        }

        private ObservableCollection<PlaylistVM> playlists = new ObservableCollection<PlaylistVM>();
        private PlaylistRepository _playListPersistence;
        private PlaylistItemRepository _playlistItemRepository;

        public ObservableCollection<PlaylistVM> Playlists
        {
            get { return playlists; }
            set { SetProperty(ref playlists, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            LoadData();
            return true;
        }


        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadData();
        }

        private void LoadData()
        {
            //load from db
            foreach (var playList in _playListPersistence.GetItems(1, 20, "Name"))
            {
                IList<SingleItemViewModel> videos = new List<SingleItemViewModel>();
                foreach (var playListItem in _playlistItemRepository.GetItemsFromPlayList(playList.Id))
                {
                    videos.Add(new SingleItemViewModel()
                    {
                        NewName = playListItem.NewName,
                        Address = playListItem.Address,
                        Description= playListItem.Description
                    });
                }

                var playListVM = new PlaylistVM()
                {
                    Name = playList.Name,
                    Description = playList.Description,
                    FolderPath = playList.FolderPath,
                    Videos = videos
                };

                Playlists.Add(playListVM);
            }
        }

    }
}

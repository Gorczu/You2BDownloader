using SearchingModule.BusinessLogic;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonControls.VM;
using Prism.Regions;
using Persistence.Respositories;
using Persistence;

namespace SearchingModule.ViewModels
{
    public class SearchingViewModel : BindableBase, ISearchingViewModel, INavigationAware
    {
        public SearchingViewModel(IPlaylistCollector searchCommand)
        {
            this.SearchCommand = searchCommand;
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            _playlistItemRepository = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
        }

        private string _searchText;
        private IPlaylistCollector _searchCommand;
        private PlaylistRepository _playListPersistence;
        private PlaylistItemRepository _playlistItemRepository;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public IPlaylistCollector SearchCommand
        {
            get => _searchCommand;
            set => _searchCommand = value;
        }

        private IList<YoutubeItem> _result;
        public IList<YoutubeItem> Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);
            }
        }

        private List<ListItemViewModel> _userPlaylist;
        public List<ListItemViewModel> UserPlaylist
        {
            get => _userPlaylist;
            set => SetProperty(ref _userPlaylist , value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UserPlaylist = new List<ListItemViewModel>();
            foreach (var item in _playListPersistence.GetItems(1, 10, "Name"))
            {
                UserPlaylist.Add(new ListItemViewModel()
                {
                    Name = item.Name,
                    Created = item.StartGeneration,
                    Description = item.Description,
                    Gerne = item.Gerne,
                    Path = item.FolderPath
                });
            }

            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}

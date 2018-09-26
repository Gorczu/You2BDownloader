using CommonControls.VM;
using Persistence;
using Persistence.Respositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SearchModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPlaylistModule.ViewModels
{
    public class UserPlaylistViewModel : BindableBase, IUserPlaylistViewModel, INavigationAware
    {
        private ListItemViewModel _currenItem;
        private ObservableCollection<ListItemViewModel> _playlistCollection;
        private PlaylistRepository _playListPersistence;

        public UserPlaylistViewModel()
        {
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
        }

        public ListItemViewModel CurrenItem
        {
            get => _currenItem;
            set => SetProperty(ref _currenItem , value);
        }

        public ObservableCollection<ListItemViewModel> PlaylistCollection
        {
            get => _playlistCollection;
            set => SetProperty(ref _playlistCollection ,value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}

using CommonControls.VM;
using Microsoft.Practices.Unity;
using Persistence;
using Persistence.Respositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using UserPlaylistModule.Commands;
using UserPlaylistModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserPlaylistModule.ViewModels
{
    public class UserPlaylistViewModel : BindableBase, IUserPlaylistViewModel, INavigationAware
    {
        private ListItemViewModel _currenItem = new ListItemViewModel();
        private ObservableCollection<ListItemViewModel> _playlistCollection = 
            new ObservableCollection<ListItemViewModel>();
        private IAddItemCommand _addItemCommand;
        private PlaylistRepository _playListPersistence;
        private IRemovePlaylist _removePlaylist;

        public UserPlaylistViewModel(IPathSelector pathSelector)
        {

            this.PathSelector = pathSelector;
            this.PathSelector.SetPath( path => this._currenItem.Path = path);
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            AddItemCommand = new AddPlaylistCommand(this);
            RemovePlaylist = new RemovePlaylist(this);
        }

        public IPathSelector PathSelector
        {
            get;
            set;
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

        
        public IAddItemCommand AddItemCommand
        {
            get => _addItemCommand;
            set => _addItemCommand = value;
        }

        public IRemovePlaylist RemovePlaylist
        {
            get => _removePlaylist;
            set => _removePlaylist = value;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            GetListsFromRepository();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetListsFromRepository();
        }

        private void GetListsFromRepository()
        {
            _playlistCollection.Clear();
            foreach (var item in _playListPersistence.GetItems(1, 10, "Name"))
            {
                _playlistCollection.Add(new ListItemViewModel()
                {
                    Name = item.Name,
                    Created = item.StartGeneration,
                    Description = item.Description,
                    Gerne = item.Gerne,
                    Path = item.FolderPath
                });
            }
            RaisePropertyChanged("PlaylistCollection");
        }
    }
}

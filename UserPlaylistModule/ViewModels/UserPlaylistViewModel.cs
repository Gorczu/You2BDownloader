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
        private PlaylistItemRepository _playListItemPersistence;
        private IRemovePlaylist _removePlaylist;

        public UserPlaylistViewModel(IPathSelector pathSelector)
        {
            this.PathSelector = pathSelector;
            this.PathSelector.SetPath(path => this._currenItem.Path = path);
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            _playListItemPersistence = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
            GetListsFromRepository();
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
            set => SetProperty(ref _currenItem, value);
        }

        public ObservableCollection<ListItemViewModel> PlaylistCollection
        {
            get => _playlistCollection;
            set => SetProperty(ref _playlistCollection, value);
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

        private ObservableCollection<SingleItemViewModel> _selectedPlayListItemCollection = 
            new ObservableCollection<SingleItemViewModel>();

        public ObservableCollection<SingleItemViewModel> SelectedPlayListItemCollection
        {
            get => _selectedPlayListItemCollection;
            set => SetProperty(ref _selectedPlayListItemCollection, value);
        }

        private ListItemViewModel _selectedPlaylist;
        public ListItemViewModel SelectedPlaylist
        {
            get
            {
                if(_selectedPlaylist == null)
                {
                    if(PlaylistCollection.Count == 0)
                    {
                        GetListsFromRepository();
                    }
                    _selectedPlaylist = PlaylistCollection.FirstOrDefault() ?? null;
                }
                return _selectedPlaylist;
            }
            set => SetProperty(ref _selectedPlaylist, value);
        }

        public DelegateCommand SelectedPlaylistChanged
        {
            get
            {
                return new DelegateCommand(() => {
                    SelectedPlayListItemCollection.Clear();
                    foreach (var item in _playListItemPersistence.GetItemsWhere(_selectedPlaylist.Id.ToString(),
                        "PlayListId"))
                    {
                        SelectedPlayListItemCollection.Add(new SingleItemViewModel()
                        {
                            NewName = item.NewName,
                            Address = item.Address,
                            Description = item.Description,
                            PlayListId= item.PlayListId
                        });
                    }
                });
            }
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
                    Id = item.Id,
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

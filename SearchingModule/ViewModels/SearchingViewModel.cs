﻿using SearchingModule.BusinessLogic;
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
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Windows.Threading;
using System.Threading;

namespace SearchingModule.ViewModels
{
    public class SearchingViewModel : BindableBase, ISearchingViewModel, INavigationAware, IDbObjectInserter
    {
        public SearchingViewModel(IPlaylistCollector searchCommand)
        {
            this.SearchCommand = searchCommand;
            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
            _playListPersistence = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            _playlistItemRepository = new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
  
        }

        private string _searchText;
        private IPlaylistCollector _searchCommand;

        internal void UpdateCollection()
        {
            _dispatcher.Invoke(() => OnPropertyChanged("Result"));
        }

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

        private Dispatcher _dispatcher;
        private IList<YoutubeItem> _result = new List<YoutubeItem>();
        public IList<YoutubeItem> Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);
            }
        }

        private List<ListItemViewModel> _userPlaylist = new List<ListItemViewModel>();
        public List<ListItemViewModel> UserPlaylist
        {
            get => _userPlaylist;
            set => SetProperty(ref _userPlaylist , value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetListsFromRepository();
        }

        private void GetListsFromRepository()
        {
            UserPlaylist.Clear();
            foreach (var item in _playListPersistence.GetItems(1, 10, "Name"))
            {
                _userPlaylist.Add(new ListItemViewModel()
                {
                    Id= item.Id,
                    Name = item.Name,
                    Created = item.StartGeneration,
                    Description = item.Description,
                    Gerne = item.Gerne,
                    Path = item.FolderPath
                });
            }
            RaisePropertyChanged("UserPlaylist");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            GetListsFromRepository();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void InsertElement(object element)
        {
            throw new NotImplementedException();
        }

        public ICommand Drop
        {
            get
            {
                return new DelegateCommand<ListItemViewModel>(obj =>
                {
                    if(obj.ObjectToInsert!= null)
                    {
                       Task.Run(() =>
                       {
                           YoutubeItem youTubeItem = (YoutubeItem)obj.ObjectToInsert;
                           var inserter = youTubeItem.GetInserter();
                           bool inserted = inserter.InsertAllElements(youTubeItem, obj.Id);
                       });
                    }
                });
            }
        }
        
        
    }
}

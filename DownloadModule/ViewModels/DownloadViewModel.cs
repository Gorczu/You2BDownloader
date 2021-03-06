﻿using CommonControls.VM;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private string _musicSource;
        public string MusicSource
        {
            get => _musicSource;
            set => SetProperty(ref _musicSource, value);
        }

        public ICommand Play
        {
            get
            {
                return new DelegateCommand<SingleItemViewModel>(PlayExecute, CanPlay);
            }
        }

        private void PlayExecute(object obj)
        {
            if (obj != null)
            {
                MusicSource = ((SingleItemViewModel)((FrameworkElement)obj).DataContext).WholePath;
            }
        }

        private bool CanPlay(SingleItemViewModel obj)
        {
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
                var existing = Playlists.FirstOrDefault(o => o.Name == playList.Name);

                PlaylistVM playListVM = null;
                if (existing != null)
                {
                    playListVM = existing;
                }
                else
                {
                    playListVM = new PlaylistVM()
                    {
                        Name = playList.Name,
                        Description = playList.Description,
                        FolderPath = playList.FolderPath,
                        Videos = new ObservableCollection<SingleItemViewModel>(),
                    };
                    Playlists.Add(playListVM);
                }

                foreach (var playListItem in _playlistItemRepository.GetItemsFromPlayList(playList.Id))
                {
                    SingleItemViewModel existingMovieVM = playListVM.Videos.FirstOrDefault(e => e.NewName == playListItem.NewName);
                    if (existingMovieVM == null)
                    {
                        playListVM.Videos.Add(new SingleItemViewModel()
                        {
                            NewName = playListItem.NewName,
                            Address = playListItem.Address,
                            Description = playListItem.Description
                        });
                    }
                }
            }
        }
    }
}

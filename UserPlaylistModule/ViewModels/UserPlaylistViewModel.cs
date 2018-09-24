using Prism.Commands;
using Prism.Mvvm;
using SearchModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPlaylistModule.ViewModels
{
    public class UserPlaylistViewModel : BindableBase, IUserPlaylistViewModel
    {
        private ListItemViewModel _currenItem;
        private ObservableCollection<ListItemViewModel> _playlistCollection;

        public ListItemViewModel CurrenItem { get => _currenItem; set => SetProperty(ref _currenItem , value); }
        public ObservableCollection<ListItemViewModel> PlaylistCollection
        {
            get => _playlistCollection;
            set => SetProperty(ref _playlistCollection ,value);
        }

    }
}

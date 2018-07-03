using PlaylistModule.BusinessLogic;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistModule.ViewModels
{
    public class PlaylistViewModel : BindableBase
    {
        public PlaylistViewModel(IPlaylistCollector searchCommand)
        {
            this.SearchCommand = searchCommand;
        }

        private string _message;

        private IPlaylistCollector _searchCommand;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public IPlaylistCollector SearchCommand
        {
            get => _searchCommand;
            set => _searchCommand = value;
        }

        public PlaylistViewModel()
        {
            Message = "Playlist Prism Module";
        }
    }
}

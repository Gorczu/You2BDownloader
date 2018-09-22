using SearchingModule.BusinessLogic;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.ViewModels
{
    public class PlaylistViewModel : BindableBase, IPlaylistViewModel
    {
        public PlaylistViewModel(IPlaylistCollector searchCommand)
        {
            this.SearchCommand = searchCommand;
        }

        private string _searchText;

        private IPlaylistCollector _searchCommand;

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
    }
}

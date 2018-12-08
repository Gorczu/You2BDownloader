using CommonControls.VM;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace DownloadModule.ViewModels
{
    public class PlaylistVM : BindableBase
    {
        private string _name;
        private string _folderPath;
        private string _description;
        private byte[] _image;
        private ObservableCollection<SingleItemViewModel> _videos;
        private int _percent;

        public PlaylistVM()
        {
            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string FolderPath
        {
            get => _folderPath;
            set => SetProperty(ref _folderPath, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public int Percent
        {
            get => _percent;
            set => SetProperty(ref _percent, value);
        }

        public ObservableCollection<SingleItemViewModel> Videos
        {
            get => _videos;
            set => SetProperty(ref _videos, value);
        }

        private Dictionary<string, Task> _tasks = new Dictionary<string, Task>();

        private Dispatcher _dispatcher;

        public ICommand Download
        {
            get
            {
                return new DelegateCommand(DownloadExec, CanDownlaod);
            }
        }
        
        private bool CanDownlaod()
        {
            return true;
        }

        private int _selectedKindOfMusicIdx = 0;
        public int SelectedKindOfMusicIdx
        {
            get => _selectedKindOfMusicIdx;
            set => SetProperty(ref _selectedKindOfMusicIdx, value);
        }

        private bool _isSelectionEnabled = true;
        public bool IsSelectionEnabled
        {
            get => _isSelectionEnabled;
            set => SetProperty(ref _isSelectionEnabled, value);
        }

        public bool DownloadMusic
        {
            get { return _selectedKindOfMusicIdx == 0; }
        }
        
        private async void DownloadExec()
        {
            IsSelectionEnabled = false;
            foreach (var item in Videos)
            {
                Task task = null;
                if(!_tasks.TryGetValue(item.Address, out task))
                {
                    await item.Download(this._folderPath, DownloadMusic);
                    _tasks.Add(item.Address, task);
                }
            }
        }
    }
}

using CommonControls.VM;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        private IList<SingleItemViewModel> _videos;
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

        public IList<SingleItemViewModel> Videos
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

        private void DownloadExec()
        {
            foreach(var item in Videos)
            {
                Task task = null;
                if(!_tasks.TryGetValue(item.Address, out task))
                {
                    task =  item.Download(this._folderPath);
                    _tasks.Add(item.Address, task);
                }
            }
        }
    }
}

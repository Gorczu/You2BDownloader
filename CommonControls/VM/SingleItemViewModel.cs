using CommonControls.Download;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using YoutubeExplode;

namespace CommonControls.VM
{
    public class SingleItemViewModel : BindableBase
    {
        public SingleItemViewModel()
        {
            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
            Downloader = new Downloader();
        }

        private string _address;
        private string _newName;
        private string _description;
        private int _playListId;
        private byte[] _image;
        private int _percentDownloaded;
        private Dispatcher _dispatcher;

        
        public IDownloader Downloader { get; set; }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public int PlayListId
        {
            get => _playListId;
            set => SetProperty(ref _playListId, value);
        }

        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public int PercentDownloaded
        {
            get => _percentDownloaded;
            set => SetProperty(ref _percentDownloaded, value);
        }
        
        public string WholePath
        {
            get;
            set;
        }

        public async Task Download(string folderPath, bool music)
        {
            var pathWithoutExtension = Path.Combine(folderPath, NewName);
            Action<int> updateProgressCallback = p => _dispatcher.Invoke(() => PercentDownloaded = p);
            string newId = null;
            if(YoutubeClient.TryParseVideoId($"youtube.com/watch?v={Address}", out newId) && YoutubeClient.ValidateVideoId(newId))
            {
                WholePath = await Downloader.Download(Address, updateProgressCallback, pathWithoutExtension, music);
            }
        }
    }
}

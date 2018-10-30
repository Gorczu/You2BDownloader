using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControls.VM
{
    public class SingleItemViewModel : BindableBase
    {
        private string _address;
        private string _newName;
        private string _description;
        private int _playListId;
        private byte[] _image;
        private int _percentDownloaded;

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
    }
}

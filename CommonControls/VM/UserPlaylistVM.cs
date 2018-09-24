using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControls.VM
{
    public class UserPlaylistVM : BindableBase
    {
        private string _name;
        private byte[] _image;
        private string _gerne;
        private string _description;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public string Gerne
        {
            get => _gerne;
            set => SetProperty(ref _gerne ,value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}

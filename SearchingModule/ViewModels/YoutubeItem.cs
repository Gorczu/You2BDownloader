using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SearchingModule.ViewModels
{
    public abstract class YoutubeItem : BindableBase
    {
        private string _name;

        private string _source;

        private string _imgSrc;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name , value);
        }

        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }
        
        public string ImgSrc
        {
            get => _imgSrc;
            set => SetProperty(ref _imgSrc , value);
        }
    }
}

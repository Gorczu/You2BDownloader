using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using SearchingModule.BusinessLogic;

namespace SearchingModule.ViewModels
{
    public abstract class YoutubeItem : BindableBase
    {
        public abstract IList<YoutubeItem> GetAllElements();

        public abstract ITaskToDbInserter<YoutubeItem> GetInserter();

        public string Id { get; set; }

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

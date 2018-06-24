using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using System.Windows.Navigation;

namespace YouToubeDownloader.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "YouToube Downloader";
        IRegionManager _regions;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regions)
        {
            _regions = regions;
        }

        public ICommand ChangeSite
        {
            get
            {
                return new DelegateCommand<string>(a => _regions.RequestNavigate("ContentRegion", a));
            }
        }
    }
}

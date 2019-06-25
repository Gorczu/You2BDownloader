using CommonControls.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace YouToubeDownloader.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "YouToube Downloader";
        private Dispatcher _disp;
        IRegionManager _regions;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regions)
        {
            _disp = Dispatcher.FromThread(Thread.CurrentThread);
            _regions = regions;
            ProgressHelper.SetProgress += SetProgress;

        }

        public ICommand ChangeSite
        {
            get
            {
                return new DelegateCommand<string>(a => 
                _regions.RequestNavigate("ContentRegion", a));
            }
        }

        private int _progressValue = 0;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue , value );
        }

        private string _progressText = "Finished!";
        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText , value);
        }

        public void SetProgress(string text, int percent)
        {
            _disp.Invoke(() =>
            {
                this.ProgressText = text;
                this.ProgressValue = percent;
            });
        }
    }
}

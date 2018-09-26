using YouToubeDownloader.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using DownloadModule;
using Prism.Regions;
using Persistence;
using UserPlaylistModule;
using SearchingModule;
using log4net;
using log4net.Config;
using System.IO;

namespace YouToubeDownloader
{
    class Bootstrapper : UnityBootstrapper
    {
        // Logger instance named "MyApp".
        private static readonly ILog LOG = LogManager.GetLogger(typeof(Bootstrapper));

        private static readonly string LOG_4NET_CONFIG = 
            Path.Combine(
            Directory.GetCurrentDirectory(),
            "Config", "Log4Net.xml");

        protected override DependencyObject CreateShell()
        {
            if(!File.Exists(LOG_4NET_CONFIG))
            {
                throw new IOException($"No file in location {LOG_4NET_CONFIG}");
            }

            XmlConfigurator.Configure(new System.IO.FileInfo(LOG_4NET_CONFIG));
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(SearchingModule.SearchingModule));
            moduleCatalog.AddModule(typeof(UserPlaylistModule.UserPlaylistModule));
            moduleCatalog.AddModule(typeof(DownloadModule.DownloadModule));
            moduleCatalog.AddModule(typeof(CommonControls.CommonControlsModule));

            //---TEST FOR CONNECTION
            //var a = SqlConnector.GetDefaultConnection();
        }
    }
}

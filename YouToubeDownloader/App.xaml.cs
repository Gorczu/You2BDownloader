using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using log4net.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using YouToubeDownloader.Views;

namespace YouToubeDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        // Logger instance named "MyApp".
        private static readonly ILog LOG = LogManager.GetLogger(typeof(App));

        private static readonly string LOG_4NET_CONFIG =
            Path.Combine(
            Directory.GetCurrentDirectory(),
            "Config", "Log4Net.xml");

        public App()
        {
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            
            moduleCatalog.AddModule(typeof(SearchingModule.SearchingModule));
            moduleCatalog.AddModule(typeof(UserPlaylistModule.UserPlaylistModule));
            moduleCatalog.AddModule(typeof(DownloadModule.DownloadModule));
            moduleCatalog.AddModule(typeof(CommonControls.CommonControlsModule));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!File.Exists(LOG_4NET_CONFIG))
            {
                throw new IOException($"No file in location {LOG_4NET_CONFIG}");
            }

            XmlConfigurator.Configure(new System.IO.FileInfo(LOG_4NET_CONFIG));
        }
    }
}

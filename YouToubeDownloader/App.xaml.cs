using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using log4net;
using log4net.Config;
using Persistence;
using Persistence.Respositories;
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
        
        private static readonly string LOG_4NET_CONFIG =
            Path.Combine(
            Directory.GetCurrentDirectory(),
            "Config", "Log4Net.xml");

        private static readonly ILog LOG = LogManager.GetLogger(typeof(App));
        
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            var plr = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            var items = plr.GetItems(1, 10, "Name");

            moduleCatalog.AddModule<SearchingModule.SearchingModule>()
                         .AddModule<CommonControls.CommonControlsModule>()
                         .AddModule<UserPlaylistModule.UserPlaylistModule>()
                         .AddModule<DownloadModule.DownloadModule>();
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

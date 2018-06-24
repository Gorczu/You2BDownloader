using YouToubeDownloader.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using SearchModule;
using PlaylistModule;
using DownloadModule;
using Prism.Regions;
using Persistence;

namespace YouToubeDownloader
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(SearchModule.SearchModule));
            moduleCatalog.AddModule(typeof(PlaylistModule.PlaylistModule));
            moduleCatalog.AddModule(typeof(DownloadModule.DownloadModule));

            //---TEST FOR CONNECTION
            //var a = SqlConnector.GetDefaultConnection();
        }
    }
}

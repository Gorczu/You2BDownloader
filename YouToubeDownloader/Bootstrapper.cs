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
            moduleCatalog.AddModule(typeof(SearchingModule.SearchingModule));
            moduleCatalog.AddModule(typeof(UserPlaylistModule.UserPlaylistModule));
            moduleCatalog.AddModule(typeof(DownloadModule.DownloadModule));

            //---TEST FOR CONNECTION
            //var a = SqlConnector.GetDefaultConnection();
        }
    }
}

using UserPlaylistModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Prism.Unity;
using UserPlaylistModule.ViewModels;
using UserPlaylistModule.Commands;
using Prism.Ioc;

namespace UserPlaylistModule
{
    public class UserPlaylistModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", "PersonList");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserPlaylist>();
            containerRegistry.Register<IPathSelector, PathSelector>();

            
        }
    }
}
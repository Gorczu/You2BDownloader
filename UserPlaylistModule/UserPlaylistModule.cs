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
            regionManager.RequestNavigate("ContentRegion", "UserPlaylist");
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var instance = new PathSelector();
            containerRegistry.RegisterInstance(typeof(IPathSelector), instance);
            
            
            containerRegistry.RegisterForNavigation<UserPlaylist>();

            
        }
    }
}
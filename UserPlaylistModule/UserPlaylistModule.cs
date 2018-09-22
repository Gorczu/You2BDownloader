using UserPlaylistModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UserPlaylistModule
{
    public class UserPlaylistModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public UserPlaylistModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<UserPlaylist>();
            _regionManager.RequestNavigate("ContentRegion", "UserPlaylist");
        }
    }
}
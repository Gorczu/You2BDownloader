using PlaylistModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using PlaylistModule.BusinessLogic;
using PlaylistModule.ViewModels;

namespace PlaylistModule
{
    public class PlaylistModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public PlaylistModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IPlaylistCollector, PlaylistCollector>();
            _container.RegisterTypeForNavigation<Playlist>();
        }
    }
}
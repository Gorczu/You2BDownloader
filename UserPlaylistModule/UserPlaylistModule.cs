using UserPlaylistModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using SearchModule.Commands;
using UserPlaylistModule.ViewModels;
using SearchModule.ViewModels;

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
            //_container.RegisterType<IUserPlaylistViewModel, UserPlaylistViewModel>();
            //_container.RegisterType<IAddItemCommand, AddPlaylistCommand>();

            _regionManager.RequestNavigate("ContentRegion", "UserPlaylist");
        }
    }
}
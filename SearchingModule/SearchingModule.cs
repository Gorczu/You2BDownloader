using SearchingModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using SearchingModule.BusinessLogic;
using SearchingModule.ViewModels;

namespace SearchingModule
{
    public class SearchingModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public SearchingModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IPlaylistCollector, PlaylistCollector>();
            
            _container.RegisterTypeForNavigation<Searching>();
        }
    }
}
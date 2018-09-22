using SearchModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace SearchModule
{
    public class SearchModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public SearchModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<Search>();
            _regionManager.RequestNavigate("ContentRegion", "Search");
        }
    }
}
using CommonControls.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace CommonControls
{
    public class CommonControlsModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public CommonControlsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            //_container.RegisterTypeForNavigation<ViewA>();
        }
    }
}
using DownloadModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace DownloadModule
{
    public class DownloadModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public DownloadModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
           
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<Download>();
        }
    }
}
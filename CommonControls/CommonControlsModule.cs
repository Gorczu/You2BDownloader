using CommonControls.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Prism.Unity;
using CommonControls.Download;
using CommonControls.VM;
using Prism.Ioc;

namespace CommonControls
{
    public class CommonControlsModule : IModule
    {
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<SingleItemViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDownloader, Downloader>();
            
        }
    }
}
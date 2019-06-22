using DownloadModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Prism.Unity;
using Prism.Ioc;

namespace DownloadModule
{
    public class DownloadModule : IModule
    {
        

        public void Initialize()
        {
           
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Download>();
        }
    }
}
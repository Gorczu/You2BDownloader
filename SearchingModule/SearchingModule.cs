using SearchingModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Prism.Unity;
using SearchingModule.BusinessLogic;
using SearchingModule.ViewModels;
using Prism.Ioc;

namespace SearchingModule
{
    public class SearchingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPlaylistCollector, PlaylistCollector>();

            containerRegistry.RegisterForNavigation<Searching>();
        }
    }
}
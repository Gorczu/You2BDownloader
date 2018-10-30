using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadModule.ViewModels
{
    public class DownloadViewModel : BindableBase, INavigationAware
    {

        private IList<PlaylistVM> playlists;
        public IList<PlaylistVM> Playlists
        {
            get { return playlists; }
            set { SetProperty(ref playlists, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            LoadData();
            return true;
        }

        
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadData();
        }

        private void LoadData()
        {
            //from db
        }

    }
}

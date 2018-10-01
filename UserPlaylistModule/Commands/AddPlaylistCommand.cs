using CommonControls.VM;
using Microsoft.Practices.Unity;
using Persistence;
using Persistence.Models;
using Persistence.Respositories;
using UserPlaylistModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;

namespace UserPlaylistModule.Commands
{
    public class AddPlaylistCommand : IAddItemCommand
    {
        private IUserPlaylistViewModel _viewModel;
        private PlaylistRepository _listRepository;

        public IUserPlaylistViewModel ViewModel
        {
            get => _viewModel;
            set => _viewModel = value;
        }


        public AddPlaylistCommand(UserPlaylistViewModel userPlaylistViewModel)
        {
            
            ViewModel = userPlaylistViewModel;
            this._listRepository = new PlaylistRepository(SqlConnector.GetDefaultConnection());
            this._viewModel.CurrenItem.PropertyChanged += (a, b) => CanExecuteChanged(a, b);
        }
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool result = false;
            if(ViewModel.CurrenItem!= null)
            {
                result = !string.IsNullOrEmpty(ViewModel.CurrenItem.Name)
                      && !string.IsNullOrEmpty(ViewModel.CurrenItem.Path)
                      && Directory.Exists(ViewModel.CurrenItem.Path);
            }

            return result;
        }

        public void Execute(object parameter)
        {
            var persistenceItem = new PlayList()
            {
                Description = ViewModel.CurrenItem.Description,
                Gerne = ViewModel.CurrenItem.Gerne,
                FolderPath = ViewModel.CurrenItem.Path,
                Name = ViewModel.CurrenItem.Name,
                StartGeneration = DateTime.Now,
                Image = ViewModel.CurrenItem.Image
            };

            if(_listRepository.InsertItem(persistenceItem))
            {
                this.ViewModel.PlaylistCollection.Add(ViewModel.CurrenItem);
                this.ViewModel.CurrenItem = new ListItemViewModel();
            }
        }
    }
}

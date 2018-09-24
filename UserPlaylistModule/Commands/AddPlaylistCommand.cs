using Persistence;
using Persistence.Models;
using Persistence.Respositories;
using SearchModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SearchModule.Commands
{
    public class AddPlaylistCommand : ICommand
    {
        private IUserPlaylistViewModel _viewModel;
        private PlaylistRepository _listRepository;

        public AddPlaylistCommand(IUserPlaylistViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._listRepository = new PlaylistRepository(SqlConnector.GetDefaultConnection());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.CurrenItem.Name)
                && !string.IsNullOrEmpty(_viewModel.CurrenItem.Path);
        }

        public void Execute(object parameter)
        {
            var persistenceItem = new PlayList()
            {
                Description = _viewModel.CurrenItem.Description,
                Gerne = _viewModel.CurrenItem.Gerne,
                StartGeneration = DateTime.Now,
                FolderPath = _viewModel.CurrenItem.Path,
                Name = _viewModel.CurrenItem.Name
            };

            if(_listRepository.InsertItem(persistenceItem))
            {
                this._viewModel.PlaylistCollection.Add(_viewModel.CurrenItem);
                _viewModel.CurrenItem = new ListItemViewModel();
            }
        }
    }
}

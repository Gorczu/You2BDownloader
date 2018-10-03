using CommonControls.VM;
using Persistence;
using Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPlaylistModule.ViewModels;

namespace UserPlaylistModule.Commands
{
    public class RemovePlaylist : IRemovePlaylist
    {
        private IUserPlaylistViewModel _viewModel;
        private PlaylistRepository _listRepository;

        public RemovePlaylist(IUserPlaylistViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._listRepository = new PlaylistRepository(SqlConnector.GetDefaultConnection());
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var itemToRemove = (ListItemViewModel)parameter;
            if (_listRepository.DeleteItem(itemToRemove.Id))
            {
                this._viewModel.PlaylistCollection.Remove(itemToRemove);
            }
        }
    }
}

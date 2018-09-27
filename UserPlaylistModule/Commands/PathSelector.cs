using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserPlaylistModule.Commands
{
    public class PathSelector : IPathSelector
    {
        private Action<string> _setPathAction;
        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    _setPathAction(folderDialog.SelectedPath); 
                }
            }
        }

        public void SetPath(Action<string> setPathAction)
        {
            this._setPathAction = setPathAction;
        }
    }
}

using CommonControls.VM;
using Persistence;
using Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommonControls.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        private PlaylistItemRepository _playListItemPersistence = 
            new PlaylistItemRepository(SqlConnector.GetDefaultConnection());
        public UserListView()
        {
            InitializeComponent();
        }

        private void PlaylistItem_drop(object sender, DragEventArgs e)
        {
            if(e.Data!=null)
            {
                //YoutubeMovie typ tutaj wstrzyknac typy
                var item = (SingleItemViewModel)e.Data.GetData("DataContext");
                _playListItemPersistence.InsertItem(new Persistence.Models.PlaylistItem()
                {
                    Address = item.Address??null,
                    Description = item.Description??null,
                    NewName = item.NewName??null,
                    PlayListId = ((ListItemViewModel)this.DataContext).Id,
                });
            }
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }
    }
}

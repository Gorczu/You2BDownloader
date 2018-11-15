using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace UserPlaylistModule.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class UserPlaylist : UserControl
    {
        public UserPlaylist()
        {
            InitializeComponent();
        }

        private void listViewChanged(object sender, SizeChangedEventArgs e)
        {
            if(Math.Abs(e.NewSize.Width) > double.Epsilon  &&   Math.Abs(e.PreviousSize.Width) > double.Epsilon)
            {
                double change = e.NewSize.Width - e.PreviousSize.Width;
                DescCol.Width += (change / 2.0);
                NameCol.Width += (change / 2.0);
            }
        }

        
    }
}

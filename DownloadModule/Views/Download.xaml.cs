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

namespace DownloadModule.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class Download : UserControl
    {
        public Download()
        {
            InitializeComponent();
        }

        private void OnWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = sender as ScrollViewer;
            if(null != scv)
            {
                scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }
    }
}

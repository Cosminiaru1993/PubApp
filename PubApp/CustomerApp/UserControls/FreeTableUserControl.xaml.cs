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

namespace CustomerApp.UserControls
{
    /// <summary>
    /// Interaction logic for FreeTableUserControl.xaml
    /// </summary>
    public partial class FreeTableUserControl : UserControl
    {
        public FreeTableUserControl()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.Red;

            (App.Current.MainWindow as CustomerWindow).LoadMenu();
        }
    }
}

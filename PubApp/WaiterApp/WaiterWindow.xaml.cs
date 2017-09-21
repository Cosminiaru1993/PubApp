using PubDataLayer;
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
using System.Windows.Shapes;

namespace WaiterApp
{
    /// <summary>
    /// Interaction logic for WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        public WaiterWindow()
        {
            InitializeComponent();
        }

        private void btnTable1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = OrderRetriver.GetOrderFromTable(1);
                if (order != null)
                {
                    listboxProductsInOrder.ItemsSource = order.Product_Order;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has ocured:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

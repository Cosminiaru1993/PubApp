using PubDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
            listboxCategory.ItemsSource = CategoryRetriver.GetAllCategories();
            productsOrdered = new ObservableCollection<ProductWithQuantity>();
            this.DataContext = this;
        }

        public ObservableCollection<ProductWithQuantity> productsOrdered
        {
            get; set;

        }



        public double Sum
        {
            get { return (double)GetValue(SumProperty); }
            set { SetValue(SumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SumProperty =
            DependencyProperty.Register("Sum", typeof(double), typeof(MenuUserControl), new PropertyMetadata(0.0));


        private void listboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category selectedCategory = listboxCategory.SelectedItem as Category;
            if (selectedCategory != null)
            {
                // listboxProducts.ItemsSource = ProductRetriver.GetAllProductsByCategoryId(selectedCategory.id);
                listboxProducts.ItemsSource = ProductRetriver.GetAllProductsByCategoryId(selectedCategory.id);

            }
        }

        private void listboxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (listboxProducts.SelectedItem != null)
            {
                Product selectedProduct = listboxProducts.SelectedItem as Product;

                ProductWithQuantity productWithQuantity = productsOrdered.SingleOrDefault(p => p.id == selectedProduct.id);
                if (productWithQuantity == null)
                {
                    productWithQuantity = new ProductWithQuantity();
                    productWithQuantity.id = selectedProduct.id;
                    productWithQuantity.name = selectedProduct.name;
                    productWithQuantity.price = selectedProduct.price;
                    productWithQuantity.Quantity = 1;
                    productsOrdered.Add(productWithQuantity);
                    Sum = Sum + selectedProduct.price;
                }
                else
                {
                    productWithQuantity.Quantity = productWithQuantity.Quantity + 1;
                    Sum = Sum + selectedProduct.price;
                }
                listboxProducts.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = new Order();
                order.date = DateTime.Now;
                order.is_paid = false;
                order.table_number = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TableNumber"]);

                foreach (ProductWithQuantity p in productsOrdered)
                {
                    Product_Order producInOrder = new Product_Order();
                    producInOrder.price = p.price;
                    producInOrder.quantity = p.Quantity;
                    producInOrder.product_id = p.id;
                    order.Product_Order.Add(producInOrder);

                }

                if (OrderRetriver.AddOrder(order))
                {
                    MessageBox.Show("Order Added", "Order", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has ocured:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           (App.Current.MainWindow as CustomerWindow).LoadOrder();
        }
    }
}

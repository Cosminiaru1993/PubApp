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
using System.Windows.Shapes;


namespace CustomerApp
{

    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {

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
            DependencyProperty.Register("Sum", typeof(double), typeof(MenuWindow), new PropertyMetadata(0.0));



        public MenuWindow()
        {
            InitializeComponent();
            listboxCategory.ItemsSource = CategoryRetriver.GetAllCategories();
            productsOrdered = new ObservableCollection<ProductWithQuantity>();
            this.DataContext = this;

        }

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
                    productWithQuantity.Quantity= productWithQuantity.Quantity+1;
                    Sum = Sum + selectedProduct.price;
                }
                listboxProducts.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
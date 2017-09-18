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
        public ObservableCollection<Product> productsOrdered
        {
            get; set;

        }
        public MenuWindow()
        {
            InitializeComponent();
            listboxCategory.ItemsSource = CategoryRetriver.GetAllCategories();
            productsOrdered= new ObservableCollection<Product>();
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
                productsOrdered.Add((Product)listboxProducts.SelectedItem);
            }
            listboxProducts.SelectedItem = null;
            
        }
    }
}

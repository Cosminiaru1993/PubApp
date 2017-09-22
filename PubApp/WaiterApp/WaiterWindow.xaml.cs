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
    public class Table : DependencyObject
    {
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(Table));



        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(Table), new PropertyMetadata(Brushes.Green));

        public int OrderId { get; set; }

        public Table(int number)
        {
            this.Number = number;
        }

    }

    /// <summary>
    /// Interaction logic for WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        private List<Table> tables;

        public WaiterWindow()
        {
            InitializeComponent();
            GenerateTables();
            UpdateTablesState();
        }

        private void GenerateTables()
        {
            tables = new List<Table>();
            for (int i = 1; i <= 6; i++)
            {
                tables.Add(new Table(i));
            }

            listBoxTables.ItemsSource = tables;
        }

        private void UpdateTablesState()
        {
            foreach (Table table in tables)
            {
                table.BackgroundBrush = OrderRetriver.HasTableOrderInProgres(table.Number) ? Brushes.Red : Brushes.Green;
            }
        }


        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    Table table = btn.DataContext as Table;
                    if (table != null)
                    {
                        int tableNumber = table.Number;

                        listboxProductsInOrder.ItemsSource = null;
                        Order order = OrderRetriver.GetInProgresOrderFromTable(tableNumber);
                        if (order != null)
                        {
                            table.OrderId = order.id;
                            listboxProductsInOrder.ItemsSource = order.Product_Order;
                            textblockOrder.DataContext = table;
                            payBtn.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has ocured:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void payBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Table table = textblockOrder.DataContext as Table;
                if (table != null)
                {
                    OrderRetriver.MarkOrderIsPaid(table.OrderId);
                    UpdateTablesState();

                    listboxProductsInOrder.ItemsSource = null;
                    textblockOrder.DataContext = null; ;
                    payBtn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has ocured:" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
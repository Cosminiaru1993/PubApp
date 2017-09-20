using PubDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomerApp
{
    public class ProductWithQuantity : DependencyObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int category_id { get; set; }


        public int Quantity
        {
            get { return (int)GetValue(QuantityProperty); }
            set { SetValue(QuantityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Quantity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register("Quantity", typeof(int), typeof(ProductWithQuantity));


    }

}

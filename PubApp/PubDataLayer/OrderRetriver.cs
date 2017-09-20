using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubDataLayer
{
    public class OrderRetriver
    {
        public static bool AddOrder(Order ord)
        {
            using (PubAppEntities context = new PubAppEntities())
            {

                context.Orders.Add(ord);
                foreach (Product_Order productInOrder in ord.Product_Order)
                {
                    context.Product_Order.Add(productInOrder);
                }
                return context.SaveChanges() > 0;
            }
        }
    }
}

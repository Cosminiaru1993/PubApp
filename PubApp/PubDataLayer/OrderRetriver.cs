using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubDataLayer;
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

        public static Order GetInProgresOrderFromTable(int tableNo)
        {
            using (PubAppEntities context = new PubAppEntities())
            {
                return context.Orders.Include("Product_Order").Include("Product_Order.Product")
                    .OrderByDescending(order=> order.date)
                    .FirstOrDefault(order => order.table_number == tableNo && order.is_paid==false);
            }
        }

        public static bool HasTableOrderInProgres(int tableNo)
        {
            using (PubAppEntities context = new PubAppEntities())
            {
                Order inProgressOrder = context.Orders.OrderByDescending(order => order.date).FirstOrDefault(order => order.table_number == tableNo && order.is_paid == false);
                return (inProgressOrder != null);
            }
        }

        public static bool MarkOrderIsPaid(int orderId)
        {
            using (PubAppEntities context = new PubAppEntities())
            {
                Order order = context.Orders.SingleOrDefault(ord => ord.id == orderId);
                if (order != null)
                {
                    order.is_paid = true;
                }
                return context.SaveChanges()>0;
            }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubDataLayer
{
    public class ProductRetriver
    {
        public static IEnumerable<Product> GetAllProductsByCategoryId(int categoryId)
        {
            using (PubAppEntities context = new PubAppEntities())
            {
                return context.Products.Where(product => product.category_id == categoryId).ToList();
            }
            return new List<Product>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubDataLayer
{
    public class CategoryRetriver
    {
        public static IEnumerable<Category> GetAllCategories()
        {
            using (PubAppEntities context = new PubAppEntities())
            {
                return context.Categories.ToList();
            }
            return new List<Category>();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public static class CategoriesService
    {
        public static List<CategoryModel> GetCategories()
        {
            using (ToDoAppContext db = new ToDoAppContext(ConnectionString.path))
            {
                var categories = db.Categories.ToList();
                return categories;
            }
        }
    }
}

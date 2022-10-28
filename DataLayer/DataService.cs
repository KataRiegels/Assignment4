using Microsoft.EntityFrameworkCore;
using DataLayer.Model;

namespace DataLayer
{
    public class DataService
    {
        public IList<Category> GetCategories() 
        {
            using var db = new NorthwindContext();
            return db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            using var db = new NorthwindContext();
            return db.Categories.FirstOrDefault(x => x.Id == id);
 
        }

        public Category CreateCategory(string name, string description)
        {
            Category category = new Category();
            using var db = new NorthwindContext();
            var maxId = db.Categories.Max(x => x.Id);
            category.Id = maxId + 1;
            category.Name = name;
            category.Description = description;
            db.Categories.Add(category);
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var db = new NorthwindContext();
            var category = GetCategory(id);
            if (category == null)
            {
                return false;
            }
            db.Categories.Remove(GetCategory(id));

            return true;
        }
    }
}

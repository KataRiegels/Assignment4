using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category? GetCategory(int id);
        IList<Product> GetProducts();
        Product? GetProduct(int id);
        Category CreateCategory(Category category);
        bool UpdateCategory(int id, string name, string description);
        bool DeleteCategory(int id);

        IList<Product> GetProductByName(string search);
    }
}
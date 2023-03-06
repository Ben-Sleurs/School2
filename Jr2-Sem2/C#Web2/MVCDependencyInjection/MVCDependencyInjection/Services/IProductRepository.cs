using MVCDependencyInjection.Models;

namespace MVCDependencyInjection.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product this[string name] { get; }
        void Add(Product product);
        void Delete(Product product);
    }
}

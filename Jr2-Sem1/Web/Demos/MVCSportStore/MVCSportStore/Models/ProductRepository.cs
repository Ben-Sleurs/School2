using MVCSportStore.Data;
using MVCSportStore.Models.Data;

namespace MVCSportStore.Models
{
    public class ProductRepository
    {
        StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        private IEnumerable<Product> GetProducts(int productPage)
        {
            return _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSettings.ProductPagination)
                .Take(PageSettings.ProductPagination);
        }
        private IEnumerable<Product> GetProducts(int productPage,string category)
        {
            return _context.Products
                .Where(p=> p.Category == null || p.Category==category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSettings.ProductPagination)
                .Take(PageSettings.ProductPagination);
        }
        private PagingInfo GetPagingInfo(int productPage)
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = productPage;
            pagingInfo.ItemsPerPage = PageSettings.ProductPagination;
            pagingInfo.TotalItems = _context.Products.Count();
            return pagingInfo;
        }
        private PagingInfo GetPagingInfo(int productPage,string category)
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = productPage;
            pagingInfo.ItemsPerPage = PageSettings.ProductPagination;
            pagingInfo.TotalItems = (category == null)
                ? _context.Products.Count()
                : _context.Products.Where(e => e.Category == category).Count();
            return pagingInfo;
        }
        public ProductModel GetProductModel(int productPage)
        {
            var productModel = new ProductModel();
            productModel.Products = GetProducts(productPage);
            productModel.PagingInfo = GetPagingInfo(productPage);
            return productModel;
        }
        public ProductModel GetProductModel(int productPage,string category)
        {
            var productModel = new ProductModel();
            productModel.Products = GetProducts(productPage,category);
            productModel.PagingInfo = GetPagingInfo(productPage,category);
            return productModel;
        }
    }
}

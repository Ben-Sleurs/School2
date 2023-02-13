using MVCSportStore.Models.Data;

namespace MVCSportStore.Models
{
    public class ProductModel
    {
        public IEnumerable<Product>  Products { get; set; }

        public PagingInfo PagingInfo { get; set; }      
    }
}

using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts();
        Task<ProductModel> FindProductById(long id);
        Task<ProductModel> CreateProduct(ProductModel productVO);
        Task<ProductModel> UpdateProduct(ProductModel productVO);
        Task<bool> DeleteProductById(long id);
    }
}

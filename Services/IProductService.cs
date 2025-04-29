namespace AtomyWeb.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
    }
}

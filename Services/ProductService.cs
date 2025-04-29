using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AtomyWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _nav;

        public ProductService(HttpClient http, NavigationManager nav)
        {
            _http = http;
            _nav = nav;
        }

        public Task<IEnumerable<ProductModel>> GetProductsAsync() =>
            _http.GetFromJsonAsync<IEnumerable<ProductModel>>($"{_nav.BaseUri}data/products.json");
    }
}

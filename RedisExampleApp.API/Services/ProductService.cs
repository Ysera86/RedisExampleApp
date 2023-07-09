using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;

namespace RedisExampleApp.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);  
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return  await _productRepository.GetAllAsync();
        }

        public Task<Product> GetByIdAsync(int id)
        {
           var product = _productRepository.GetByIdAsync(id);

            // dto mapping

            return product; 
        }
    }
}

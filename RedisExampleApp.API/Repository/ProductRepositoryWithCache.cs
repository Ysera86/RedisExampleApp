using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RedisExampleApp.API.Models;
using RedisExampleApp.Cache;

namespace RedisExampleApp.API.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService; // adı redisRepo olabilirdi.

        public ProductRepositoryWithCache(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RedisExampleApp.API.Models;
using RedisExampleApp.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisExampleApp.API.Repository
{
    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService; // adı redisRepo olabilirdi.

        private readonly IDatabase _cacheRepository;

        private const string productKey = "productCache"; // hash key

        public ProductRepositoryWithCacheDecorator(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;

            _cacheRepository = _redisService.GetDb(1);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _productRepository.CreateAsync(product);

            if (await _cacheRepository.KeyExistsAsync(productKey))
            {
                await _cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(newProduct));
            }

            return newProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(productKey))
            {
                return await LoadToCacheFromDbAsync();
            }

            var products = new List<Product>();
            foreach (var item in (await _cacheRepository.HashGetAllAsync(productKey)).ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);

                products.Add(product);
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (await _cacheRepository.KeyExistsAsync(productKey))
            {
                var product = await _cacheRepository.HashGetAsync(productKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }

            var products = await LoadToCacheFromDbAsync();

            return products.FirstOrDefault(x => x.Id == id);
        }

        // dbden al redise cache
        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products = await _productRepository.GetAllAsync();
            products.ForEach(product =>
            {
                _cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(product));
            });

            return products;
        }
    }
}

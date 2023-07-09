using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Services
{
    public interface IProductService // normalde dto döneriz
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
    }
}

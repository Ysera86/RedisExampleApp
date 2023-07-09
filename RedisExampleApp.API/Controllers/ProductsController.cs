using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;
using RedisExampleApp.API.Services;
using RedisExampleApp.Cache;
using StackExchange.Redis;

namespace RedisExampleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        #region Sample 1

        //private readonly RedisService _redisService;

        //public ProductsController(IProductRepository repository, RedisService redisService)
        //{
        //    _repository = repository;

        //    _redisService = redisService;

        //    var db = _redisService.GetDb(0);
        //    db.StringSet("isim", "Ahmet");
        //    // db.StringSet("isim:1", "Ahmet");
        //} 

        #endregion

        #region Sample 2

        //private readonly IDatabase _database;

        //public ProductsController(IProductRepository repository, IDatabase database)
        //{
        //    _repository = repository;

        //    _database = database;
        //    _database.StringSet("soyisim", "Mehmet");
        //}

        #endregion

        // nihayetinde
        #region Controller artık repo değil servis bilecek

        //private readonly IProductRepository _repository;

        //public ProductsController(IProductRepository repository)
        //{
        //    _repository = repository;
        //} 
        #endregion

        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //return Ok(await _repository.GetAllAsync());
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //return Ok(await _repository.GetByIdAsync(id));
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            //return Created(string.Empty, await _repository.CreateAsync(product));
            return Created(string.Empty, await _productService.CreateAsync(product));
        }
    }
}

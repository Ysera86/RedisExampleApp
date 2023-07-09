using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;
using RedisExampleApp.Cache;
using StackExchange.Redis;

namespace RedisExampleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;


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

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;         
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Created(string.Empty, await _repository.CreateAsync(product));
        }
    }
}

using StackExchange.Redis;
using System;

namespace RedisExampleApp.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;

        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url); // "localhost:6379"
        }

        public IDatabase GetDb(int dbIndex)
        { 
            return _connectionMultiplexer.GetDatabase(dbIndex); 
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Application.Services
{
    public interface IRedisClient
    {
        Task<string> GetStringAsync(string key);
        Task SetStringAsync(string key, string value);
    }



    public class RedisClient : IRedisClient
    {
        private readonly StackExchange.Redis.IDatabase _database;

        public RedisClient(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task SetStringAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
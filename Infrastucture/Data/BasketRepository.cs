﻿using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketTd)
		{
			return await _database.KeyDeleteAsync(basketTd);
		}

		public async Task<CustomerBasket> GetBasketAsync(string id)
		{ 
			var data = await _database.StringGetAsync(id);
			return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
		}

		public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
		{
			var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
			if (!created) return null;
			else return await GetBasketAsync(basket.Id);
		}
	}
}

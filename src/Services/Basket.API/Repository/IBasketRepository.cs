﻿using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repository;

public interface IBasketRepository
{
	Task<Cart?> GetBasketByUsername(string username);
	Task<Cart?> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null);
	Task<bool> DeleteBasketFromUsername(string username);
}

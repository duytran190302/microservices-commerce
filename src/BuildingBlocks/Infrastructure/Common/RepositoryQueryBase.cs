﻿using Contracts.Domain.Interfaces;
using Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common;

public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
where T : EntityBase<K>
where TContext : DbContext
{
	private readonly TContext _context;

	public RepositoryQueryBase(TContext dbContext)
	{
		_context = dbContext ?? throw new ArgumentException(nameof(dbContext));
	}
	public IQueryable<T> FindAll(bool trackChanges = false)
	{
		return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
	}

	public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
	{
		var items = FindAll(trackChanges);

		items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
		return items;
	}
	//// var orders = await context.Orders
 //           .FindAll(includeProperties: 

	//			o => o.Product, // Include thông tin về sản phẩm
	//			o => o.Customer) // Include thông tin về khách hàng
 //           .ToListAsync();

	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
	{
		return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
	}

	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
	{
		var items = FindByCondition(expression, trackChanges);
		items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
		return items;
	}

	public async Task<T?> GetByIdAsync(K id)
	{
		return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
	}

	public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
	{
		return await FindByCondition(x => x.Id.Equals(id), false, includeProperties).FirstOrDefaultAsync();
	}
}

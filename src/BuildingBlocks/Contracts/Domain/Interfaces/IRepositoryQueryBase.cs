﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domain.Interfaces;

public interface IRepositoryQueryBase<T, K>
	where T : EntityBase<K>
{
	IQueryable<T> FindAll(bool trackChanges = false);
	IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
	IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
	IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
		params Expression<Func<T, object>>[] includeProperties);

	Task<T?> GetByIdAsync(K id);
	Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
}

public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryBase<T, K>
	where T : EntityBase<K>
{
	K Create(T entity);
	Task<K> CreateAsync(T entity);
	IList<K> CreateList(IEnumerable<T> entities);
	Task<IList<K>> CreateListAsync(IEnumerable<T> entities);
	void Update(T entity);
	Task UpdateAsync(T entity);
	void UpdateList(IEnumerable<T> entities);
	Task UpdateListAsync(IEnumerable<T> entities);
	void Delete(T entity);
	Task DeleteAsync(T entity);
	void DeleteList(IEnumerable<T> entities);
	Task DeleteListAsync(IEnumerable<T> entities);
	Task<int> SaveChangesAsync();
	Task<IDbContextTransaction> BeginTransactionAsync();
	Task EndTransactionAsync();
	Task RollbackTransactionAsync();
	Task SaveAsync();
	void Save();
}

public interface IRepositoryBaseAsync<T, K, TContext>
	: IRepositoryBaseAsync<T, K>
	where T : EntityBase<K>
	where TContext : DbContext
{

}

public interface IRepositoryQueryBase<T, K, TContext>
	: IRepositoryQueryBase<T, K>
	where T : EntityBase<K>
	where TContext : DbContext
{

}
﻿using Contracts.Domain.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repository;

public interface ICustomerRepository : IRepositoryQueryBase<Entities.Customer, int, CustomerContext>
{
	Task<IEnumerable<Entities.Customer>> GetCustomers();
	Task<Entities.Customer> GetCustomer(int id);
	// Task CreateCustomer(Entities.Customer customer);
	// Task UpdateCustomer(Entities.Customer customer);
	// Task DeleteCustomer(int id);
}
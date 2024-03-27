using Customer.API.Persistence;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repository;

public class CustomerRepository : RepositoryQueryBase<Entities.Customer, int, CustomerContext>
	, ICustomerRepository
{
	public CustomerRepository(CustomerContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Entities.Customer>> GetCustomers()
	{
		return await FindAll().ToListAsync();
	}

	public async Task<Entities.Customer> GetCustomer(int id)
	{
		return await GetByIdAsync(id);
	}


}

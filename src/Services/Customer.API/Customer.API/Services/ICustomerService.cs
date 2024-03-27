using Shared.Dtos.Customer;

namespace Customer.API.Services;

public interface ICustomerService
{
	Task<IEnumerable<CustomerDto>> GetCustomers();
	Task<CustomerDto> GetCustomer(int id);

}

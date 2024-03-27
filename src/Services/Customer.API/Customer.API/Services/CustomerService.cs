using AutoMapper;
using Customer.API.Repository;
using Shared.Dtos.Customer;

namespace Customer.API.Services;

public class CustomerService : ICustomerService
{
	private readonly ICustomerRepository _repository;
	private readonly IMapper _mapper;
	public CustomerService(ICustomerRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<CustomerDto>> GetCustomers()
	{
		return _mapper.Map<IEnumerable<CustomerDto>>(await _repository.GetCustomers());
	}

	public async Task<CustomerDto> GetCustomer(int id)
	{
		return _mapper.Map<CustomerDto>(await _repository.GetCustomer(id));
	}
}

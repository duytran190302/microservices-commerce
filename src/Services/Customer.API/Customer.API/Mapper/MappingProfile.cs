using AutoMapper;
using Infrastructure.Mapping;
using Shared.Dtos.Customer;

namespace Customer.API.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Entities.Customer, CustomerDto>();
		CreateMap<CreateCustomerDto, Entities.Customer>();
		CreateMap<UpdateCustomerDto, Entities.Customer>().IgnoreAllNonExisting();
	}
}

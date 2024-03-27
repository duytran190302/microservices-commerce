using AutoMapper;
using Contracts.Common.Interfaces;
using Contracts.Domain.Interfaces;
using Customer.API.Mapper;
using Customer.API.Persistence;
using Customer.API.Repository;
using Customer.API.Services;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Extensions;

public static class ServiceExtension
{
	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnectionString");
		services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new MappingProfile());
		});

		var mapper = config.CreateMapper();
		services.AddSingleton(mapper);
		services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		services.AddScoped(typeof(IRepositoryQueryBase<,,>), typeof(RepositoryQueryBase<,,>));
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<ICustomerService, CustomerService>();

		//services.ConfigureHealthChecks(connectionString);
		return services;
	}
}

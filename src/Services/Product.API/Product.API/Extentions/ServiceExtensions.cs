﻿using Contracts.Common.Interfaces;
using Contracts.Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;
using Product.API.Repository;
using System.Configuration;

namespace Product.API.Extentions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			services.Configure<RouteOptions>(options=> options.LowercaseUrls = true);
			services.AddEndpointsApiExplorer();
			services.ConfigureProductDbContext(configuration);
			services.AddInfrastructureService();
			services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));
			services.AddSwaggerGen();
			return services;
		}

		private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnectionString");
			var builder = new MySqlConnectionStringBuilder(connectionString);

			services.AddDbContext<ProductContext>(m => m.UseMySql(builder.ConnectionString,
				ServerVersion.AutoDetect(builder.ConnectionString), e =>
				{
					e.MigrationsAssembly("Product.API");
					e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				}));

			return services;
		}
		private static IServiceCollection AddInfrastructureService(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBase<,,>));
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped<IProductRepository, ProductRepository>();

			return services;

		}

	}
}

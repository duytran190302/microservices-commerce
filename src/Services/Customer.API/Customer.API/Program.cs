using Customer.API.Extensions;
using Customer.API.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace Customer.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();

			var builder = WebApplication.CreateBuilder(args);
			Log.Information($"Start {builder.Environment.ApplicationName} Api up");
			try
			{
				builder.Services.AddControllers();
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();
				builder.Host.AddAppConfigurations();

				builder.Services.ConfigureServices(builder.Configuration);

				var app = builder.Build();

				if (app.Environment.IsDevelopment())
				{
					app.UseSwagger();
					app.UseSwaggerUI();
				}

				app.UseRouting();
				//app.UseHttpsRedirection();

				app.UseAuthorization();
				app.UseEndpoints(endpoints =>
				{
					//endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
					//{
					//	Predicate = _ => true,
					//	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
					//});
					endpoints.MapDefaultControllerRoute();
				});

				app.SeedCustomer();
				app.MapControllers();
				app.Run();
			}
			catch (Exception ex)
			{
				string type = ex.GetType().Name;
				if (type.Equals("StopTheHostException", StringComparison.Ordinal))
					throw;
				Log.Fatal(ex, "Unhanded exception");
			}
			finally
			{
				Log.Information("Shut down Customer API complete");
				Log.CloseAndFlush();
			}
		}
	}
}

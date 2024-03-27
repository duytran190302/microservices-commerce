using Common.Logging;
using Product.API.Extentions;
using Product.API.Persistence;
using Serilog;

namespace Product.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
			Log.Information("Starting Product API up");

			try
			{
				var builder = WebApplication.CreateBuilder(args);

				builder.Host.UseSerilog(Serilogger.Configure);
				builder.Host.AddAppConfigurations();

				// Add services to the container.
				builder.Services.AddInfrastructure(builder.Configuration);


				//builder.Services.AddControllers();
				//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
				//builder.Services.AddEndpointsApiExplorer();
				//builder.Services.AddSwaggerGen();

				var app = builder.Build();

				// Configure the HTTP request pipeline.
				app.UseInfrastructure();
				//if (app.Environment.IsDevelopment())
				//{
				//	app.UseSwagger();
				//	app.UseSwaggerUI();
				//}

				//app.UseHttpsRedirection();

				//app.UseAuthorization();

				//app.MapControllers();
				app.MigrateDatabase<ProductContext>((context, _) =>
				{
					ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
				})
					.Run();
				Log.Information($"End build Pipeline in Program file");
			}
			catch (Exception ex)
			{
				Log.Information($"Error Product API :{ex.Message}");
				string type = ex.GetType().Name;
				if (type.Equals("StopTheHostException", StringComparison.Ordinal))
					throw;
				Log.Fatal(ex, "Unhanded exception");
			}
			finally
			{
				Log.Information("Shut down Product API complete");
				Log.CloseAndFlush();
			}
		}
	}
}

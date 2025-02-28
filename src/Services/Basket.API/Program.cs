using Common.Logging;
using Serilog;
namespace Basket.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
			Log.Information("Starting Basket API up");

			try
			{
				var builder = WebApplication.CreateBuilder(args);

				builder.Host.UseSerilog(Serilogger.Configure);
				// Add services to the container.

				builder.Services.AddControllers();
				// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();

				var app = builder.Build();

				// Configure the HTTP request pipeline.
				if (app.Environment.IsDevelopment())
				{
					app.UseSwagger();
					app.UseSwaggerUI();
				}

				app.UseHttpsRedirection();

				app.UseAuthorization();

				app.MapControllers();

				app.Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Unhandlerd exception");
			}
			finally
			{
				Log.Information("Shut down Product API complete");
				Log.CloseAndFlush();
			}
		}
	}
}

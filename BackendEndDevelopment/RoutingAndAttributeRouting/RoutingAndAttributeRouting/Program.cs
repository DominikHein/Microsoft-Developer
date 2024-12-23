using RoutingAndAttributeRouting.Models;

namespace RoutingAndAttributeRouting
{
	public class Program
	{

		public static void Main(string[] args)
		{

			var items = new List<Item>();

			var builder = WebApplication.CreateBuilder(args);

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

			app.MapGet("/", () => "Hello World!");

			app.MapGet("/items", () => items);

			app.MapGet("/items/{id}", (int id) =>
			{
				var item = items.FirstOrDefault(x => x.Id == id);
				if (item == null)
				{
					return Results.NotFound();
				}
				return Results.Ok(item);
			});

			app.MapPost("/items/post", (Item item) =>
			{
				items.Add(item);
				return Results.Created($"/items/{item.Id}", item);
			});

			app.MapDelete("/items/{id}", (int id) =>
			{
				var item = items.FirstOrDefault(x => x.Id == id);
				if (item == null)
				{
					return Results.NotFound();
				}
				items.Remove(item);
				return Results.NoContent();
			});

			app.Run();
		}
	}
}

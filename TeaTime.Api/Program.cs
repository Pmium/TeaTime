using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.Repositories;
using TeaTime.Api.Middlewares;
using TeaTime.Api.Services;

namespace TeaTime.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<InMemoryContext>(options =>
            {
                options.UseInMemoryDatabase("TeaTimeDb");
            });

            builder.Services.AddSingleton<OracleDbContext>();
            builder.Services.AddScoped<IStoresService, StoresService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IStoresRepository, InMemoryStoresRepository>();
                builder.Services.AddScoped<IOrdersRepository, InMemoryOrdersRepository>();
            }
            else
            {
                builder.Services.AddScoped<IStoresRepository, OracleStoresRepository>();
                builder.Services.AddScoped<IOrdersRepository, OracleOrdersRepository>();
            }

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

            app.UseMiddleware<ApiAuthMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}

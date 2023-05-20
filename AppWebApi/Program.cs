using Domain.Repositoy;
using Infrastructure.DataBase;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Security.Agregate;
using Domain.InterfacesServices;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace AppWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AssetContext>(
                options => options.UseInMemoryDatabase("DbAssets"));

            builder.Services.AddScoped<DbDataFake>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<IAssetService, AssetServices>();

            //Mapper
            var mapConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddControllers();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hahn Softwareentwicklung",
                    Description = "App demo.",
                    Contact = new OpenApiContact
                    {
                        Name = "Cristian Fiochetti",
                        Url = new Uri("https://github.com/KaiserMeca")
                    },
                    Version = "v1"
                });
            });

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbDataFake = scope.ServiceProvider.GetService<DbDataFake>()!;
                dbDataFake.SeedData();
            }

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
    }
}
using Domain.Repositoy;
using Domain.UnitOfWork;
using Infrastructure.DataBase;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Security.Agregate;

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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAssetRepository, AssetRepository>();

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
    }
}
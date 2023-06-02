using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.InterfacesServices;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Shared.DomainEvent;
using Shared.DomainEvent.Handler;
using Domain.Assets.Models;
using Domain.Assets.ValueObjectModels;
using Domain.Assets.Aggregates.Events;
using Infrastructure.EventHandlers;
using System.Reflection;

namespace AppWebApi
{
    /// <summary>
    /// The entry point class for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that starts the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
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
            builder.Services.AddScoped<IDomainEventBus, DomainEventBus>();
            builder.Services.AddScoped<IMailSendOkServices, MailSendOkServices>();
            builder.Services.AddScoped<IDomainEventHandlerFactory, DomainEventHandlerFactory>();
            builder.Services.AddScoped<NotificationEmailAddedAssetEventHandler>();
            builder.Services.AddScoped<IDomainEventHandler<NotifyAssetAdded>, NotificationEmailAddedAssetEventHandler>();

            //Mapper
            var mapConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddLogging(config =>
            {
                config.AddConsole();
            });

            builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

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
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddSwaggerGen();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.Configure<EmailConfiguration>(configuration.GetSection("Email"));

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

            app.UseMiddleware<MiddlewareExtension>();

            app.UseCors("AllowWebApp");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

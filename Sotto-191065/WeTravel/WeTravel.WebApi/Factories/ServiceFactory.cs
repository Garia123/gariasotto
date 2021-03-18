using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.IO;
using System;
using WeTravel.Filter;
using WeTravel.Domain;
using WeTravel.Model;
using WeTravel.ServiceInterface;
using WeTravel.DataAccess;
using WeTravel.DataAccessInterface;
using WeTravel.Service;

namespace WeTravel.WebApi
{
    [ExcludeFromCodeCoverageAttribute]
    public class ServiceFactory
    {
        private readonly IServiceCollection _services;

        public ServiceFactory(IServiceCollection services)
        {
            _services = services;
        }

        public void AddAllServices()
        {
            AddControllers();
            AddDbContextService();
            AddRepositories();
            AddUnitOfWork();
            AddServices();
            AddRepositoryFilter();
            AddFilters();
            AddSwagger();
            AddCors();
            AddAlgorithms();
        }

        private void AddControllers()
        {
            _services.AddControllers(options => options.Filters.Add(typeof(WeTravelExceptionFilter)));
        }

        private void AddDbContextService()
        {
            _services.AddDbContext<DbContext, WeTravelDbContext>();
        }

        private void AddUnitOfWork()
        {
            _services.AddScoped<IUnitOfWork, DataAccess.UnitOfWork>();
        }

        private void AddRepositories()
        {
            _services.AddScoped<ICategoryRepository, CategoryRepository>();
            _services.AddScoped<ILodgingRepository, LodgingRepository>();
            _services.AddScoped<IRegionRepository, RegionRepository>();
            _services.AddScoped<IReserveRepository, ReserveRepository>();
            _services.AddScoped<ITouristLocationRepository, TouristLocationRepository>();
            _services.AddScoped<IUserRepository, UserRepository>();
            _services.AddScoped<ISessionRepository, SessionRepository>();
            _services.AddScoped<IReviewRepository, ReviewRepository>();
        }

        private void AddAlgorithms()
        {
            _services.AddScoped<ReservePrice, ReservePriceCalculation>();
        }

        private void AddServices()
        {
            _services.AddScoped<ICategoryService, CategoryService>();
            _services.AddScoped<ILodgingService, LodgingService>();
            _services.AddScoped<IRegionService, RegionService>();
            _services.AddScoped<IReserveService, ReserveService>();
            _services.AddScoped<ITouristLocationService, TouristLocationService>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<ISessionService, SessionService>();
            _services.AddScoped<IReviewService, ReviewService>();
        }

        private void AddRepositoryFilter()
        {
            _services.AddScoped<IFilterRepository<Domain.Lodging, LodgingModelFilter>, LodgingFilter>();
            _services.AddScoped<IFilterRepository<Domain.TouristLocation, TouristLocationModelFilter>, TouristLocationFilter>();
        }

        private void AddFilters()
        {
            _services.AddScoped<WeTravelAuthFilter>();
        }

        private void AddSwagger()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "WeTravel Swagger",
                    Description = "Swagger documentation for WeTravel API",
                });
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void AddCors()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
    }
}

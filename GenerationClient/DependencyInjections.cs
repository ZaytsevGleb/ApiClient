using GenerationClient.Core;
using GenerationClient.Core.Interfaces;
using GenerationClient.Core.Stratagies;
using GenerationClient.DataAccesss;
using GenerationClient.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace GenerationClient
{
    public static class DependencyInjections
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<Application>();

            // DataAccess
            services.AddSingleton<IApplicationDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                return new ApplicationDbContext(options.Options);
            });

            // Persentation
            services.AddSingleton<IUserInterfaceService, ConsoleUserInterfaceService>();

            // Infrastructure
            services.AddSingleton<IProductsApiClient>(provider =>
            {
                var httpClient = new HttpClient();
                var baseUrl = configuration["WeightControlServiceUrl"];

                return new ProductsApiClient(baseUrl, httpClient);
            });

            // Core
            services.AddSingleton<IStrategy, ShowExistingProductsStrategy>();
            services.AddSingleton<IStrategy, TransferProductsStrategy>();

            return services;
        }
    }
}

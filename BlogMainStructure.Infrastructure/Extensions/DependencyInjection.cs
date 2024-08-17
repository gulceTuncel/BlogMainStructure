using BlogMainStructure.Infrastructure.AppContext;
using BlogMainStructure.Infrastructure.Repositories.ArticleRepositories;
using BlogMainStructure.Infrastructure.Repositories.TagRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlogMainStructure.Infrastructure.Repositories.AuthorRepositories;


namespace BlogMainStructure.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        // Configures and registers infrastructure services in the application's dependency injection container.
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registers the AppDbContext with dependency injection.
            // Configures Entity Framework Core to use lazy loading proxies and SQL Server with the provided connection string.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
            });

            // Registers the IAuthorRepository and its implementation AuthorRepository with scoped lifetime.
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            // Registers IHttpContextAccessor to access HttpContext in services.
            services.AddHttpContextAccessor();

            return services;
        }
    }
}

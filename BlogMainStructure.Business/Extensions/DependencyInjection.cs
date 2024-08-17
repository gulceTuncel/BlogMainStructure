using BlogMainStructure.Business.Services.MailServices;
using Microsoft.Extensions.DependencyInjection;
using BlogMainStructure.Business.Services.AuthorServices;
using BlogMainStructure.Business.Services.TagServices;
using BlogMainStructure.Business.Services.ArticleServices;
using Mapster;
using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Business.DTOs.ArticleDTOs;

namespace BlogMainStructure.Business.Extensions
{
    // This static class provides an extension method for IServiceCollection to register business services.
    public static class DependencyInjection
    {
        // This method adds business services to the service collection for dependency injection.
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register the mail service for dependency injection, scoped to the lifetime of the request.
            services.AddScoped<IMailService, MailService>();

            // Register the author service for dependency injection, scoped to the lifetime of the request.
            services.AddScoped<IAuthorService, AuthorService>();

            // Register the tag service for dependency injection, scoped to the lifetime of the request.
            services.AddScoped<ITagService, TagService>();

            // Register the article service for dependency injection, scoped to the lifetime of the request.
            services.AddScoped<IArticleService, ArticleService>();

            // Return the modified service collection to support method chaining.

            ConfigureMapster();

            return services;
        }

        private static void ConfigureMapster()
        {
            TypeAdapterConfig<Article, ArticleDTO>.NewConfig().Map(dest => dest.AuthorName, src => src.Author.FirstName + " " + src.Author.LastName);
        }

    }
}

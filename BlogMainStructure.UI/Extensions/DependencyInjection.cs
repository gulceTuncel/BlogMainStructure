using AspNetCoreHero.ToastNotification;
using BlogMainStructure.Infrastructure.AppContext;
using Microsoft.AspNetCore.Identity;

namespace BlogMainStructure.UI.Extensions
{
    // Static class for setting up dependency injection for UI services
    public static class DependencyInjection
    {
        // Extension method for IServiceCollection to add UI-specific services
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            // Configures Toast Notifications
            services.AddNotyf(options =>
            {
                options.HasRippleEffect = true; // Enable ripple effect on notifications
                options.DurationInSeconds = 3; // Duration of the notification in seconds
                options.IsDismissable = true; // Allow users to dismiss notifications
                options.Position = NotyfPosition.BottomRight; // Position of the notification on the screen
            });

            // Configures Identity services for user and role management with Entity Framework
            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddDefaultTokenProviders() //Configures default token providers
                .AddEntityFrameworkStores<AppDbContext>(); // Registers the AppDbContext for Identity

            return services; // Returns the IServiceCollection for chaining
        }
    }
}

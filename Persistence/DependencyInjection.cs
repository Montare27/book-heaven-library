namespace persistence
{
    using business;
    using business.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookDbContext>(x =>
                x.UseNpgsql(configuration.GetConnectionString("BookHeaven")));
            
            services.AddScoped<IBookDbContext, BookDbContext>();
            
            return services;
        }
    }
}

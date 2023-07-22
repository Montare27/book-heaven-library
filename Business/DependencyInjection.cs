namespace business
{
    using Common.AutoMapper;
    using Extensions;
    using FluentValidation;
    using Interfaces;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddAutoMapper(expression => expression.AddProfile(new AutoMapperProfiles()));
            
            services.AddScoped<RatingService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<SaveBookService>();
            
            services.AddTransient<IFileEncoderService, FileBase64Service>();
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            
            return services;
        }
    }
}

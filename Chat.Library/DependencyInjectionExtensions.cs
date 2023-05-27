using Chat.Commons.Contracts;
using Chat.Library.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Library;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();

        return services;
    }
}
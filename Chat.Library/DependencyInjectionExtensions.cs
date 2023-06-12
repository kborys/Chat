using Chat.Library.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Library;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLibrary(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddTransient<IJwtUtils, JwtUtils>();

        return services;
    }
}
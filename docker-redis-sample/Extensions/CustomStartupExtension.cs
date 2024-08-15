using Docker.Redis.Sample.Shared.Domain.Configuration;
using Docker.Redis.Sample.Shared.Domain.Enums;
using Microsoft.Extensions.Options;
using Redis.OM;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Docker.Redis.Sample.Extensions
{
    public static class CustomStartupExtension
    {
        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            Enumeration.LoadValue<FundType>();

            services
                .AddSingleton(sp =>
                {
                    var options = sp.GetService<IOptions<RedisConnectionStringsOptions>>();

                    if (options == null)
                        throw new Exception("String de conexão inválida");

                    return ConnectionMultiplexer.Connect(options.Value.ConnectionStringRedis).GetDatabase(0);
                })
                .AddSingleton(sp =>
                {
                    var options = sp.GetService<IOptions<RedisConnectionStringsOptions>>();

                    if (options == null)
                        throw new Exception("String de conexão inválida");

                    return new RedisConnectionProvider(ConnectionMultiplexer.Connect(options!.Value.ConnectionStringRedis));
                });

            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    //options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<RedisConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));

            return services;
        }
    }
}

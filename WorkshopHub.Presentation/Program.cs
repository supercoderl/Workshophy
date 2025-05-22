
using Aikido.Zen.DotNetCore;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;
using WorkshopHub.Domain.Extensions;
using WorkshopHub.Infrastructure.Database;
using WorkshopHub.Infrastructure.Extensions;
using WorkshopHub.Presentation.Extensions;
using WorkshopHub.ServiceDefaults;
using HealthChecks.ApplicationStatus.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using WorkshopHub.Presentation.BackgroundServices;
using WorkshopHub.Domain.Consumers;

namespace WorkshopHub.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddServiceDefaults();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            if (builder.Environment.IsProduction())
            {
                builder.Services.AddZenFirewall();
            }

            var isAspire = builder.Configuration["ASPIRE_ENABLED"] == "true";

            var rabbitConfiguration = builder.Configuration.GetRabbitMqConfiguration();
            var redisConnectionString =
                isAspire ? builder.Configuration["ConnectionStrings:Redis"] : builder.Configuration["RedisHostName"];
            var dbConnectionString = isAspire
                ? builder.Configuration["ConnectionStrings:Database"]
                : builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services
                .AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddApplicationStatus()
                .AddSqlServer(dbConnectionString!)
                .AddRedis(redisConnectionString!, "Redis")
                .AddRabbitMQ(
                    async _ =>
                    {
                        var factory = new ConnectionFactory
                        {
                            Uri = new Uri(rabbitConfiguration.ConnectionString),
                        };
                        return await factory.CreateConnectionAsync();
                    },
                    name: "RabbitMQ");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(dbConnectionString,
                    b => b.MigrationsAssembly("WorkshopHub.Infrastructure"));
            });

            builder.Services.AddSwagger();
            builder.Services.AddAuth(builder.Configuration);
            builder.Services.AddInfrastructure("WorkshopHub.Infrastructure", dbConnectionString!);
            builder.Services.AddQueryHandlers();
            builder.Services.AddServices();
            builder.Services.AddSortProviders();
            builder.Services.AddCommandHandlers();
            builder.Services.AddNotificationHandlers();
            builder.Services.AddApiUser();
            builder.Services.AddEmail(builder.Configuration);

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<FanoutEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureNewtonsoftJsonSerializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });
                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.ConfigureNewtonsoftJsonDeserializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });

                    cfg.Host(rabbitConfiguration.Host, (ushort)rabbitConfiguration.Port, "/", h =>
                    {
                        h.Username(rabbitConfiguration.Username);
                        h.Password(rabbitConfiguration.Password);
                    });

                    // Every instance of the service will receive the message
                    cfg.ReceiveEndpoint("workshop-hub-" + Guid.NewGuid(), e =>
                    {
                        e.Durable = false;
                        e.AutoDelete = true;
                        e.ConfigureConsumer<FanoutEventConsumer>(context);
                        e.DiscardSkippedMessages();
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            builder.Services.AddHostedService<SetInactiveUsersService>();

            builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

            builder.Services.AddLogging(x => x.AddSimpleConsole(console =>
            {
                console.TimestampFormat = "[yyyy-MM-ddTHH:mm:ss.fff] ";
                console.IncludeScopes = true;
            }));

            if (builder.Environment.IsProduction() || !string.IsNullOrWhiteSpace(redisConnectionString))
            {
                builder.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                    options.InstanceName = "workshop-hub";
                });
            }
            else
            {
                builder.Services.AddDistributedMemoryCache();
            }

            var app = builder.Build();

            app.MapDefaultEndpoints();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appDbContext = services.GetRequiredService<ApplicationDbContext>();
                var storeDbContext = services.GetRequiredService<EventStoreDbContext>();
                var domainStoreDbContext = services.GetRequiredService<DomainNotificationStoreDbContext>();

                appDbContext.EnsureMigrationsApplied();
                storeDbContext.EnsureMigrationsApplied();
                domainStoreDbContext.EnsureMigrationsApplied();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            if (builder.Environment.IsProduction())
            {
                app.UseZenFirewall();
            }

            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapControllers();

            app.Run();
        }
    }
}

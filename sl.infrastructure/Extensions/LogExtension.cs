using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using sl.infrastructure.Validation;
using sl.application.Models;
using sl.domain.Models;
using sl.infrastructure.Repositories;
using sl.application.Queries;
using sl.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace sl.infrastructure.Extensions
{
    public static class LogExtension
    {
        public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(LogViewModel).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            var connectionString = configuration.GetConnectionString("db");
            services.AddDbContextPool<LogDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    x => x.MigrationsAssembly("sl.infrastructure.migrations")
                          .MigrationsHistoryTable("ef_migrations")
                    );
            });
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            return services;
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerProvider loggerProvider)
        {
            var logger = loggerProvider.CreateLogger("exception-handler");
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/problem+json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var details = new ProblemDetails
                        {
                            Status = context.Response.StatusCode,
                            Title = "Internal Server Error.",
                            Detail = contextFeature.Error.Message
                        };

                        var jsonValue = System.Text.Json.JsonSerializer.Serialize(details);

                        await context.Response.WriteAsync(jsonValue);
                    }
                });
            });
        }
    }
}

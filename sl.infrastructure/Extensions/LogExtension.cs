using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bp.common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using sl.infrastructure.Validation;
using sl.application.Models;
using sl.domain.Models;
using sl.infrastructure.Repositories;
using sl.application.Queries;

namespace sl.infrastructure.Extensions
{
    public static class LogExtension
    {
        public static IServiceCollection AddLogs(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LogViewModel).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            return services;
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerProvider loggerProvider)
        {
            var logger = loggerProvider.CreateLogger("exception=handler");
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/problem+json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        ProblemDetails details;
                        if (contextFeature.Error.IsSystemException())
                        {
                            details = new ProblemDetails
                            {
                                Status = context.Response.StatusCode,
                                Title = "Internal Server Error.",
                                Detail = $"Something went wrong!",
                            };
                        }
                        else
                        {
                            details = new ProblemDetails
                            {
                                Status = context.Response.StatusCode,
                                Title = "Internal Logical Error.",
                                Detail = contextFeature.Error.Message,
                            };
                        }
                        var jsonValue = System.Text.Json.JsonSerializer.Serialize(details);

                        await context.Response.WriteAsync(jsonValue);
                    }
                });
            });
        }
    }
}

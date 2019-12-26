using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using sl.application.Commands.CreateLog;

namespace sl.infrastructure.Validation
{
    public static class FluentValidationExtension
    {
        /// <summary>
        /// Injects validation rules
        /// </summary>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CreateLogCommand>());
            return builder;
        }
    }
}

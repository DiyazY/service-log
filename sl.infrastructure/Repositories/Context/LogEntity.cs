using NpgsqlTypes;
using sl.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace sl.infrastructure.Repositories.Context
{
    public sealed class LogEntity
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string SystemId { get; set; }
        public string StackTrace { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string[] Labels { get; set; }

        /// <summary>
        /// Search vector
        /// </summary>
        public NpgsqlTsVector SearchVector { get; set; }

        internal static Expression<Func<LogEntity, Log>> Projection
        {
            get
            {
                return log => Log.InstanciateLog(
                    log.Id,
                    log.Message,
                    log.SystemId,
                    (LogLevel)Enum.Parse(typeof(LogLevel), log.Level),
                    log.StackTrace,
                    log.Labels,
                    log.RegisteredAt
                    );
            }
        }
    }
}

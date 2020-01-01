using NpgsqlTypes;
using sl.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace sl.infrastructure.Repositories.Context
{
    public sealed class LogEntity
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public string Level { get; private set; }
        public string SystemId { get; private set; }
        public string StackTrace { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public List<string> Labels { get; private set; }

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
                    log.Labels.ToArray(),
                    log.RegisteredAt
                    );
            }
        }
    }
}

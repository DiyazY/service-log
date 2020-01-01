using sl.domain.Models;
using sl.infrastructure.Repositories.Context;
using System;
using System.Linq;

namespace sl.infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly LogDbContext _dbContext;
        public LogRepository(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddLog(Log log)
        {
            var entity = new LogEntity
            {
                Id = log.Id,
                Message = log.Message,
                Level = log.Level.ToString(),
                SystemId = log.SystemId,
                StackTrace = log.StackTrace,
                RegisteredAt = log.RegisteredAt,
                Labels = log.Labels?.Select(p => p.Value).ToArray()
            };
            _dbContext.Logs.Add(entity);
            _dbContext.SaveChanges();
        }

        public Log GetLogById(Guid id)
        {
            var entity = _dbContext.Logs.First(p => p.Id == id);
            var log = Log.InstanciateLog(
                entity.Id,
                entity.Message,
                entity.SystemId,
                (LogLevel)Enum.Parse(typeof(LogLevel),
                entity.Level),
                entity.StackTrace,
                entity.Labels.ToArray(),
                entity.RegisteredAt);
            return log;
        }
    }
}

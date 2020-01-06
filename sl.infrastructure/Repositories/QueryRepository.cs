using Microsoft.EntityFrameworkCore;
using sl.application.Models;
using sl.application.Queries;
using sl.infrastructure.Repositories.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sl.infrastructure.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly LogDbContext _dbContext;
        public QueryRepository(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<LogViewModel> GetLogsByTerm(string systemId, int page = 1, int count = 5, string queryString = null)
        {
            var query = _dbContext.Logs.Where(p => p.SystemId.ToUpper() == systemId.ToUpper());

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                query = query.Where(p => p.SearchVector.Matches(queryString));
            }

            var logs = query
                .Skip((page - 1) * count)
                .Take(count)
                .Select(p => new LogViewModel
                {
                    Id = p.Id,
                    Level = p.Level,
                    Labels = p.Labels,
                    Message = p.Message,
                    SystemId = p.SystemId,
                    RegisteredAt = p.RegisteredAt
                });
            return logs;
        }

        public Task<int> GetLogsCountByTerm(string systemId, string queryString = null)
        {
            var query = _dbContext.Logs.Where(p => p.SystemId.ToUpper() == systemId.ToUpper());

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                query = query.Where(p => p.SearchVector.Matches(queryString));
            }

            return query.CountAsync();
        }
    }
}

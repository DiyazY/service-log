using sl.application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sl.application.Queries
{
    public interface IQueryRepository
    {
        IEnumerable<LogViewModel> GetLogsByTerm(string systemId, int page = 1, int count = 5, string queryString = null);
        Task<int> GetLogsCountByTerm(string systemId, string queryString = null);
    }
}

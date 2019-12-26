using sl.application.Models;
using System.Collections.Generic;

namespace sl.application.Queries
{
    public interface IQueryRepository
    {
        IEnumerable<LogViewModel> GetLogsByTerm(string systemId, int page = 1, int count = 5, string queryString = null);
    }
}

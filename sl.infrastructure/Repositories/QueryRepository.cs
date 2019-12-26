using sl.application.Models;
using sl.application.Queries;
using System;
using System.Collections.Generic;

namespace sl.infrastructure.Repositories
{
    //TODO: implement
    public class QueryRepository : IQueryRepository
    {
        public IEnumerable<LogViewModel> GetLogsByTerm(string systemId, int page = 1, int count = 5, string queryString = null)
        {
            throw new NotImplementedException();
        }
    }
}

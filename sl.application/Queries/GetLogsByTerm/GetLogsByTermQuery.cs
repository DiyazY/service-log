using MediatR;
using sl.application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace sl.application.Queries.GetLogsByTerm
{
    public class GetLogsByTermQuery : IRequest<LogsByTermResponse>
    {
        public string SystemId { get; set; }
        public string Term { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }

        public sealed class Handler : IRequestHandler<GetLogsByTermQuery, LogsByTermResponse>
        {
            private readonly IQueryRepository _queryRepository;
            public Handler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public Task<LogsByTermResponse> Handle(GetLogsByTermQuery request, CancellationToken cancellationToken)
            {
                var count = _queryRepository.GetLogsCountByTerm(request.SystemId, request.Term);
                var logs = _queryRepository.GetLogsByTerm(request.SystemId, request.Page, request.Count, request.Term);
                return Task.FromResult(new LogsByTermResponse
                {
                    Logs = logs,
                    Count = count.GetAwaiter().GetResult()
                });
            }
        }
    }
}

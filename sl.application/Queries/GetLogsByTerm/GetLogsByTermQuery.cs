using MediatR;
using sl.application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace sl.application.Queries.GetLogsByTerm
{
    public class GetLogsByTermQuery : IRequest<IEnumerable<LogViewModel>>
    {
        public string SystemId { get; set; }
        public string Term { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }

        public sealed class Handler : IRequestHandler<GetLogsByTermQuery, IEnumerable<LogViewModel>>
        {
            private readonly IQueryRepository _queryRepository;
            public Handler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public Task<IEnumerable<LogViewModel>> Handle(GetLogsByTermQuery request, CancellationToken cancellationToken)
            {
                var logs = _queryRepository.GetLogsByTerm(request.SystemId, request.Page, request.Count, request.Term);
                return Task.FromResult(logs);
            }
        }
    }
}

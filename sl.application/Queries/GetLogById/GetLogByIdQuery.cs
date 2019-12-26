using MediatR;
using sl.application.Extensions;
using sl.application.Models;
using sl.domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sl.application.Queries.GetLogById
{
    public sealed class GetLogByIdQuery: IRequest<LogDetailedViewModel>
    {
        public Guid Id { get; set; }

        public sealed class Handler : IRequestHandler<GetLogByIdQuery, LogDetailedViewModel>
        {
            private readonly ILogRepository _logRepository;
            public Handler(ILogRepository logRepository)
            {
                _logRepository = logRepository;
            }
            public Task<LogDetailedViewModel> Handle(GetLogByIdQuery request, CancellationToken cancellationToken)
            {
                var log = _logRepository.GetLogById(request.Id);
                return Task.FromResult(log.ToDetailedViewModel());
            }
        }
    }
}

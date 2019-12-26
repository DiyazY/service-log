using MediatR;
using sl.domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sl.application.Commands.CreateLog
{
    public sealed class CreateLogCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string SystemId { get; set; }
        public string StackTrace { get; set; }
        public string[] Labels { get; set; }

        public class Handler : IRequestHandler<CreateLogCommand>
        {
            private readonly ILogRepository _logRepository;
            public Handler(ILogRepository logRepository)
            {
                _logRepository = logRepository;
            }

            public Task<Unit> Handle(CreateLogCommand request, CancellationToken cancellationToken)
            {
                Enum.TryParse(request.Level, out LogLevel logLevel);
                var log = Log.CreateLog(
                    request.Id,
                    request.Message,
                    request.SystemId,
                    logLevel,
                    request.StackTrace,
                    request.Labels
                    );
                _logRepository.AddLog(log);
                return Task.FromResult(Unit.Value);
            }
        }
    }
}

using sl.application.Models;
using sl.domain.Models;
using System.Linq;

namespace sl.application.Extensions
{
    public static class LogExtension
    {
        public static LogDetailedViewModel ToDetailedViewModel(this Log log)
        {
            return new LogDetailedViewModel
            {
                Id = log.Id,
                Level = log.Level.ToString(),
                StackTrace = log.StackTrace,
                Labels = log.Labels.Select(p=>p.Value).ToArray(),
                SystemId = log.SystemId,
                Message = log.Message,
                RegisteredAt = log.RegisteredAt
            };
        }
    }
}

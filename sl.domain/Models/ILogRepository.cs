using System;

namespace sl.domain.Models
{
    public interface ILogRepository
    {
        void AddLog(Log log);
        Log GetLogById(Guid id);
    }
}

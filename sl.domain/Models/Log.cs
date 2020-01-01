using sl.domain.Exceptions;
using System;
using System.Collections.Generic;

namespace sl.domain.Models
{
    public class Log
    {
        #region props
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public LogLevel Level { get; private set; }
        public string SystemId { get; private set; }
        public string StackTrace { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public List<Label> Labels { get; private set; }
        #endregion

        #region constructors
        private Log()
        {

        }
        private Log(Guid id, string message, string systemId, LogLevel level)
        {
            if (id == Guid.Empty)
                throw new LogDomainException("Log's id is empty!!!");
            Id = id;

            if(string.IsNullOrWhiteSpace(message))
                throw new LogDomainException("Log's message is empty!!!");
            Message = message;

            if (string.IsNullOrWhiteSpace(systemId))
                throw new LogDomainException("Log's system id is empty!!!");
            SystemId = systemId;

            if (level == LogLevel.Unknown)
                throw new LogDomainException("Log's level is not right!!! Ex.: Trace, Debug, Info, Warn, Error, Fatal.");
            Level = level;

            RegisteredAt = DateTime.UtcNow;
        }
        #endregion

        #region methods

        public static Log CreateLog(Guid id, string message, string systemId, LogLevel level, string stackTrace, string [] labels = null)
        {
            var log = new Log(id, message, systemId, level);

            if (!string.IsNullOrWhiteSpace(stackTrace))
            {
                log.StackTrace = stackTrace;
            }

            if(labels?.Length > 0)
            {
                log.Labels = new List<Label>(labels.Length);
                for (int i = 0; i < labels.Length; i++)
                {
                    var label = new Label(labels[i]);
                    log.Labels.Add(label);
                }
            }

            return log;
        }

        public static Log InstanciateLog(Guid id, string message, string systemId, LogLevel level, string stackTrace, string[] labels, DateTime registeredAt)
        {
            var log = CreateLog(id, message, systemId, level, stackTrace, labels);
            log.RegisteredAt = registeredAt;
            return log;
        }

        #endregion

    }
}

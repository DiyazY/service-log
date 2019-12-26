using System;

namespace sl.application.Models
{
    public class LogDetailedViewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string SystemId { get; set; }
        public string StackTrace { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string[] Labels { get; set; }
    }
}

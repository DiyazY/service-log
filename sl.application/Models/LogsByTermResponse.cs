using System;
using System.Collections.Generic;
using System.Text;

namespace sl.application.Models
{
    public class LogsByTermResponse
    {
        public IEnumerable<LogViewModel> Logs { get; set; }
        public int Count { get; set; }
    }
}

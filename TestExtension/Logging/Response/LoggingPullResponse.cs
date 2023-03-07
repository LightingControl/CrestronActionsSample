using System.Collections.Generic;
using TwoN.Common.Interfaces;
using TwoN.Logging.Response.Entity;

namespace TwoN.Logging.Response
{
    public class LoggingPullResponse : IResponse
    {
        public LoggingPullResponse(IEnumerable<LoggingEventEntity> events)
        {
            Events = events;
        }

        public IEnumerable<LoggingEventEntity> Events { get; set; }

        public bool Success => true;
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoN.Common.Interfaces;
using TwoN.Logging.Common;

namespace TwoN.Logging.Interface
{
    public interface ILoggingService : IService
    {
        Task<IResponse> Caps();
        Task<IResponse> Subscribe(LoggingSubscribeParamInclude include = LoggingSubscribeParamInclude.New, IEnumerable<LoggingEventName> filter = null, uint duration = 0, int includeTimeOffset = 0);
        Task<IResponse> Unsubscribe(uint id);
        Task<IResponse> Pull(uint id, uint timeout = 0);
    }
}
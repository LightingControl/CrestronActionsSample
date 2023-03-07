using TwoN.Common.Interfaces;

namespace TwoN.Logging.Response

{
    public class LoggingSubscribeResponse : IResponse
    {
        public LoggingSubscribeResponse(uint id)
        {
            Id = id;
        }

        public uint Id { get; set; }

        public bool Success => true;
    }
}
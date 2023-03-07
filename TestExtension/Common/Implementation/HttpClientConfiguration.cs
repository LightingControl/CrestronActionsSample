using System.Net;
using TwoN.Common.Enums;
using TwoN.Common.Interfaces;

namespace TwoN.Common.Implementation
{
    public class HttpClientConfiguration : IHttpClientConfiguration
    {
        public IPAddress Address { get; set; }
        public ushort Port { get; set; }
        public int Timeout { get; set; }
        public NetworkCredential NetworkCredential { get; set; }
        public Schema Schema { get; set; }
    }
}
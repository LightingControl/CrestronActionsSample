using System.Net;
using TwoN.Common.Enums;

namespace TwoN.Common.Interfaces
{
    public interface IHttpClientConfiguration
    {
        IPAddress Address { get; set; }
        ushort Port { get; set; }
        int Timeout { get; set; }
        NetworkCredential NetworkCredential { get; set; }
        Schema Schema { get; set; }
    }
}
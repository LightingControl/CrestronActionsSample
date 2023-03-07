using System;
using System.Threading.Tasks;

namespace TwoN.Common.Interfaces
{
    public interface IHttpClient : IDisposable
    {
        Task<string> GetAsync(Uri endpoint);
        byte[] GetBytes(Uri endpoint);
        string Post(Uri endpoint, string payload);
        string Put(Uri endpoint, string payload);
        string Put(Uri endpoint, byte[] data);
        string Delete(Uri endpoint, string payload);
        void ClientConfigure(IHttpClientConfiguration clientConfiguration);
    }
}
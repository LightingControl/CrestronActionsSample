using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TwoN.Common.Enums;
using TwoN.Common.Interfaces;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable InconsistentNaming

namespace TwoN.Common.Implementation
{
    public class HttpClient : IHttpClient
    {
        private IHttpClientConfiguration configuration;
        private Uri baseAddress;
        private System.Net.Http.HttpClient client;

        public HttpClient(IHttpClientConfiguration clientConfiguration)
        {
            ClientConfigure(clientConfiguration);
        }

        public async Task<string> GetAsync(Uri endpoint)
        {
            var response = await client.GetAsync(endpoint).ConfigureAwait(false);
            var resultContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent;
        }

        public void ClientConfigure(IHttpClientConfiguration clientConfiguration)
        {
            this.configuration = clientConfiguration ?? throw new ArgumentNullException($"{nameof(clientConfiguration)} cannot be null.");
            CreateHttpClient();
        }

        public byte[] GetBytes(Uri endpoint) => throw new NotImplementedException();

        public string Post(Uri endpoint, string payload) => throw new NotImplementedException();

        public string Put(Uri endpoint, string payload) => throw new NotImplementedException();

        public string Put(Uri endpoint, byte[] data) => throw new NotImplementedException();

        public string Delete(Uri endpoint, string payload) => throw new NotImplementedException();

        private void CreateHttpClient()
        {
            // Dispose HTTP Client if already created
            this.client?.Dispose();

            this.baseAddress = this.configuration.Schema == Schema.Http ? new Uri($"http://{this.configuration.Address}") : new Uri($"https://{this.configuration.Address}");

            // HttpClient creation
            var handler = new HttpClientHandler { Credentials = this.configuration.NetworkCredential };
            this.client = new System.Net.Http.HttpClient(handler)
            {
                BaseAddress = this.baseAddress,
                Timeout = TimeSpan.FromSeconds(this.configuration.Timeout)
            };

            // IgnoreBadCert
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
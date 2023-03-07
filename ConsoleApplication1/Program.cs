using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static SwitchService Switch;
        private static BinService Bin;
        private static bool state;
        
        public static void Main(string[] args)
        {
                Initialize();
                Switch.Ctrl(2,SwitchService.SwitchAction.On);
                Console.ReadKey();          
                Switch.Ctrl(2,SwitchService.SwitchAction.Off);
                Console.ReadKey();
                Bin.sendBin();
                Console.ReadKey();
        }
        
        

        public static void Initialize()
        {
            Switch = new SwitchService();
            Bin = new BinService();
            state = false;

            
            

        }

        
    }
    public enum Schema
    {
        Http,
        Https
    }
    public class HttpClientConfiguration 
    {
        public IPAddress Address { get; set; }
        public ushort Port { get; set; }
        public int Timeout { get; set; }
        public NetworkCredential NetworkCredential { get; set; }
        public Schema Schema { get; set; }
    }

    public class SwitchService 
    {
        public System.Net.Http.HttpClient HttpClient { get; set; }

        public SwitchService()
        {
            // Create http configuration
            var httpConfiguration = new HttpClientConfiguration
            {
                Address = IPAddress.Parse("192.168.10.26"),
                Schema = Schema.Https,
                Timeout = 15,
                NetworkCredential = new NetworkCredential("apiuser","apipass")
            };

            // HttpClient creation
            var handler = new HttpClientHandler { Credentials = httpConfiguration.NetworkCredential };
            HttpClient = new HttpClient(handler)
            {
                BaseAddress =  new Uri($"https://{httpConfiguration.Address}"),
                Timeout = TimeSpan.FromSeconds(15)
            };

            // IgnoreBadCert
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
           
        }
       
        internal static string CtrlString(int switchId, SwitchAction action, string response)
        {
            var api = "/api/switch/ctrl";

            api = string.Concat(api, "?switch=", switchId);
            api = string.Concat(api, "&action=", action.ToString().ToLower());
            if (response != null)
                api = string.Concat(api, "&response=", response);

            return api;
        }
        public enum SwitchAction
        {
            On,
            Off,
            Trigger
        }
        public void Ctrl(int switchId, SwitchAction action, string response = null)
        {
            try
            {
                //CrestronConsole.PrintLine($"Sending Http Call ");
                var resource = CtrlString(switchId, action, response);
                var result = HttpClient.GetAsync(new Uri(resource, UriKind.Relative));

            }
            catch (Exception e)
            {
                //CrestronConsole.PrintLine($"Error In Http Call : {e}");
                
            }
        }

        
    }
    public class BinService 
    {
        public System.Net.Http.HttpClient HttpClient { get; set; }

        public BinService()
        {
            // Create http configuration
            var httpConfiguration = new HttpClientConfiguration
            {
                Address = IPAddress.Parse("34.205.150.168"),
                Schema = Schema.Https,
                Timeout = 15,
                NetworkCredential = new NetworkCredential("apiuser","apipass")
            };

            // HttpClient creation
            var handler = new HttpClientHandler { Credentials = httpConfiguration.NetworkCredential };
            HttpClient = new HttpClient(handler)
            {
                BaseAddress =  new Uri($"https://{httpConfiguration.Address}"),
                Timeout = TimeSpan.FromSeconds(15)
            };

            // IgnoreBadCert
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
           
        }

        public async Task sendBin()
        {
            try
            {
                var resource = "/basic-auth/apiuser/apipass";
                var requestUri = new Uri(resource, UriKind.Relative);
                log(1,$"Sending:{requestUri.ToString()}");
                var response = await HttpClient.GetAsync(requestUri).ConfigureAwait(false);
                var resultContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
                log(1,resultContent);

            }
            catch (Exception e)
            {
                log(1, e.Message);
                log(1, $"Exception in Call to httpbin {e.Message}");
                var ine = e.InnerException;
                while (ine != null)
                {
                    log(1, $"Inner exception: {ine.Message}");
                    log(1, $"Inner exception: {ine.StackTrace}");
                    ine = ine.InnerException;
                }
            }
        }

        private void log(int p0, string result)
        {
            Console.WriteLine(result);
        }
    }
}

    
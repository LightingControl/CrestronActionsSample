using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
//using System.Net.Http;
using System.Threading.Tasks;
using Crestron.RAD.Common.Attributes.Programming;
using Crestron.RAD.Common.Interfaces;
using Crestron.RAD.Common.Interfaces.ExtensionDevice;
using Crestron.RAD.DeviceTypes.ExtensionDevice;
using Crestron.SimplSharp;
using Newtonsoft.Json;
using TwoN.Common.Enums;
using TwoN.Common.Error;
using TwoN.Common.Implementation;
using TwoN.Common.Interfaces;
using TwoN.Logging.Common;
using HttpClient = TwoN.Common.Implementation.HttpClient;
using IPAddress = System.Net.IPAddress;


namespace TestExtension
{
    public class TESTEXT : AExtensionDevice , ICloudConnected
    {
        
        private bool state;
        private HttpClient client;
        private HttpClient client2;
        private LoggingService loggingService;
        private LoggingService loggingServiceBIN;

     

        
        protected override IOperationResult DoCommand(string command, string[] parameters)
        {
            
            if (command == "sendone")
            {

                PullLog(1);
            }
            
            if (command == "sendtwo")
            {

                PullLogBIN(1);
            }

            return new OperationResult(OperationResultCode.Success);
        }

        protected override IOperationResult SetDriverPropertyValue<T>(string propertyKey, T value)
        {
            return null;
        }

        protected override IOperationResult SetDriverPropertyValue<T>(string objectId, string propertyKey, T value)
        {
            return null;
        }
        public async Task<IResponse> SubscribeLog(IEnumerable<LoggingEventName> filter = null)
        {
            try
            {
                if (loggingService == null)
                    throw new NullReferenceException(
                        $"{nameof(loggingService)} cannot be null.");

                return await loggingService.Subscribe(filter: filter);
            }
            catch (Exception e)
            {
                log(1, $"Exception in Subscribe Log {e.Message}");
                var ine = e.InnerException;
                while (ine != null)
                {
                    log(1, $"Inner exception: {ine.Message}");
                    log(1, $"Inner exception: {ine.StackTrace}");
                    ine = ine.InnerException;
                }
            }

            return null;
        }
        public async Task<IResponse> SubscribeLogBIN(IEnumerable<LoggingEventName> filter = null)
        {
            try
            {
                if (loggingService == null)
                    throw new NullReferenceException(
                        $"{nameof(loggingService)} cannot be null.");

                return await loggingService.Subscribe(filter: filter);
            }
            catch (Exception e)
            {
                log(1, $"Exception in Subscribe Log {e.Message}");
                var ine = e.InnerException;
                while (ine != null)
                {
                    log(1, $"Inner exception: {ine.Message}");
                    log(1, $"Inner exception: {ine.StackTrace}");
                    ine = ine.InnerException;
                }
            }

            return null;
        }
        public async Task<IResponse> PullLog(uint id)
        {
            try
            {
                if (loggingService == null)
                    throw new NullReferenceException($"{nameof(loggingService)} should not be null.");

                var response = await loggingService.Pull(id);

                if (response is ErrorResponse errorResponse)
                {
                    log(1, errorResponse.Description);
                }
                
                return response;
            }
            catch (Exception e)
            {
                log(1, e.Message);
                log(1, $"Exception in PullLog {e.Message}");
                var ine = e.InnerException;
                while (ine != null)
                {
                    log(1, $"Inner exception: {ine.Message}");
                    log(1, $"Inner exception: {ine.StackTrace}");
                    ine = ine.InnerException;
                }
            }

            return null;
        }
        public async Task<IResponse> PullLogBIN(uint id)
        {
            try
            {
                var resource = "/basic-auth/apiuser/apipass";
                var result = await client2.GetAsync(new Uri(resource, UriKind.Relative));
                
                log(1,result);

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

            return null;
        }
        private void log(int i1, string errorResponseDescription)
        {
            CrestronConsole.PrintLine(errorResponseDescription);
        }

        public void Initialize()
        {
            
            // Create http configuration
            // Create http configuration
            var httpConfiguration = new HttpClientConfiguration
            {
                Address = IPAddress.Parse("192.168.10.26"),
                Schema = Schema.Https,
                Timeout = 15,
                NetworkCredential = new NetworkCredential("apiuser","apipass")
            };
            var httpConfiguration2 = new HttpClientConfiguration
            {
                Address = IPAddress.Parse("34.205.150.168"),
                Schema = Schema.Https,
                Timeout = 15,
                NetworkCredential = new NetworkCredential("apiuser","apipass")
            };
            client2 = new HttpClient(httpConfiguration2);
            
            client = new HttpClient(httpConfiguration);

            // Create services
            //switchService = new SwitchService(client);
            //ioService = new IoService(client);
            loggingService = new LoggingService(client);
            //callService = new PhoneCallService(client);

            // Monitor setup
            
            var monitorRequire = new List<LoggingEventName>
            {
              LoggingEventName.SwitchStateChanged,
            };
            
          
            state = false;

            Connected = true;

            SubscribeLog(monitorRequire);

        }
      
    }
    
   
}

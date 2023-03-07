using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwoN.Common;
using TwoN.Common.Interfaces;
using TwoN.Logging.Common;
using TwoN.Logging.Interface;
using TwoN.Logging.Json;
using TwoN.Logging.Json.EventType;
using TwoN.Logging.Response;
using TwoN.Logging.Response.Entity;

namespace TestExtension
{
    public class LoggingService : ILoggingService
    {
        public IHttpClient HttpClient { get; set; }
        
        public LoggingService(IHttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<IResponse> Unsubscribe(uint id) => throw new NotImplementedException();
        
        public async Task<IResponse> Caps() => throw new NotImplementedException();

        public async Task<IResponse> Pull(uint id, uint timeout = 0)
        {
            try
            {
                var resource = LoggingEndpoint.Pull(id, timeout);
                var result = await HttpClient
                    .GetAsync(new Uri(resource, UriKind.Relative));
                var pullResponse = JsonConvert.DeserializeObject<PullResponseJson>(result);

                IResponse response;

                if (pullResponse.Success)
                {
                    var entities = new List<LoggingEventEntity>();
                    pullResponse.Result.Events.ForEach(x =>
                    {
                        var eventName = Utils.ParseEnum<LoggingEventName>(x.Name);
                        entities.Add(new LoggingEventEntity
                        {
                            Id = x.Id,
                            TzShift = x.TzShift,
                            UtcTime = x.UtcTime,
                            UpTime = x.UpTime,
                            Event = eventName,
                            Params = GetParams(eventName, x.Params)
                        });
                    });

                    response = new LoggingPullResponse(entities);
                }
                else
                {
                    response = Utils.ErrorResponse(result);
                }

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IResponse> Subscribe(LoggingSubscribeParamInclude include = LoggingSubscribeParamInclude.New,
            IEnumerable<LoggingEventName> filter = null, uint duration = 0, int includeTimeOffset = 0)
        {
            try
            {
                var resource = LoggingEndpoint.Subscribe(include, filter, duration, includeTimeOffset);
                var result = await HttpClient
                    .GetAsync(new Uri(resource, UriKind.Relative));
                var subscribeResponse = JsonConvert.DeserializeObject<SubscribeJson>(result);
                
          
                var response = subscribeResponse.Success ? new LoggingSubscribeResponse(subscribeResponse.Result.Id) : Utils.ErrorResponse(result);

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Internal

        private LoggingEventEntity.ILoggingEventEntityParams GetParams(LoggingEventName eventName, object param)
        {
            LoggingEventEntity.ILoggingEventEntityParams result = null;
            switch (eventName)
            {
                case LoggingEventName.DeviceState:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsDeviceState>();

                        result = new LoggingEventEntity.DeviceState
                        {
                            State = Utils.ParseEnum<LoggingEventEntity.DeviceState.DeviceStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.AudioLoopTest:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsAudioLoopTest>();

                        result = new LoggingEventEntity.AudioLoopTest
                        {
                            Result = Utils.ParseEnum<LoggingEventEntity.AudioLoopTest.AudioResultEnum>(paramsObj.Result)
                        };
                    }
                    break;
                case LoggingEventName.MotionDetected:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsMotionDetected>();

                        result = new LoggingEventEntity.MotionDetected
                        {
                            State = Utils.ParseEnum<LoggingEventEntity.MotionDetected.MotionStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.NoiseDetected:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsNoiseDetected>();

                        result = new LoggingEventEntity.NoiseDetected
                        {
                            State = Utils.ParseEnum<LoggingEventEntity.NoiseDetected.NoiseStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.KeyPressed:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsKeyPressed>();

                        LoggingEventEntity.Key key;
                        uint quickDialButton = 0;
                        if (paramsObj.Key.StartsWith("%"))
                        {
                            key = LoggingEventEntity.Key.QuickDial;
                            quickDialButton = uint.Parse(paramsObj.Key.Substring(1));
                        }
                        else if (paramsObj.Key == "*")
                        {
                            key = LoggingEventEntity.Key.Star;
                        }
                        else if (paramsObj.Key == "#")
                        {
                            key = LoggingEventEntity.Key.Hatch;
                        }
                        else
                        {
                            uint number = uint.Parse(paramsObj.Key);
                            key = (LoggingEventEntity.Key)number;
                        }

                        result = new LoggingEventEntity.KeyPressed
                        {
                            Key = key,
                            QuickDialButton = quickDialButton
                        };
                    }
                    break;
                case LoggingEventName.KeyReleased:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsKeyReleased>();

                        LoggingEventEntity.Key key;
                        uint quickDialButton = 0;
                        if (paramsObj.Key.StartsWith("%"))
                        {
                            key = LoggingEventEntity.Key.QuickDial;
                            quickDialButton = uint.Parse(paramsObj.Key.Substring(1));
                        }
                        else if (paramsObj.Key == "*")
                        {
                            key = LoggingEventEntity.Key.Star;
                        }
                        else if (paramsObj.Key == "#")
                        {
                            key = LoggingEventEntity.Key.Hatch;
                        }
                        else
                        {
                            uint number = uint.Parse(paramsObj.Key);
                            key = (LoggingEventEntity.Key)number;
                        }

                        result = new LoggingEventEntity.KeyReleased
                        {
                            Key = key,
                            QuickDialButton = quickDialButton
                        };
                    }
                    break;
                case LoggingEventName.CodeEntered:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsCodeEntered>();

                        result = new LoggingEventEntity.CodeEntered
                        {
                            Code = paramsObj.Code,
                            Valid = paramsObj.Valid
                        };
                    }
                    break;
                case LoggingEventName.CardEntered:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsCardEntered>();

                        result = new LoggingEventEntity.CardEntered
                        {
                            Direction = Utils.ParseEnum<LoggingEventEntity.CardEntered.CardDirectionEnum>(paramsObj.Direction),
                            Reader = paramsObj.Reader,
                            Uid = paramsObj.Uid,
                            Valid = paramsObj.Valid
                        };
                    }
                    break;
                case LoggingEventName.InputChanged:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsInputChanged>();

                        result = new LoggingEventEntity.InputChanged
                        {
                            Port = paramsObj.Port,
                            State = paramsObj.State
                        };
                    }
                    break;
                case LoggingEventName.OutputChanged:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsOutputChanged>();

                        result = new LoggingEventEntity.OutputChanged
                        {
                            Port = paramsObj.Port,
                            State = paramsObj.State
                        };
                    }
                    break;
                case LoggingEventName.SwitchStateChanged:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsSwitchStateChanged>();

                        result = new LoggingEventEntity.SwitchStateChanged
                        {
                            State = paramsObj.State,
                            Switch = paramsObj.Switch
                        };
                    }
                    break;
                case LoggingEventName.CallStateChanged:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsCallStateChanged>();

                        result = new LoggingEventEntity.CallStateChanged
                        {
                            Direction = Utils.ParseEnum<LoggingEventEntity.CallStateChanged.CallDirectionEnum>(paramsObj.Direction),
                            State = Utils.ParseEnum<LoggingEventEntity.CallStateChanged.CallStateEnum>(paramsObj.State),
                            Peer = paramsObj.Peer,
                            Reason = Utils.ParseEnum<LoggingEventEntity.CallStateChanged.CallReasonEnum>(paramsObj.Reason),
                            Session = paramsObj.Session,
                            Call = paramsObj.Call
                        };
                    }
                    break;
                case LoggingEventName.RegistrationStateChanged:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsRegistrationStateChanged>();

                        result = new LoggingEventEntity.RegistrationStateChanged
                        {
                            RegistrationState = Utils.ParseEnum<LoggingEventEntity.RegistrationStateChanged.RegistrationStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.TamperSwitchActivated:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsTamperSwitchActivated>();

                        result = new LoggingEventEntity.TamperSwitchActivated
                        {
                            TamperState = Utils.ParseEnum<LoggingEventEntity.TamperSwitchActivated.TamperStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.UnauthorizedDoorOpen:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsUnauthorizedDoorOpen>();

                        result = new LoggingEventEntity.UnauthorizedDoorOpen
                        {
                            UnAuthState = Utils.ParseEnum<LoggingEventEntity.UnauthorizedDoorOpen.UnAuthStateEnum>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.DoorOpenTooLong:
                    {
                        var jObj = (JObject)param;
                        var paramsObj = jObj.ToObject<ParamsDoorOpenTooLong>();

                        result = new LoggingEventEntity.DoorOpenTooLong
                        {
                            State = Utils.ParseEnum<LoggingEventEntity.DoorOpenTooLong.DoorOpen>(paramsObj.State)
                        };
                    }
                    break;
                case LoggingEventName.LoginBlocked:
                    {
                        JObject jObj = (JObject)param;
                        ParamsLoginBlocked paramsObj = jObj.ToObject<ParamsLoginBlocked>();

                        result = new LoggingEventEntity.LoginBlocked
                        {
                            Address = paramsObj.Address
                        };
                    }
                    break;
            }

            return result;
        }

        #endregion
    }
}
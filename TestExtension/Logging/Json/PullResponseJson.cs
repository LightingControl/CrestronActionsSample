using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwoN.Logging.Json
{
    internal class PullResponseJson
    {
        public bool Success { get; set; }
        public EventType.Result Result { get; set; }
    }

    namespace EventType
    {
        internal class Result
        {
            public List<LoggingEventJson> Events { get; set; }
        }

        internal class LoggingEventJson
        {
            public uint Id { get; set; }
            public uint UtcTime { get; set; }
            public uint UpTime { get; set; }
            public int TzShift { get; set; }
            [JsonProperty("Event")]
            public string Name { get; set; }
            public object Params { get; set; }
        }

        internal class ParamsDeviceState
        {
            public string State { get; set; }
        }

        internal class ParamsAudioLoopTest
        {
            public string Result { get; set; }
        }

        internal class ParamsMotionDetected
        {
            public string State { get; set; }
        }

        internal class ParamsNoiseDetected
        {
            public string State { get; set; }
        }

        internal class ParamsKeyPressed
        {
            public string Key { get; set; }
        }

        internal class ParamsKeyReleased
        {
            public string Key { get; set; }
        }

        internal class ParamsCodeEntered
        {
            public string Code { get; set; }
            public bool Valid { get; set; }
        }

        internal class ParamsCardEntered
        {
            public string Direction { get; set; }
            public string Reader { get; set; }
            public string Uid { get; set; }
            public bool Valid { get; set; }
        }

        internal class ParamsInputChanged
        {
            public string Port { get; set; }
            public bool State { get; set; }
        }

        internal class ParamsOutputChanged
        {
            public string Port { get; set; }
            public bool State { get; set; }
        }

        internal class ParamsSwitchStateChanged
        {
            public int Switch { get; set; }
            public bool State { get; set; }
        }

        internal class ParamsCallStateChanged
        {
            public string Direction { get; set; }
            public string State { get; set; }
            public string Peer { get; set; }
            public string Reason { get; set; }
            public uint Session { get; set; }
            public uint Call { get; set; }
        }

        internal class ParamsRegistrationStateChanged
        {
            public uint SipAccount { get; set; }
            public string State { get; set; }
        }

        internal class ParamsTamperSwitchActivated
        {
            public string State { get; set; }
        }

        internal class ParamsUnauthorizedDoorOpen
        {
            public string State { get; set; }
        }

        internal class ParamsDoorOpenTooLong
        {
            public string State { get; set; }
        }

        internal class ParamsLoginBlocked
        {
            public string Address { get; set; }
        }
    }

    internal class IntToBoolJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value.ToString() == "1";
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}

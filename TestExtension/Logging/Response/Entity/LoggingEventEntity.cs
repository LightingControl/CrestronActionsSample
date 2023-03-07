using TwoN.Logging.Common;

namespace TwoN.Logging.Response.Entity
{
    public class LoggingEventEntity
    {
        public uint Id { get; set; }
        public int TzShift { get; set; }
        public uint UtcTime { get; set; }
        public uint UpTime { get; set; }
        public LoggingEventName Event { get; set; }
        public ILoggingEventEntityParams Params { get; set; }

        public interface ILoggingEventEntityParams { }

        public class DeviceState : ILoggingEventEntityParams
        {
            public enum DeviceStateEnum
            {
                Unknown,
                Startup
            }

            public DeviceStateEnum State { get; set; }
        }

        public class AudioLoopTest : ILoggingEventEntityParams
        {
            public enum AudioResultEnum
            {
                Unknown,
                Passed,
                Failed
            }

            public AudioResultEnum Result { get; set; }
        }

        public class MotionDetected : ILoggingEventEntityParams
        {
            public enum MotionStateEnum
            {
                Unknown,
                In,
                Out
            }

            public MotionStateEnum State { get; set; }
        }

        public class NoiseDetected : ILoggingEventEntityParams
        {
            public enum NoiseStateEnum
            {
                Unknown,
                In,
                Out
            }

            public NoiseStateEnum State { get; set; }
        }

        public enum Key
        {
            Unknown,
            Zero = 0,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            QuickDial,
            Star,
            Hatch
        }

        public class KeyPressed : ILoggingEventEntityParams
        {
            public Key Key { get; set; }
            public uint QuickDialButton { get; set; }
        }

        public class KeyReleased : ILoggingEventEntityParams
        {
            public Key Key { get; set; }
            public uint QuickDialButton { get; set; }
        }

        public class CodeEntered : ILoggingEventEntityParams
        {
            public string Code { get; set; }
            public bool Valid { get; set; }
        }

        public class CardEntered : ILoggingEventEntityParams
        {
            public enum CardDirectionEnum
            {
                Unknown,
                In,
                Out,
                Any
            }

            public CardDirectionEnum Direction { get; set; }
            public string Reader { get; set; }
            public string Uid { get; set; }
            public bool Valid { get; set; }
        }

        public class InputChanged : ILoggingEventEntityParams
        {
            public string Port { get; set; }
            public bool State { get; set; }
        }

        public class OutputChanged : ILoggingEventEntityParams
        {
            public string Port { get; set; }
            public bool State { get; set; }
        }

        public class SwitchStateChanged : ILoggingEventEntityParams
        {
            public int Switch { get; set; }
            public bool State { get; set; }
        }

        public class CallStateChanged : ILoggingEventEntityParams
        {
            public enum CallDirectionEnum
            {
                Unknown,
                Incoming,
                Outgoing
            }

            public enum CallStateEnum
            {
                Unknown,
                Connecting,
                Ringing,
                Connected,
                Terminated
            }

            public enum CallReasonEnum
            {
                Unknown,
                Normal,
                Busy,
                Rejected,
                Noanswer,
                Noresponse,
                CompletedElsewhere,
                Failure
            }

            public CallDirectionEnum Direction { get; set; }
            public CallStateEnum State { get; set; }
            public string Peer { get; set; }
            public CallReasonEnum Reason { get; set; }
            public uint Session { get; set; }
            public uint Call { get; set; }
        }

        public class RegistrationStateChanged : ILoggingEventEntityParams
        {
            public enum SipAccountEnum
            {
                Unknown,
                First,
                Second
            }

            public enum RegistrationStateEnum
            {
                Unknown,
                Registered,
                Unregistered,
                Registering,
                Unregistering
            }

            public SipAccountEnum SipAccount { get; set; }
            public RegistrationStateEnum RegistrationState { get; set; }
        }

        public class TamperSwitchActivated : ILoggingEventEntityParams
        {
            public enum TamperStateEnum
            {
                Unknown,
                In,
                Out
            }

            public TamperStateEnum TamperState { get; set; }
        }

        public class UnauthorizedDoorOpen : ILoggingEventEntityParams
        {
            public enum UnAuthStateEnum
            {
                Unknown,
                In,
                Out
            }

            public UnAuthStateEnum UnAuthState { get; set; }
        }

        public class DoorOpenTooLong : ILoggingEventEntityParams
        {
            public enum DoorOpen
            {
                Unknown,
                In,
                Out
            }

            public DoorOpen State { get; set; }
        }

        public class LoginBlocked : ILoggingEventEntityParams
        {
            public string Address { get; set; }
        }
        
    }
}
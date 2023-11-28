using System;
using System.Collections.Generic;
using Crestron.RAD.Common.Attributes.Programming;
using Crestron.RAD.Common.BasicDriver;
using Crestron.RAD.Common.Interfaces;
using Crestron.RAD.Common.Interfaces.ExtensionDevice;
using Crestron.RAD.Common.Transports;
using Crestron.RAD.DeviceTypes.ExtensionDevice;
using Crestron.SimplSharp;

namespace TestExtension
{
    public class TESTEXT : AExtensionDevice , ICloudConnected
    {
        [ProgrammableOperation("ActionWithParameterNoDef")]
        public void ActionWithParameterNoDef(
            [Display("Time")]
            [Min(1)]
            [Max(10)]
            int time)
        {
            CrestronConsole.PrintLine("ActionCalled {0}",time);
    
        }
        
        [ProgrammableOperation("ActionWithParameterRest")]
        public void ActionWithParameterRestrictions (
            [Display("Time")]
            [Unit(Unit.Seconds)]
            [Default(5)]
            [Min(1)]
            [Max(10)]
            int time)
        {
            CrestronConsole.PrintLine("ActionCalled {0}",time);
    
        }
        
        [ProgrammableOperation("ActionWithParameter")]
        public void ActionWithParameter(int time)
        {
            CrestronConsole.PrintLine("ActionCalled {0}",time);
    
        }
        [ProgrammableOperation("ActionNoParameter")]
        public void ActionWithNoParameter()
        {
            CrestronConsole.PrintLine("ActionCalled ");
    
        }
        protected override IOperationResult DoCommand(string command, string[] parameters)
        {
            return null;
        }

        protected override IOperationResult SetDriverPropertyValue<T>(string propertyKey, T value)
        {
            return null;
        }

        protected override IOperationResult SetDriverPropertyValue<T>(string objectId, string propertyKey, T value)
        {
            return null;
        }
        [ProgrammableOperation("Send Command")]
        public void SendCommand(  [Display("Command")]
            [DynamicAvailableValues(nameof(TESTEXT), nameof(GetAvailableCommands), OperationType.Method)]
            string command)
        {
           // Do Stuff!! 
           
        }
        public IEnumerable<string> GetAvailableCommands()
        {
            
            var list = new List<string>();
            list.Add("ONE");
            list.Add("TWO");
            list.Add("THREE");

            return list;
        }

        public void Initialize()
        {
            // Check to see if the Driver has been inite in the concrete class
            if (DeviceProtocol == null)
            {
                if (ConnectionTransport == null)
                    ConnectionTransport = new LCBaseTransport();

                DeviceProtocol = new LCBaseProtocol(ConnectionTransport, 01);
                DeviceProtocol.Initialize(DriverData);
            }
        }

        public override void Connect()
        {
            var retrievedData = GetSetting("INSTANCEID");

            if (retrievedData != null)
            {
                string temp = (string)retrievedData;
                CrestronConsole.PrintLine($"Found Instance Data ! {retrievedData}");
            }
            else
            {
                CrestronConsole.PrintLine("Did not Find Instance Data !");
                SaveSetting("INSTANCEID","TESTINSTANCEID");
                CrestronConsole.PrintLine("SAVED  Instance Data !");
            }

            base.Connect();
        }
    }
    public class LCBaseTransport : ATransportDriver
    {
        public override void SendMethod(string message, object[] paramaters)
        {
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
    public class LCBaseProtocol : ABaseDriverProtocol
    {
        public bool _enablefeedback;


        public string CurrentHardware;
        public string Ipaddress;
        public string LicenceKey;
        public string Password;
        public string Username;

        public LCBaseProtocol(ISerialTransport transport, byte id) : base(transport, id)
        {
        }

        

        public override void SetUserAttribute(string attributeId, string attributeValue)
        {
            
            OnUserAttributeChanged(attributeId, attributeValue);
        }

        public override void SetUserAttribute(string attributeId, bool attributeValue)
        {
            
            OnUserAttributeChanged(attributeId, attributeValue);
        }

        private void OnUserAttributeChanged(string attributeId, string attributevalue)
        {
         
        }

        private void OnUserAttributeChanged(string attributeId, bool attributevalue)
        {
            
        }

        public override void SetUserAttribute(string attributeId, ushort attributeValue)
        {
            
        }


        protected override void ConnectionChangedEvent(bool connection)
        {
           
        }

        protected override void ChooseDeconstructMethod(ValidatedRxData validatedData)
        {
            
        }
    }

}

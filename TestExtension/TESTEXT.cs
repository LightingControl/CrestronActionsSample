using Crestron.RAD.Common.Attributes.Programming;
using Crestron.RAD.Common.Interfaces;
using Crestron.RAD.Common.Interfaces.ExtensionDevice;
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

        public void Initialize()
        {
            
        }
    }
}
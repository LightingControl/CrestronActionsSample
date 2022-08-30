# CrestronActionsSample

This small project has been produced to test :

ProgrammableOperationAttribute
To get a method or property for a Crestron Home Extension device to appear as an operation in sequences automatically, 
the method or property should be decorated with the ProgrammableOperationAttribute. Refer to the following sample code.

The test System was Crestron Home running on MC4_R


Build: Jul 27 2022  (4907.470007)
Updater: 2.8000.00008
Bootloader: 02.014.002
Cab: 1.8001.0189
Mono: 6.12.0.107

IR FPGA: Version: 15
XIOSDK: 3.6.0
IoTSDK: 1.8.0
Build time: 20:29:15
Product ID: 0x7D2A
Revision ID: 0x0000
CPU ID: 0x0008
Ethernet Phy: Rev1
PUF: 3.015.0124
IOPVersion: FPGA [v15] slot:6
PYNG-HUB-SetupProgram: 3.015.0124
RFGwayVersion: ezsp version 06 stack id 02 stack version 6230
BACnetVersion: 15.1.29
Forced Auth Mode: True
FIPS Mode: True

Home Setup App is 1.0.25

```csharp
[ProgrammableOperation("ActionWithParameterNoDef")]
public void ActionWithParameterNoDef(
[Display("Time")]
[Min(1)]
[Max(10)]
int time)
{
CrestronConsole.PrintLine("ActionCalled {0}",time);

        }
        //Test Case 4
        [ProgrammableOperation("ActionWithParameterNoDef")]
        public void ActionWithParameterNoDef(
            [Display("Time")]
            [Min(1)]
            [Max(10)]
            int time)
        {
            CrestronConsole.PrintLine("ActionCalled {0}",time);
    
        }
        //Test Case 3
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
        // Test Case 2
        
        [ProgrammableOperation("ActionWithParameter")]
        public void ActionWithParameter(int time)
        {
            CrestronConsole.PrintLine("ActionCalled {0}",time);
    
        }
        
        // Test Case 1
        
        [ProgrammableOperation("ActionNoParameter")]
        public void ActionWithNoParameter()
        {
            CrestronConsole.PrintLine("ActionCalled ");
    
        }
```

Findings are as follows:

Test 1

Action with no Parameter can be added to a sequence and when "played" it's console command shows in the console.
Test 2
Action with Parameter ( and no Restrictions ) can be added to a sequence and when "played" it's console command shows in the console.

Test 3
Test 4
Actions with Parameters decorated with Max/Min etc.
When adding an action to a sequence the Parameter is not passed to the Sequence list correctly. The Default is shown.
On Calling this Action using the Play Button no Console is seen at the console and an error is seen in the logs


```err



1. Error: SimplSharpPro[App00] # 2022-08-30 07:25:39 # 
Exception:SendJoinEventDataForSmartObject - System.ArgumentException: No value supplied for required parameter 'time' and no prompt with a fixed value was found.
at Crestron.Tools.Runtime.Programming.Execution.Metho dExecutionItem.Crestron.Tools.Runtime.Programming.Execution.Interfaces.IExecution Item.Execute 
(System.Object target, System.Collections.Generic.IDictionary`2[TKey,TValue] parameterValues) [0x00116] in <521f88e03af041f38946bdc2568d1236>:0




```            
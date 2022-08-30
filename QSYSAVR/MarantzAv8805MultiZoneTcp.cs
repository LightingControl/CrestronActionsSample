// Copyright (C) 2018 to the present, Crestron Electronics, Inc.
// All rights reserved.
// No part of this software may be reproduced in any form, machine
// or natural, without the express written consent of Crestron Electronics.
// Use of this source code is subject to the terms of the Crestron Software License Agreement 
// under which you licensed this source code.

using Crestron.RAD.Common.Interfaces;
using Crestron.RAD.Common.Transports;
using Crestron.RAD.DeviceTypes.RADAVReceiver;


namespace Crestron.RAD.Drivers.AVReceivers
{
    public class MarantzAv8805Tcp : ABasicAVReceiver, ICloudConnected
    {
        public MarantzAv8805Tcp() { }
       

        public void Initialize()
        {
            var tcpTransport = new EmptyTransport();
            
                        
            ConnectionTransport = tcpTransport;
            
            ReceiverProtocol = new SoundUnitedAvrProtocol(ConnectionTransport, Id)
            {
                            EnableLogging = InternalEnableLogging,
                            CustomLogger = InternalCustomLogger
            };
            
            ReceiverProtocol.StateChange += StateChange;
            ReceiverProtocol.RxOut += SendRxOut;
            ReceiverProtocol.Initialize(AvrData);
        }
    }

    public class EmptyTransport : ATransportDriver
    {
        public override void SendMethod(string message, object[] paramaters)
        {
            //throw new System.NotImplementedException();
        }

        public override void Start()
        {
            //throw new System.NotImplementedException();
        }

        public override void Stop()
        {
            //throw new System.NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using TControls.Core;
using DemoProtocol;
using ExitGames.Client.Photon;

public class ReadySwitchRequest : RequestBase
{
    public void SendEvent()
    {
        NetworkService.instance.peer.OpCustom((byte)OperationCode.ReadySwitch, 
            new Dictionary<byte, object>(), true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        TLogger.DEBUG(operationResponse.DebugMessage);
    }
}


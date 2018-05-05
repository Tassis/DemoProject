using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using TControls.Core;
using DemoProtocol;

public class LoadingFinishRequest : RequestBase
{
    public void SendRequest()
    {
        NetworkService.instance.peer.OpCustom((byte)OperationCode.LoadingFinish, 
                                            new Dictionary<byte, object>(), true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
       
    }
}


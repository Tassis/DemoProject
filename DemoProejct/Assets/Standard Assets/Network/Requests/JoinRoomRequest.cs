using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using TControls.Core;
using DemoProtocol;

public class JoinRoomEventArgs : BaseRequestEventArgs
{}

public class JoinRoomRequest : RequestBase
{
    public static event EventHandler<JoinRoomEventArgs> JoinRoomEvent;

    public void SendRequest(string serial)
    {
        var parameter = new Dictionary<byte, object>()
        {
            { (byte)ParameterCode.Serial, serial },
        };

        NetworkService.instance.peer.OpCustom((byte)OperationCode.JoinRoom, parameter, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        JoinRoomEventArgs args = new JoinRoomEventArgs();

        args.returnCode = operationResponse.ReturnCode;
        args.debugMessage = operationResponse.DebugMessage;

        TLogger.DEBUG("OnJoinRoom");
        JoinRoomEvent(this, args);
    }
}


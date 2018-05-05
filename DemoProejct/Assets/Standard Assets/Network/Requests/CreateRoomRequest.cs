using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using DemoProtocol;


public class CreateRoomEventArgs : BaseRequestEventArgs
{ }

public class CreateRoomRequest : RequestBase
{
    public static EventHandler<CreateRoomEventArgs> CreateRoomEvent;

    public void SendRequest()
    {
        NetworkService.instance.peer.OpCustom((byte)OperationCode.CreateRoom,
                                              new Dictionary<byte, object>(), true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        CreateRoomEventArgs args = new CreateRoomEventArgs();

        args.returnCode = operationResponse.ReturnCode;
        args.debugMessage = operationResponse.DebugMessage;

        if (CreateRoomEvent != null)
            CreateRoomEvent(this, args);
    }
}

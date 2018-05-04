using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using DemoProtocol;


public class CreateRoomEventArgs : EventArgs
{
    public short returnCode { get; set; }
    public string debugMessage { get; set; }
}

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
        args.debugMessage = operationResponse.DebugMessage;

        if (operationResponse.ReturnCode == (short)ResultCode.Success)
            args.returnCode = (short)ResultCode.Success;
        else
            args.returnCode = (short)ResultCode.Failed;

        if (CreateRoomEvent != null)
            CreateRoomEvent(this, args);
    }
}

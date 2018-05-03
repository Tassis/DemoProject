using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using DemoProtocol;
using DemoProtocol.Tools;
using TControls.Core;

public class LoginEventArgs : EventArgs
{
    public short returnCode { get; set; }
    public string debugMessage { get; set; }
    public string username { get;  set; }

}

public class LoginRequest : RequestBase
{
    public static EventHandler<LoginEventArgs> LoginEvent;

    public void SendRequest()
    {
        NetworkService.instance.peer.OpCustom( (byte)OperationCode.Login, 
                                                new Dictionary<byte, object>(), true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        LoginEventArgs args = new LoginEventArgs();

        args.returnCode = operationResponse.ReturnCode;
        args.debugMessage = operationResponse.DebugMessage;

        if (operationResponse.ReturnCode == (byte)ResultCode.Success)
        {
            args.username = operationResponse.Parameters[(byte)ParameterCode.Username] as string;
        }

        if (LoginEvent != null)
            LoginEvent(this, args);

    }
}

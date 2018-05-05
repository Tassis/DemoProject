using System;
using ExitGames.Client.Photon;

public class BaseRequestEventArgs : EventArgs
{
    public short returnCode { get; set; }
    public string debugMessage { get; set; }
}

public abstract class RequestBase
{
    public abstract void OnOperationResponse(OperationResponse operationResponse);
    
}


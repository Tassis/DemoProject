using System;

public class ConnectEventArgs : EventArgs
{
    public bool isConnecting;
    public ConnectEventArgs(bool isConnecting)
    {
        this.isConnecting = isConnecting;
    }
}

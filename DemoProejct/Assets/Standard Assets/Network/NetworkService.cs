using System;
using ExitGames.Client.Photon;
using TControls.Core;

public class ConnectEventArgs : EventArgs
{
    public bool isConnecting;

    public ConnectEventArgs(bool isConnecting)
    {
        this.isConnecting = isConnecting;
    }
}

public class NetworkService : TSingleton<NetworkService>, IPhotonPeerListener
{
    public PhotonPeer peer;                             // Peer Channel
    public bool IsConnected { get; private set; }        // ConnectState
    public string _DebugMessage { get; private set; }    // DebugMessage

    public static event EventHandler<ConnectEventArgs> ConnectEvent;

    public NetworkService()
    {
        // Initialize Variable.
        peer = null;
        IsConnected = false;
    }

    public void Connect(string ipaddr, short port, string serverName)
    {
        try
        {
            string linkaddr = string.Format("{0}:{1}", ipaddr, port);
            this.peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            bool connected = this.peer.Connect(linkaddr, serverName);
            // try connect to server,  if not will call event.
            if (!connected)
            {
                if (ConnectEvent != null)
                    ConnectEvent(this, new ConnectEventArgs(false));
            }
            else
            {
                if (ConnectEvent != null)
                    ConnectEvent(this, new ConnectEventArgs(true));
            }


        }
        catch (Exception ex)
        {
            if (ConnectEvent != null)
             ConnectEvent(this, new ConnectEventArgs(false));
            throw ex;
        }
    }

    public void Disconnect()
    {
        try
        {
            if (this.peer != null)
                peer.Disconnect();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateService()
    {
        try
        {
            if (this.peer != null)
                this.peer.Service();
        }
        catch ( Exception ex)
        {
            throw ex;
        }
    }

  
    public void DebugReturn(DebugLevel level, string message)
    {
        TLogger.DEBUG(message);

    }

    public void OnEvent(EventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        NetworkRequest.instance.OnOperationResponse(operationResponse);
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                IsConnected = true;
                TLogger.DEBUG(IsConnected.ToString());
                break;
            case StatusCode.Disconnect:
            case StatusCode.DisconnectByServer:
            case StatusCode.DisconnectByServerLogic:
                IsConnected = false;
                break;
        }
    }

    public bool CheckPeerConnected()
    {
        return peer.PeerState == PeerStateValue.Connected;
    }
}

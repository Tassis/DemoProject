using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TControls.Core;
using DemoProtocol;

public class NetworkConnector : MonoBehaviour {

    public string serverIP;
    public short port;
    public string serverName = "";

    private void Awake()
    {
        NetworkService.ConnectEvent += onConnectEvent;
        
    }

    public void Connect()
    {
        NetworkService.instance.Connect(serverIP, port, serverName);
     
    }

    public void Login()
    {
        NetworkService.instance.peer.OpCustom((byte)OperationCode.Login, new Dictionary<byte, object>(), true);
    }

    private void onConnectEvent(object sender, ConnectEventArgs e)
    {
        TLogger.DEBUG(e.isConnecting.ToString());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TControls.Core;
using DemoProtocol;

public class NetworkConnector : MonoBehaviour {

    public string serverIP = "localhost";
    public short port = 4530;
    public string serverName = "Demo";

    private void Start()
    {   
        Connect();
    }

    public void Connect()
    {
        NetworkService.instance.Connect(serverIP, port, serverName);
    }
}

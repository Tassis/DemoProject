using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TControls.Core;
using System;

public class NetworkCallService : MonoBehaviour {

    private void Awake()
    {
        LoggerKit.Init();

        NetworkService.ConnectEvent += OnConnectEvent;


        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        NetworkService.instance.UpdateService();
    }


    private void OnApplicationQuit()
    {
        NetworkService.instance.Disconnect();
    }
    private void OnDestroy()
    {
        NetworkService.instance.Disconnect();
    }

    private void OnConnectEvent(object sender, ConnectEventArgs e)
    {
        if (e.isConnecting)
            TLogger.INFO("Peer is created.");
        else
            TLogger.ERROR("Peer create failed.");
    }
}

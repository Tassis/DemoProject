using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCallService : MonoBehaviour {

    private void Awake()
    {
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
}

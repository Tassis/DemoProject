using UnityEngine;
using UnityEngine.UI;
using DemoProtocol;
using TControls.Core;
using System;

public class LobbyFunction : MonoBehaviour {

    [SerializeField]    
    public Text accout;

    public string Serial { get; set; }

    private void Start()
    {
        accout.text = NetworkRecord.Username;
        CreateRoomRequest.CreateRoomEvent += OnCreateRoomEvent;
        JoinRoomRequest.JoinRoomEvent += OnJoinRoomEvent;
    }

    private void OnDestroy()
    {
        CreateRoomRequest.CreateRoomEvent -= OnCreateRoomEvent;
        JoinRoomRequest.JoinRoomEvent -= OnJoinRoomEvent;
    }

    public void CreateRoom()
    {
        CreateRoomRequest request = new CreateRoomRequest();
        request.SendRequest();

        CommonUtils.instance.SwitchViewMask(true);
    }

    public void JoinRoom()
    {
        JoinRoomRequest request = new JoinRoomRequest();
        request.SendRequest(this.Serial);

        CommonUtils.instance.SwitchViewMask(true);
    }


    private void OnCreateRoomEvent(object sender, CreateRoomEventArgs e)
    {
        CommonUtils.instance.SwitchViewMask(false);

        if (e.returnCode == (short)ResultCode.Success)
        {
            SceneHandler.instance.LoadScene("Room", false, false);
        }
        TLogger.DEBUG(e.debugMessage);
    }

    private void OnJoinRoomEvent(object sender, JoinRoomEventArgs e)
    {
        CommonUtils.instance.SwitchViewMask(false);

        TLogger.DEBUG("OnJoinRoomEvent.");

        if (e.returnCode == (short)ResultCode.Success)
        {
            SceneHandler.instance.LoadScene("Room", false, false);
        }
    }
}

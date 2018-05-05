using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoProtocol.Datas;

public class RoomFunction : MonoBehaviour {

    public ReadySwitch button;
    public Text SerialField;
    public List<RoomActorView> views;

    private Sprite[] buttonPics;

    private void Awake()
    {
        RoomUpdateHandler.RoomUpdateEvent += OnRoomUpdateEvent;
        GameStartHandler.GameStartEvent += OnGameStartEvent;
        UpdateRoomInfo();

    }

    public void ChangeReadyState()
    {
       var request = new ReadySwitchRequest();
        request.SendEvent();
    }

    private void UpdateRoomInfo()
    {
        RoomInfoData roomInfo = NetworkRecord.RoomInfoData;
        SerialField.text = roomInfo.serial;

        for (int i = 0; i < roomInfo.actorList.Count; i++)
        {
            // Update room's info.
            views[i].actorID.text = roomInfo.actorList[i].memberID;
            if (roomInfo.actorList[i].isReady)
                views[i].isReady.text = "Ready";
            else
                views[i].isReady.text = "";

            // Check ready state, update button.
            if (roomInfo.actorList[i].memberID == NetworkRecord.Username)
            {
                button.SetButtonState(roomInfo.actorList[i].isReady);
            }
        }
    }

    private void OnGameStartEvent(object sender, EventArgs e)
    {
        CommonUtils.instance.SwitchViewMask(true);
        SceneHandler.instance.LoadScene("Game", false, true);
    }

    private void OnRoomUpdateEvent(object sender, EventArgs e)
    {
        UpdateRoomInfo();
    }

    public void OnTimerStart(){}
    public void OnTimerUpdate(float second){}
    public void OnTimerEnd()
    {
        CommonUtils.instance.SwitchViewMask(false);
        SceneHandler.instance.LoadScene("Game", false, true);
    }
}

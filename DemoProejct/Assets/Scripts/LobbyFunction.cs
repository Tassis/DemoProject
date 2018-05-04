using UnityEngine;
using UnityEngine.UI;
using DemoProtocol;
using TControls.Core;

public class LobbyFunction : MonoBehaviour {

    [SerializeField]    
    public Text accout;

    private void Start()
    {
        accout.text = NetworkRecord.Username;
        CreateRoomRequest.CreateRoomEvent += OnCreateRoomEvent;
    }

    public void CreateRoom()
    {
        CreateRoomRequest request = new CreateRoomRequest();
        request.SendRequest();

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
}

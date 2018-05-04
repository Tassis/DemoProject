using UnityEngine;
using UnityEngine.UI;
using TControls.Core;
using DemoProtocol;
using System;

public class TitleCollection : MonoBehaviour{

    public Image mask;
    public GameObject loading;
    public GameObject text;

    public NetworkConnector connector;
    bool canClick = true;

    private void Awake()
    {
        LoginRequest.LoginEvent += OnLoginEvent;
    }

    public void TitleEnter()
    {
        if (NetworkService.instance.CheckPeerConnected() && canClick)
        {
            mask.GetComponent<Animator>().SetTrigger("click");
            loading.SetActive(true);
            text.SetActive(false);

            canClick = false;
            // Push loginRequest.
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.SendRequest();
        }
    }

    private void Update()
    {
        if (NetworkService.instance.CheckPeerConnected())
            text.SetActive(true);
    }

    private void OnLoginEvent(object sender, LoginEventArgs e)
    {
        if (e.returnCode == (short)ResultCode.Success)
        {
            TLogger.INFO("Login Success. username: " + e.username);

            SceneHandler.instance.LoadScene("Lobby", false, false);
            return;
        }
        string msg = string.Format("Login Failed, {0}", e.debugMessage);
        TLogger.ERROR(msg);

    }

}

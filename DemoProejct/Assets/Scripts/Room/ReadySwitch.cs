using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadySwitch : MonoBehaviour {

    public GameObject[] button;

    public void SetButtonState(bool isReady)
    {
        if (isReady)
        {
            button[0].SetActive(false);
            button[1].SetActive(true);
            return;
        }

        button[1].SetActive(false);
        button[0].SetActive(true);
    }
}

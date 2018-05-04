using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils : MonoBehaviour {

    public static CommonUtils instance;

    public GameObject viewMask;

    private void Awake()
    {
        instance = this;
    }

    public void SwitchViewMask(bool isOpen)
    {
        if (isOpen)
        {
            viewMask.SetActive(isOpen);
            viewMask.GetComponent<Animator>().SetTrigger("click");
        }
        else
        {
            viewMask.GetComponent<Animator>().SetTrigger("reset");
            viewMask.SetActive(isOpen);
        }         
    }

}

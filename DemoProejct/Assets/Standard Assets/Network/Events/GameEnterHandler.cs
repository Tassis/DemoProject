using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnterHandler : HandlerBase
{
    public static event EventHandler EnterGameEvent;

    public override void OnEvent(Dictionary<byte, object> parameter)
    {
        if (EnterGameEvent != null)
            EnterGameEvent(this, EventArgs.Empty);
          
    }
}

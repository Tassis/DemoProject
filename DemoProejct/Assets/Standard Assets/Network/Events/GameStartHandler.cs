using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameStartHandler : HandlerBase
{
    public static EventHandler GameStartEvent;

    public override void OnEvent(Dictionary<byte, object> parameter)
    {
        if (GameStartEvent != null)
            GameStartEvent(this, EventArgs.Empty);    
    }
}

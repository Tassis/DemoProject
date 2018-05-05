using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoProtocol;
using TControls.Core;

public class NetworkEvent
{
    private Dictionary<EventCode, HandlerBase> eventDic;

    public NetworkEvent()
    {
        eventDic = new Dictionary<EventCode, HandlerBase>();
        InitDict();
    }

    private void InitDict()
    {
        eventDic.Add(EventCode.RoomUpdate, new RoomUpdateHandler());
        eventDic.Add(EventCode.GameStart, new GameStartHandler());
        eventDic.Add(EventCode.GameEnter, new GameEnterHandler());
    }

    public void OnEvent(EventCode code, Dictionary<byte, object> parameter)
    {
        eventDic[code].OnEvent(parameter);
    }

}


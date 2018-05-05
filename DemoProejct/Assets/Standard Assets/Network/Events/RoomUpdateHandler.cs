using System;
using System.Collections.Generic;
using DemoProtocol;
using DemoProtocol.Tools;
using DemoProtocol.Datas;
using TControls.Core;

public class RoomUpdateEventArgs: EventArgs
{
     public RoomInfoData roomInfoData { get; set; }
}

public class RoomUpdateHandler : HandlerBase
{
    public static EventHandler RoomUpdateEvent;

    public override void OnEvent(Dictionary<byte, object> parameter)
    {
        // Create args.
        RoomUpdateEventArgs args = new RoomUpdateEventArgs();

        // Deserialize data.
        byte[] roomInfoDataByte = (byte[])parameter[(byte)ParameterCode.RoomInfoData];
        args.roomInfoData = (RoomInfoData)ProtoBufTool.Deserialize<RoomInfoData>(roomInfoDataByte);

        NetworkRecord.RoomInfoData = args.roomInfoData;

        if (RoomUpdateEvent != null)
            RoomUpdateEvent(this, args);   

    }
}

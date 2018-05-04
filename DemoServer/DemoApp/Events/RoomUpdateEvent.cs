using System;
using System.Collections.Generic;
using DemoApp.Actors;
using DemoApp.Cells;
using DemoProtocol;
using DemoProtocol.Datas;
using DemoProtocol.Tools;
using Photon.SocketServer;

namespace DemoApp.Events
{
    public class RoomUpdateEvent
    {
        public static void SendEvent(CellRoom room)
        {
            // Get actor list from room.
            List<CellRoom.ActorInfo> actorList = room.actorList;
            // conversion roomActorList
            List<RoomActorData> roomActorList = new List<RoomActorData>();
            for (int i = 0; i < actorList.Count; i++)
            {
                RoomActorData actorData = new RoomActorData();
                actorData.memberID = actorList[i].memberID;
                actorData.isReady = actorList[i].isReady;
                roomActorList.Add(actorData);
            }

            // Create transfer data
            RoomInfoData roomData = new RoomInfoData();
            roomData.serial = room.SerialNum;
            roomData.actorList = roomActorList;

            // Serialize roominfodata.
            byte[] roomInfoDataByte = ProtoBufTool.Serialize(roomData);

            // push data to actors
            for (int i = 0; i < actorList.Count; i++)
            {
                // get peer
                Actor actor = ServerApp.instance.actorManager.GetActorFromMemberID(actorList[i].memberID);
                ServerPeer peer = ServerApp.instance.actorManager.TryGetPeer(actor.guid);

                // generate event data.
                var eventDict = new Dictionary<byte, object>();
                eventDict.Add((byte)ParameterCode.RoomInfoData, roomInfoDataByte);

                EventData eventData = new EventData((byte)EventCode.RoomUpdateEvent);
                eventData.Parameters = eventDict;

                peer.SendEvent(eventData, new SendParameters());
                   
            }




        }
    }
}

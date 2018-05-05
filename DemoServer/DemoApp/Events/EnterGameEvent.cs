using System;
using System.Collections.Generic;
using DemoProtocol;
using DemoApp.Actors;
using DemoApp.Cells;
using Photon.SocketServer;

namespace DemoApp.Events
{
    public class EnterGameEvent
    {
        public static void SendEvent(CellRoom room)
        {
            EventData eventData = new EventData((byte)EventCode.GameEnter, 
                                                    new Dictionary<byte, object>());
            
            foreach (CellRoom.ActorInfo roomActor in room.actorList)
            {
                Actor actor = ServerApp.instance.actorManager.GetActorFromMemberID(roomActor.memberID);
                ServerPeer peer = ServerApp.instance.actorManager.TryGetPeer(actor.guid);

                peer.SendEvent(eventData, new SendParameters());

            }
        }
    }
}

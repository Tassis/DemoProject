using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoApp.Actors;
using DemoApp.Cells;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public class ReadySwitchHandler : BaseHandler
    {
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            // Get parameter
            Actor actor = ServerApp.instance.actorManager.GetActorFromGuid(peer.peerGuid);
            CellRoom room = ServerApp.instance.cellManager.TryGetRoomByIndex(actor.roomIndex);

            room.ChangeReady(peer.peerGuid);
        }
    }
}

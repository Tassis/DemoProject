using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer;
using DemoApp.Cells;

namespace DemoApp.Handlers
{
    public class LoadingFinishHandler : BaseHandler
    {
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            Actors.Actor actor = ServerApp.instance.actorManager.GetActorFromGuid(peer.peerGuid);
            CellRoom room = ServerApp.instance.cellManager.TryGetRoomByIndex(actor.roomIndex);

            room.AddFinishCounter();
        }
    }
}

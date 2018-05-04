using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoApp.Cells;
using DemoProtocol;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public class CreateRoomHandler : BaseHandler
    {
        public CreateRoomHandler()
        {
            opCode = DemoProtocol.OperationCode.CreateRoom;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            // Create response.
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            // Try to create room.
            Actors.Actor actor = ServerApp.instance.actorManager.GetActorFromGuid(peer.peerGuid);
            CellRoom room = ServerApp.instance.cellManager.CreateRoom(actor.memberID);
           
            if (room == null)
            {
                response.ReturnCode = (short)ResultCode.Failed;
                response.DebugMessage = "CreateRoom failed.";
            }
            else
            {
                ServerApp.Logger.InfoFormat("Acotr {0} create room index: {1}", actor.memberID, room.SerialNum);

                response.ReturnCode = (short)ResultCode.Success;
                response.DebugMessage = "CreateRoom Success.";
            }
             
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}

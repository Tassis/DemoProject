using System;
using System.Collections.Generic;
using DemoApp.Actors;
using DemoApp.Cells;
using DemoProtocol;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public class JoinRoomHandler : BaseHandler
    {
        public JoinRoomHandler()
        {
            opCode = OperationCode.JoinRoom;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {   
            // get parameter
            var serial = (string)operationRequest.Parameters[(byte)ParameterCode.Serial];
            Actor actor = ServerApp.instance.actorManager.GetActorFromGuid(peer.peerGuid);
            CellRoom room = ServerApp.instance.cellManager.TryGetRoomBySerial(serial);
            // create response.
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if (room == null)
            {
                response.ReturnCode = (short)ResultCode.Failed;
                response.DebugMessage = "The room was not exist.";
            }
            else
            {
                // try to join room.
                if (room.Join(actor.memberID))
                {
                    response.ReturnCode = (short)ResultCode.Success;
                    response.DebugMessage = "JoinRoom Success";
                    ServerApp.Logger.InfoFormat("Actor {0} JoinRoom serial: {1}", actor.memberID, serial);

                    peer.SendOperationResponse(response, sendParameters);
                }
                else
                {
                    response.ReturnCode = (short)ResultCode.Failed;
                    response.DebugMessage = "The room is full.";
                    peer.SendOperationResponse(response, sendParameters);
                }
            }
        }
    }
}

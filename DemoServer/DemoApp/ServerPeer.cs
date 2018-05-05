using System;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using DemoApp.Actors;
using DemoApp.Cells;
namespace DemoApp
{

    /// Client Proxy
    /// while client link to server will create a proxy.

    public class ServerPeer : ClientPeer
    {
        public Guid peerGuid { get;  private set; }
        public ServerApp _server;

        public ServerPeer(InitRequest initRequest , ServerApp serverApp) : base(initRequest)
        {
            peerGuid = Guid.NewGuid();
            _server = serverApp;
            _server.actorManager.AddConenectPeer(peerGuid, this);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Actor actor = _server.actorManager.GetActorFromGuid(peerGuid);

            ServerApp.Logger.InfoFormat("[ServerPeer] Actor {0} disconnect", actor.memberID);

            if (actor.roomIndex >= 0)
            {
                // actor in room , must remove from room's actor list.
                CellRoom room = ServerApp.instance.cellManager.TryGetRoomByIndex(actor.roomIndex);
                room.Quit(actor.memberID);
            }
            else if (actor.roomIndex == -1)
            {
                // actor in lobby, must remove from lobby's actor list.
                ServerApp.instance.cellManager.Lobby.Remove(actor.memberID);
            }

            _server.actorManager.ActorOffline(peerGuid);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            // transfer request to requestManager to process.
            _server.requestManager.OnOperationRequest(operationRequest, sendParameters, this);
            
           
        }
    }
}

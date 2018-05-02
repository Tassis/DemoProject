using System;
using System.Collections.Generic;
using ExitGames.Logging;
using ExitGames.Net;

using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

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
          //  _server.actorManager.AddConenectPeer(peerGuid, this);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            // transfer request to requestManager to process.
            _server.requestManager.OnOperationRequest(operationRequest, sendParameters, this);
            
           
        }
    }
}

using System;
using System.Collections.Generic;
using DemoProtocol;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            opCode = DemoProtocol.OperationCode.Login;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            OperationResponse response = new OperationResponse(operationRequest.OperationCode)
            {
                ReturnCode = (short)ResultCode.Success,
                DebugMessage = "Test Sign-in",
            };

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}

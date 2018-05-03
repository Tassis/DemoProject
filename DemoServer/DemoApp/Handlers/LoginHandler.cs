using System;
using System.Collections.Generic;
using DemoProtocol;
using DemoApp.Actors;
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
            ActorRetune actorRet = ServerApp.instance.actorManager.GuestOnline(peer.peerGuid);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            response.ReturnCode = -1;
            // Success
            if (actorRet.ReturnCode == 0) 
            {
                response.ReturnCode = (short)ResultCode.Success;
                response.DebugMessage = "Login Success.";
                var parameter = new Dictionary<byte, object>()
                {
                    { (byte)ParameterCode.Username, actorRet.actorData.memberID }
                };

                response.Parameters = parameter;

                ServerApp.instance.actorManager.AddConenectPeer(peer.peerGuid, peer);
            }

            // RepeatLogin
            if (actorRet.ReturnCode == 2)
            {
                response.ReturnCode = (short)ResultCode.CustomError;
                response.DebugMessage = "Your device repeat login.";

            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}

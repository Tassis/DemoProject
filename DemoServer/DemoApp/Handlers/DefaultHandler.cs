using System;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            opCode = DemoProtocol.OperationCode.Default;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}

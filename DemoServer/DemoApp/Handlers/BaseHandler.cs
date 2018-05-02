using DemoProtocol;
using Photon.SocketServer;

namespace DemoApp.Handlers
{
    public abstract class BaseHandler
    {
        public OperationCode opCode;

        public abstract void OnOperationRequest(
            OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer);
    }
}

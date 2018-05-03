using System;
using System.Collections.Generic;
using Photon.SocketServer;

using DemoProtocol;
using DemoProtocol.Tools;
using DemoApp.Handlers; 

namespace DemoApp
{
    public class RequestManager
    {
        public Dictionary<OperationCode, BaseHandler> requestDict { get; private set; }

        public RequestManager()
        {
            requestDict = new Dictionary<OperationCode, BaseHandler>();
            InitialzeDict();
            ServerApp.Logger.Info("RequestManager is inintialized");
        }

        private void InitialzeDict()
        {
            requestDict.Add(OperationCode.Default, new DefaultHandler());
            requestDict.Add(OperationCode.Login, new LoginHandler());

        }

        public void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            BaseHandler handler = DictTool.GetValue(requestDict, (OperationCode)operationRequest.OperationCode);

            if (handler != default(BaseHandler))
                handler.OnOperationRequest(operationRequest, sendParameters, peer);
            else
            {
                handler = DictTool.GetValue(requestDict, OperationCode.Default);
                handler.OnOperationRequest(operationRequest, sendParameters, peer);
            }
        }
        

    }
}

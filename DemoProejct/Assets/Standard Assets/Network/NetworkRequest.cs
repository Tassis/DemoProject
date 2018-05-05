using System.Collections.Generic;
using ExitGames.Client.Photon;
using DemoProtocol;
using DemoProtocol.Tools;
using TControls.Core;

public class NetworkRequest : TSingleton<NetworkRequest>
{
    public Dictionary<OperationCode, RequestBase> requestDic { get; private set; }

    public NetworkRequest()
    {
        requestDic = new Dictionary<OperationCode, RequestBase>();
        InitDict();
    }

    public void InitDict()
    {
        requestDic.Add(OperationCode.Login, new LoginRequest());
        requestDic.Add(OperationCode.CreateRoom, new CreateRoomRequest());
        requestDic.Add(OperationCode.JoinRoom, new JoinRoomRequest());
        requestDic.Add(OperationCode.ReadySwitch, new ReadySwitchRequest());
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        RequestBase request = null;

        if (requestDic.TryGetValue((OperationCode)operationResponse.OperationCode, out request))
            request.OnOperationResponse(operationResponse);

    }
}


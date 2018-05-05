﻿namespace DemoProtocol
{
    public enum OperationCode : byte
    {
        Default = 0,
        Login = 5,
        CreateRoom = 6,
        JoinRoom = 7,
        ReadySwitch = 8,
        LoadingFinish = 9,
    }
}

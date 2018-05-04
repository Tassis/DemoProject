using ProtoBuf;

namespace DemoProtocol.Datas
{
    [ProtoContract]
    public class RoomActorData
    {
        [ProtoMember(1)]
        public string memberID { get; set; }
        [ProtoMember(2)]
        public bool isReady { get; set; }

    }
}

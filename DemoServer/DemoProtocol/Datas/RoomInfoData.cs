using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace DemoProtocol.Datas
{
    [ProtoContract]
    public class RoomInfoData
    {
        [ProtoMember(1)]
        public string serial;
        [ProtoMember(2)]
        public List<RoomActorData> actorList;
    }
}

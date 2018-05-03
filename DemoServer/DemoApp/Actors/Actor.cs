using System;

namespace DemoApp.Actors
{
    public class Actor
    {
        public Guid guid { get; private set; }
        public string memberID { get; private set; }
        public short roomIndex { get; set; }
        public short status { get; set; }

        public Actor(Guid _guid , string _memberID)
        {
            this.guid = _guid;
            this.memberID = _memberID;
            this.roomIndex = -1;
            this.status = -1;

        }
         
    }
}

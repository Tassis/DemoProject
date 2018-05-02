using System;

namespace DemoApp.Actors
{
    public class Actor
    {
        public Guid guid { get; private set; }
        public string uniqueID { get; private set;}
        public string memberID { get; private set; }
        public short position { get; set; }

        public Actor(Guid _guid , string _memberID, string _uniqueID, string _nickname)
        {
            this.guid = _guid;
            this.uniqueID = _uniqueID;
            this.memberID = _memberID;
            this.position = -1;
        }
         
    }
}

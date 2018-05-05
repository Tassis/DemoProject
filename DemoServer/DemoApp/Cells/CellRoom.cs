using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DemoApp.Events;
using DemoApp.Actors;

namespace DemoApp.Cells
{
    public class CellRoom
    {

        public string SerialNum { get; private set; }
        public string HostName { get; private set; }
        public List<ActorInfo> actorList { get;  private set;}
        public int Limit { get; private set; }
        public bool isPlay { get; private set; }
        public int finishCount;

        public CellRoom(string serialNum, string hostName,int limit = 2)
        {
            this.SerialNum = serialNum;
            this.HostName = hostName;
            this.actorList = new List<ActorInfo>(limit);
            this.Limit = limit;
            this.isPlay = false;
            this.finishCount = 0;
        }

        public bool Join(string memberID)
        {
            lock(this)
            {
                if (actorList.Count >= Limit)
                {
                    return false;
                }

                for (int i = 0; i < actorList.Count; i++)
                {
                    // check repeat.
                    if (memberID == actorList[i].memberID)
                    {
                        return false;
                    }
                }
                // Check done, no problem addlist.
                ActorInfo actorInfo = new ActorInfo(memberID);
                actorList.Add(actorInfo);

                // Change actor's roomIndex.
                Actor actor = ServerApp.instance.actorManager.GetActorFromMemberID(memberID);
                actor.roomIndex = (short)ServerApp.instance.cellManager.GetCellIndex(this);
                // Update RoomInfo.
                CheckStatus();
                return true;
            }
        }
        public void Quit(string memberID)
        {
            for (int i = 0; i < actorList.Count; i++)
            {
                if (actorList[i].memberID == memberID)
                    actorList.RemoveAt(i);
            }

            CheckStatus();
        }

        public void ChangeReady(Guid guid)
        {
            Actor actor = ServerApp.instance.actorManager.GetActorFromGuid(guid);
            
            foreach (ActorInfo t in actorList)
            {
                if (t.memberID == actor.memberID)
                    t.isReady = !t.isReady;
            }

            CheckStatus();
        }
        private void CheckStatus()
        {
            if (actorList.Count == 0)
            {
                actorList.Clear();
                isPlay = false;

                ServerApp.Logger.InfoFormat("The room serial: {0} is close.", SerialNum);
                ServerApp.instance.cellManager.RemoveCell(this.SerialNum);
                return;
            }

            RoomUpdateEvent.SendEvent(this);

            if (actorList.Count == Limit)
            {
                foreach (ActorInfo t in actorList)
                {
                    if (!t.isReady)
                        return;
                }
                GameStartEvent.SendEvent(this);
            }
        }

        public void AddFinishCounter()
        {
            this.finishCount++;

            if (finishCount >= Limit)
            {
                EnterGameEvent.SendEvent(this);
            }
        }

        public class ActorInfo
        {
            public string memberID { get; private set; }
            public bool isReady { get; set; }

            public ActorInfo(string memberID)
            {
                this.memberID = memberID;
                this.isReady = false;
            }
        }
    }



}

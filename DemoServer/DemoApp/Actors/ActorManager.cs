using System;
using System.Collections.Generic;

namespace DemoApp.Actors
{
    public class ActorRetune
    {
        // -1: null  0:Success 2:RepeatLogin
        public short ReturnCode { get; set;}
        public string DebugMessage { get; set; }
        public Actor actorData { get; set; }
    }


    public class ActorManager
    {
        // Current link's peer dictionary.
        protected Dictionary<Guid, ServerPeer> ConnectedClients { get; set; }
        // Get memberID from grid.
        protected Dictionary<Guid, string> GuidMemberID { get; set; }
        // Get Actor from uniqueID.
        protected Dictionary<string, Actor> MemberIDActor { get; set; }

        public ActorManager()
        {
            this.ConnectedClients = new Dictionary<Guid, ServerPeer>();
            this.GuidMemberID = new Dictionary<Guid, string>();
            this.MemberIDActor = new Dictionary<string, Actor>();

            ServerApp.Logger.Info("ActorManager is inintialized");
        }

   
        // Peer's operation.
        public void AddConenectPeer( Guid guid, ServerPeer peer )
        {
            if (!ConnectedClients.ContainsKey(guid))
                ConnectedClients.Add(guid, peer);
        }

        // try get peer from guid.
        public ServerPeer TryGetPeer(Guid guid)
        {
            ServerPeer peer;
            ConnectedClients.TryGetValue(guid, out peer);
            return peer;
        }

        // Remove peer from guid.
        public void RemovePeer(Guid guid)
        {
            if (ConnectedClients.ContainsKey(guid))
                ConnectedClients.Remove(guid);
        }

        // ---------------------------
        // Get Actor Method. 
        // ---------------------------
        public Actor GetActorFromGuid(Guid guid)
        {
            if (!GuidMemberID.ContainsKey(guid))
            {
                ServerApp.Logger.Error("Not found actor by guid.");
                return null;
            }

            string memberID = GuidMemberID[guid];
            Actor actor = GetActorFromMemberID(memberID);
            return actor;
        }

        public Actor GetActorFromMemberID(string memberID)
        {
            if (!MemberIDActor.ContainsKey(memberID))
            {
                ServerApp.Logger.ErrorFormat("Not found any actor by memberID {0}", memberID);
                return null;
            }

            Actor actor = MemberIDActor[memberID];
            return actor;
        }

        // ---------------------------
        // Online / Offline operation.
        // ---------------------------
        public ActorRetune GuestOnline(Guid guid)
        {
            ActorRetune actorRtn = new ActorRetune();
            actorRtn.ReturnCode = -1;

            lock(this)
            {
                // Check guid isn't repeat.
                if (GuidMemberID.ContainsKey(guid))
                {
                    actorRtn.ReturnCode = 2;
                    actorRtn.DebugMessage = "Repeat login";
                    return actorRtn;
                }

                // if normal login, register dictionary and retune.
                Actor guestActor = CreateGuestProfile(guid);
                if (!GuidMemberID.ContainsKey(guid))
                    GuidMemberID.Add(guid, guestActor.memberID);

                if (!MemberIDActor.ContainsKey(guestActor.memberID))
                    MemberIDActor.Add(guestActor.memberID, guestActor);

                actorRtn.ReturnCode = 0;
                actorRtn.DebugMessage = "Login success.";
                actorRtn.actorData = guestActor;
            }

            return actorRtn;
        }

        public void ActorOffline(Guid guid)
        {
            lock(this)
            {
                RemovePeer(guid);
                string memberID = string.Empty;
                // Remove actorData from dictionay.
                if (GuidMemberID.ContainsKey(guid))
                {
                    memberID = GuidMemberID[guid];
                    GuidMemberID.Remove(guid);

                    if (MemberIDActor.ContainsKey(memberID))
                        MemberIDActor.Remove(memberID);

                }
            }
        }

        private Actor CreateGuestProfile(Guid guid)
        {
            // Random generator memberID.
            Random rand = new Random();
            string memberID = string.Empty;

            while (true)
            {
                memberID = "Profile" + (int)rand.Next(100, 999);
                if (!MemberIDActor.ContainsKey(memberID))
                    break;
            }

            Actor guest = new Actor(guid, memberID);

            GuidMemberID.Add(guid, memberID);
            MemberIDActor.Add(memberID, guest);

            return guest;
        }
    }
}

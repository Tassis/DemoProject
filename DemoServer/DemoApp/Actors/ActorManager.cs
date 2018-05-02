using System;
using System.Collections.Generic;

namespace DemoApp.Actors
{
    public class ActorManager
    {
        // Current link's peer dictionary.
        protected Dictionary<Guid, ServerPeer> ConnectedClients { get; set; }
        // Get uniqueID from grid.
        protected Dictionary<Guid, string> GridUniqueID { get; set; }
        // Get uniqueID from memberID.
        protected Dictionary<string, string> MemberIDUniqueID { get; set; }
        // Get Actor from uniqueID.
        protected Dictionary<string, Actor> UniqueIDActor { get; set; }

        public ActorManager()
        {
            this.ConnectedClients = new Dictionary<Guid, ServerPeer>();
            this.GridUniqueID = new Dictionary<Guid, string>();
            this.MemberIDUniqueID = new Dictionary<string, string>();
            this.UniqueIDActor = new Dictionary<string, Actor>();
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

        // ActorOffline
        public void ActorOffline(Guid guid)
        {

        }
    }
}

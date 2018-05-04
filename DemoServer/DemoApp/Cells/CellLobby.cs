using System.Collections.Generic;

namespace DemoApp.Cells
{
    public class CellLobby
    {
        public List<string> actorMebmerIDs { get; private set; }

        public CellLobby()
        {
            actorMebmerIDs = new List<string>();
        }

        public void Enter(string memberID)
        {
            ServerApp.Logger.InfoFormat("Actor {0} enter lobby.", memberID);
            actorMebmerIDs.Add(memberID);
        }

        public void Remove(string memberID)
        {
            ServerApp.Logger.InfoFormat("Actor {0} exit lobby.", memberID);
            actorMebmerIDs.Remove(memberID);
        }

        public void Clear()
        {
            actorMebmerIDs.Clear();
        }

    }
}

using System;
using System.Collections.Generic;

namespace DemoApp.Cells
{
    public class CellManager
    {
        public CellLobby Lobby { get; private set; }
        
        public List<CellRoom> RoomList { get; private set; } 
        public Dictionary<string, CellRoom> RoomMap { get; private set; }

        
        public CellManager()
        {

            Lobby = new CellLobby();
            RoomList = new List<CellRoom>();
            RoomMap = new Dictionary<string, CellRoom>();
            ServerApp.Logger.Info("CellManager is inintialized");
            
        }


        public CellRoom GetRoom(int cellIndex)
        {
            return RoomList[cellIndex];
        }

        public int GetCellIndex(CellRoom cell)
        {
            return RoomList.IndexOf(cell);
        }

        public CellRoom CreateRoom(string memberID)
        {
            lock(this)
            {
                string serial = GetUniqueSerial();

                CellRoom room = new CellRoom(serial , memberID);
                if (RegisterCell(serial, room))
                {
                    room.Join(memberID);
                    return room;
                }

                return null;
            }
        }

        private string GetUniqueSerial()
        {
            string serial = string.Empty;
            Random rand = new Random();

            for (int i = 0; i < 9; i++)
            {
                serial = serial + rand.Next(0, 10);
            }

            if (RoomMap.ContainsKey(serial))
                serial = GetUniqueSerial();

            return serial;
        }

        public bool RegisterCell(string serial, CellRoom cell)
        {
            if (RoomMap.ContainsKey(serial))
            {
                string message = string.Format("Cell {0} is already exist", serial);
                ServerApp.Logger.Error(message);
                return false;
            }
            RoomList.Add(cell);
            RoomMap[serial] = cell;
            return true;
        }

        public void RemoveCell(string serial)
        {
            if (!RoomMap.ContainsKey(serial))
            {
                string message = string.Format("Cell {0} is not exist in map", serial);
                ServerApp.Logger.Error(message);
                return;
            }

            RoomMap.Remove(serial);
        }
    }
}

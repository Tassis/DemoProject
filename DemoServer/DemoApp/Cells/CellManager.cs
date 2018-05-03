using System.Collections.Generic;

namespace DemoApp.Cells
{
    public class CellManager
    {
        
        public Dictionary<string, CellBase> cellMap { get; private set; }
        
        public CellManager()
        {
            cellMap = new Dictionary<string, CellBase>();
            ServerApp.Logger.Info("CellManager is inintialized");
        }

        public void RegisterCell(string name, CellBase cell)
        {
            if (cellMap.ContainsKey(name))
            {
                string message = string.Format("Cell {0} is already exist", name);
                ServerApp.Logger.Error(message);
                return;
            }
            cellMap[name] = cell;
        }

        public void RemoveCell(string name)
        {
            if (!cellMap.ContainsKey(name))
            {
                string message = string.Format("Cell {0} is not exist in map", name);
                ServerApp.Logger.Error(message);
                return;
            }

            cellMap.Remove(name);
        }
    }
}

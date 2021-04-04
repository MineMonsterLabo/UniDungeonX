using System;

namespace UniDungeonX.Models
{
    public class GridMap
    {
        private readonly CellKind[,] cellKinds;

        public GridMap(CellKind[,] cellKinds)
        {
            this.cellKinds = cellKinds;
        }

        public void Put(Action<Position, CellKind> func)
        {
            for (int x = 0; x < cellKinds.GetLength(0); x++)
                for (int y = 0; y < cellKinds.GetLength(1); y++)
                    func(new Position(x, y), cellKinds[x, y]);
        }
    }
}

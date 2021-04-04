using UniDungeonX.Models;

namespace UniDungeonX.Generators
{
    class GridMapGenerator
    {
        public static GridMap Generate(Structure structure)
        {
            var cellKinds = new CellKind[structure.width, structure.height];

            foreach (var room in structure.rooms)
                PutRoom(cellKinds, room);

            return new GridMap(cellKinds);
        }

        private static void PutRoom(CellKind[,] cellKinds, Rectangle room)
        {
            var leftDownPos = room.leftDownPos;
            var width = room.width;
            var height = room.height;

            for (var x = 0; x < width; x++)
            {
                PutCell(cellKinds, x, height, leftDownPos, CellKind.UpWall);
                PutCell(cellKinds, x, height + 1, leftDownPos, CellKind.UpEdgeWall);
                PutCell(cellKinds, x, -1, leftDownPos, CellKind.DownEdgeWall);
            }
            for (var y = 0; y < height + 1; y++)
            {
                PutCell(cellKinds, -1, y, leftDownPos, CellKind.LeftEdgeWall);
                PutCell(cellKinds, width, y, leftDownPos, CellKind.RightEdgeWall);
            }

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    PutCell(cellKinds, x, y, leftDownPos, CellKind.Ground);
                }
            }
        }

        private static void PutCell(CellKind[,] cellKinds, int x, int y, Position leftDownPos, CellKind cellKind)
        {
            cellKinds[x + 1 + leftDownPos.x, y + 1 + leftDownPos.y] = cellKind;
        }
    }
}

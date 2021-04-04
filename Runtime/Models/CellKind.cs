using System;

namespace UniDungeonX.Models
{
    [Flags]
    public enum CellKind
    {
        Empty = 0,
        Ground = 1,
        LeftEdgeWall = 1 << 1,
        RightEdgeWall = 1 << 2,
        DownEdgeWall = 1 << 3,
        UpEdgeWall = 1 << 4,
        UpWall = 1 << 5
    }
}

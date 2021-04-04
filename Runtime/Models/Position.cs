namespace UniDungeonX.Models
{
    public struct Position
    {
        public readonly int x;
        public readonly int y;

        public static Position zero => new Position(0, 0);

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.x + b.x, a.y + b.y);
        }
    }
}

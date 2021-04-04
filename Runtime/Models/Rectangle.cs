namespace UniDungeonX.Models
{
    class Rectangle
    {
        public readonly int width;
        public readonly int height;
        public int Left => leftDownPos.x;
        public int Right => leftDownPos.x + width - 1;
        public int Down => leftDownPos.y;
        public int Up => leftDownPos.y + height - 1;

        public int SquareMeasure => width * height;

        public Position leftUpPos => new Position(Left, Up);
        public Position rightUpPos => new Position(Right, Up);
        public Position rightDownPos => new Position(Right, Down);
        public readonly Position leftDownPos;

        public Rectangle(int width, int height, Position leftDownPos)
        {
            this.width = width;
            this.height = height;
            this.leftDownPos = leftDownPos;
        }
    }
}

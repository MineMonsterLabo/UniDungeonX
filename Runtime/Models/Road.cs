
namespace UniDungeonX.Models
{
    class Road
    {
        public readonly int length;
        public readonly Position leftDownPos;
        public readonly bool isVertical;

        public Road(int length, Position leftDownPos, bool isVertical)
        {
            this.length = length;
            this.leftDownPos = leftDownPos;
            this.isVertical = isVertical;
        }
    }
}

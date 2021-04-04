namespace UniDungeonX.Models
{
    class Section
    {
        //区画の外周の矩形情報データ
        public readonly Rectangle outer;
        //区画にある部屋の矩形データ
        public readonly Rectangle room;

        public Section(Rectangle outer, Rectangle room)
        {
            this.outer = outer;
            this.room = room;
        }
    }
}

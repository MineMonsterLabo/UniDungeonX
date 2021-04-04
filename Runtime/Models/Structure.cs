using System.Collections.Generic;

namespace UniDungeonX.Models
{
    class Structure
    {
        public readonly IEnumerable<Rectangle> rooms;
        public readonly IEnumerable<Road> roads;
        public readonly int width;
        public readonly int height;

        public Structure(
            IEnumerable<Rectangle> rooms,
            IEnumerable<Road> roads,
            int width, int height
        )
        {
            this.rooms = rooms;
            this.roads = roads;
            this.width = width;
            this.height = height;
        }
    }
}

using System.Linq;
using UniDungeonX.Models;

namespace UniDungeonX.Generators
{
    static class StructureGenerator
    {
        public static Structure Generate(int width, int height)
        {
            var sectionDatas =
                SectionGenerator
                    .Generate(new DividedRectangleGenerator().Divide(width, height, 15));

            var roads = new RoadGenerator().Generate(sectionDatas);
            var rooms = sectionDatas.Select(x => x.room);
            return new Structure(rooms, roads, width, height);
        }
    }
}
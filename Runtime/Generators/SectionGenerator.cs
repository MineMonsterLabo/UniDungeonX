using System.Collections.Generic;
using System.Linq;
using UniDungeonX.Models;

namespace UniDungeonX.Generators
{
    class SectionGenerator
    {
        public static IEnumerable<Section> Generate(IEnumerable<Rectangle> outers)
        {
            return outers.Select(outer =>
            {
                const int minus = 3;
                var room =
                    new Rectangle(
                        outer.width - minus * 2,
                        outer.height - minus * 2,
                        outer.leftDownPos + new Position(minus, minus)
                    );
                return new Section(outer, room);
            });
        }
    }
}

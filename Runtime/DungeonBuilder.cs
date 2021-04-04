using UniDungeonX.Models;
using UniDungeonX.Generators;

namespace UniDungeonX
{
    public class DungeonBuilder
    {
        public DungeonBuilder() { }

        public GridMap Build(int width,int height)
        {
            var structure = StructureGenerator.Generate(width, height);
            return GridMapGenerator.Generate(structure);
        }
    }
}

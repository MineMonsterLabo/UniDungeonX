using System;
using System.Collections.Generic;
using System.Linq;
using UniDungeonX.Models;

namespace UniDungeonX.Generators
{
    class DividedRectangleGenerator
    {
        private readonly Random random;

        public DividedRectangleGenerator()
        {
            random = new Random();
        }

        //与えられたwidthとheightの矩形をminSizeの大きさになるまで分割する
        public IEnumerable<Rectangle> Divide(int width, int height, int minSize)
        {
            var room = new Rectangle(width, height, Position.zero);
            return RecursiveDivide(room, minSize, RandomBool());
        }

        private IEnumerable<Rectangle> RecursiveDivide(Rectangle room, int minSize, bool isVertical)
        {
            if (minSize * 2 > (isVertical ? room.width : room.height))
            {
                return new Rectangle[] { room };
            }
            var pair = DivideHelper(room, isVertical, minSize);

            if (pair.Item1.SquareMeasure > pair.Item2.SquareMeasure)
            {
                Swap(ref pair.Item1, ref pair.Item2);
            }

            return
                RecursiveDivide(pair.Item1, minSize, pair.Item1.height < pair.Item1.width)
                .Concat(RecursiveDivide(pair.Item2, minSize, pair.Item2.height < pair.Item2.width));
        }

        private (Rectangle, Rectangle) DivideHelper(Rectangle dungeonSquareData, bool isVertical, int minSize)
        {
            var offset = random.Next(minSize, (isVertical ? dungeonSquareData.width : dungeonSquareData.height) - minSize);
            var first =
              new Rectangle
              (
                  isVertical ? dungeonSquareData.width - offset : dungeonSquareData.width,
                  !isVertical ? dungeonSquareData.height - offset : dungeonSquareData.height,
                  dungeonSquareData.leftDownPos
              );
            var second =
                new Rectangle
                (
                   isVertical ? offset : dungeonSquareData.width,
                   !isVertical ? offset : dungeonSquareData.height,
                   first.leftDownPos + (isVertical ? new Position(first.width, 0) : new Position(0, first.height))
                );
            return (first, second);
        }

        private bool RandomBool()
        {
            return random.Next(0, 2) == 0;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}

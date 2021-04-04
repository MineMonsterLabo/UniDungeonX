using System;
using System.Collections.Generic;
using System.Linq;
using UniDungeonX.Models;

namespace UniDungeonX.Generators
{
    class RoadGenerator
    {
        const int SECTION_DISTANCE = 1;

        private readonly List<Road> roadList = new List<Road>();

        private readonly Random random;

        public RoadGenerator()
        {
            random = new Random();
        }

        public IEnumerable<Road> Generate(IEnumerable<Section> sectionList)
        {
            roadList.Clear();
            var SectionGroupList = sectionList.Select(section => new List<Section>() { section }.AsEnumerable()).ToList();

            while (SectionGroupList.Count > 1)
            {
                MergeOne(SectionGroupList);
            }

            return roadList;
        }

        private void AddRoad(Section first, Section second)
        {
            var roadFirst = CreateRoad(first, second);
            var roadSecond = CreateRoad(second, first);
            roadList.Add(roadFirst);
            roadList.Add(roadSecond);
            roadList.Add(CreateBridgeRoad(roadFirst, roadSecond));
        }

        private void MergeOne(List<IEnumerable<Section>> linkedSectionListList)
        {
            foreach (var linkedSectionList1 in linkedSectionListList)
            {
                foreach (var linkedSectionList2 in linkedSectionListList)
                {
                    if (linkedSectionList1 != linkedSectionList2)
                    {
                        if (Merge(linkedSectionList1, linkedSectionList2, out IEnumerable<Section> mergedLinkedSectionList))
                        {
                            linkedSectionListList.Remove(linkedSectionList1);
                            linkedSectionListList.Remove(linkedSectionList2);
                            linkedSectionListList.Add(mergedLinkedSectionList);
                            return;
                        }
                    }
                }
            }
        }

        private bool Merge
            (IEnumerable<Section> linkedSectionList1, IEnumerable<Section> linkedSectionList2, out IEnumerable<Section> mergedLinkedSectionList)
        {
            foreach (var section1 in linkedSectionList1)
            {
                var matchedSection = SearchSection(section1, linkedSectionList2);
                if (matchedSection != null)
                {
                    AddRoad(section1, matchedSection);
                    mergedLinkedSectionList = linkedSectionList1.Concat(linkedSectionList2);
                    return true;
                }
            }
            mergedLinkedSectionList = default;
            return false;
        }

        private static Section SearchSection(
            Section targetSection,
            IEnumerable<Section> sectionList
        )
        {
            return sectionList.Where(section => CheckMissing(targetSection, section)).FirstOrDefault();
        }

        private static bool CheckMissing(Section from, Section to)
        {
            return GetDistance(from, to) <= SECTION_DISTANCE;
        }

        private static int GetDistance(Section from, Section to)
        {
            //下に面している
            if (from.outer.Down >= to.outer.Up)
                return from.outer.Down - to.outer.Up;

            //上に面している
            if (from.outer.Up <= to.outer.Down)
                return to.outer.Down - from.outer.Up;

            //左に面している
            if (from.outer.Left >= to.outer.Right)
                return from.outer.Left - to.outer.Right;

            //右に面している
            return to.outer.Left - from.outer.Right;
        }

        //ある部屋からある部屋へと向かう向きに道をまっすぐ伸ばす
        private  Road CreateRoad(Section from, Section to)
        {
            //下に面している
            if (from.outer.Down >= to.outer.Up)
            {
                var length = (from.outer.height - from.room.height) / 2;
                return new Road
                (
                    length,
                    new Position(from.room.Left + random.Next(0, from.room.width), from.room.Down - length),
                    true
                );
            }
            //上に面している
            if (from.outer.Up <= to.outer.Down)
            {
                return new Road
                (
                  (from.outer.height - from.room.height) / 2,
                        new Position(from.room.Left + random.Next(0, from.room.width), from.room.Up + 1),
                     true
                );
            }
            //左に面している
            if (from.outer.Left >= to.outer.Right)
            {
                var length = (from.outer.width - from.room.width) / 2;
                return new Road
                (
                    length,
                    new Position(from.room.Left - length, from.room.Down + random.Next(0, from.room.height)),
                    false
                );
            }
            //右に面している
            return new Road
            (
                (from.outer.width - from.room.width) / 2,
                new Position(from.room.Right + 1, from.room.Down + random.Next(0, from.room.height)),
                false
            );
        }

        //道と道を繋いだ道を作る
        private static Road CreateBridgeRoad(Road from, Road to)
        {
            int length;
            Position leftDownPos;
            if (from.isVertical)
            {
                length =
                    Math.Abs(from.leftDownPos.x - to.leftDownPos.x) + 1;
                if (from.leftDownPos.y > to.leftDownPos.y)
                {
                    if (from.leftDownPos.x >= to.leftDownPos.x)
                    {
                        leftDownPos = new Position(to.leftDownPos.x, from.leftDownPos.y);
                    }
                    else
                    {
                        leftDownPos = new Position(from.leftDownPos.x, from.leftDownPos.y);
                    }
                }
                else
                {
                    if (to.leftDownPos.x >= from.leftDownPos.x)
                    {
                        leftDownPos = new Position(from.leftDownPos.x, to.leftDownPos.y);
                    }
                    else
                    {
                        leftDownPos = new Position(to.leftDownPos.x, to.leftDownPos.y);
                    }
                }
            }
            else
            {
                length =
                    Math.Abs(from.leftDownPos.y - to.leftDownPos.y) + 1;
                if (from.leftDownPos.x > to.leftDownPos.x)
                {
                    if (from.leftDownPos.y >= to.leftDownPos.y)
                    {
                        leftDownPos = new Position(from.leftDownPos.x, to.leftDownPos.y);
                    }
                    else
                    {
                        leftDownPos = new Position(from.leftDownPos.x, from.leftDownPos.y);
                    }
                }
                else
                {
                    if (to.leftDownPos.y >= from.leftDownPos.y)
                    {
                        leftDownPos = new Position(to.leftDownPos.x, from.leftDownPos.y);
                    }
                    else
                    {
                        leftDownPos = new Position(to.leftDownPos.x, to.leftDownPos.y);
                    }
                }
            }

            return new Road
            (
             length,
             leftDownPos,
             !from.isVertical
            );
        }
    }
}

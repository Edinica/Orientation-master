using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class Floor : Graph
    {
        public List<VertexWall> walls;
        public List<VertexRoom> rooms;
        public List<VertexChain> chains;

        public int Level;
        // запретить бы СЕТ снаружи

        // можно хэшировать количество этажей и т.п

        public Floor()
        {
            walls = new List<VertexWall>();
            rooms = new List<VertexRoom>();
            chains = new List<VertexChain>();
        }

        public Floor(int level) : this()
        {

            Level = level;

        }
        public bool uslovie(VertexWall first, VertexWall second, int X, int Y)
        {
            if
                (
                (((first.Point.X < X && X < second.Point.X) ||
                (second.Point.X < X && X < first.Point.X)) ||
                ((first.Point.Y < Y && Y < second.Point.Y) ||
                (second.Point.Y < Y && Y < first.Point.Y)))
                ) return true;
            else return false;
        }

    }
}

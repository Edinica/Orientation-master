using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class Floor
    {
        public List<VertexWall> walls;
        public List<VertexRoom> rooms;
        public List<VertexChain> chains;

        public int Level;
        // запретить бы СЕТ снаружи

        // можно хэшировать количество этажей и т.п

        public Floor()
        {
            walls  = new List<VertexWall>();
            rooms  = new List<VertexRoom>();
            chains = new List<VertexChain>();
        }

        public Floor(int level) : this (){

            Level = level;
           
        }

    }
}

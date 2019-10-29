using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class Floor
    {
        public int Number;

        

        public List<VertexWall> walls = new List<VertexWall>();
        public List<VertexRoom> rooms = new List<VertexRoom>();
        public List<VertexChain> chains = new List<VertexChain>();



    }
}

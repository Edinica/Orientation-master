using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace test1
{
    public interface IVertex
    {
        string Name { get; }
        Point3D Point { get; set; }
        List<GraphEdge> Edges { get; set; }

        void ChangePoint(int X = 0, int Y = 0, int Z = 0);
        void AddEdge(GraphEdge newEdge);
        void AddEdge(IVertex second, int edgeWeight);
        string ToString();

    }
}

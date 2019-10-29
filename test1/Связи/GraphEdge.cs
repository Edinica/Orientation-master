using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Перввая вершина
        /// </summary>
        public IVertex FurstVertex { get; set; }

        /// <summary>
        /// Вторая вершина
        /// </summary>
        public IVertex SecondVertex { get; set; }

        /// <summary>
        /// Вес ребра
        /// </summary>
        public int EdgeWeight { get; }

        private static int Count;
        private int Hash;
        //-----------------------
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="first">Связанная вершина</param>
        /// <param name="second">Связанная вершина</param>
        /// <param name="weight">Вес ребра</param>
        public GraphEdge(IVertex first, IVertex second, int weight)
        {

            FurstVertex = first;
            SecondVertex = second;

            EdgeWeight = weight;

            Count++;
            Hash = Count;

            first .AddEdge(this);
            second.AddEdge(this);
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override string ToString()
        {
            return FurstVertex.Name + " " + SecondVertex.Name + " " + EdgeWeight; ;
        }
    }
}

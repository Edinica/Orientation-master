using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace test1
{
    /// <summary>
    /// Вершина для связи аудиторий в корридорах
    /// </summary>
    public class VertexChain : IVertex
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Координаты в пространстве
        /// </summary>
        public Point3D Point { get; set; }

        /// <summary>
        /// Список ребер
        /// </summary>
        public List<GraphEdge> Edges { get; set; }

        //-----------------------
        /// <summary>
        /// Конструктор
        /// </summary>
        public VertexChain()
        {
            Edges = new List<GraphEdge>();
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="X">Координата Х вершины</param>
        /// <param name="Y">Координата У вершины</param>
        /// <param name="Z">Координата Z вершины</param>
        public VertexChain(string vertexName, int X, int Y, int Z)
        {
            Name = vertexName;
            Point = new Point3D(X, Y, Z);
            Edges = new List<GraphEdge>();
        }

        /// <summary>
        /// Изменить координаты вершины
        /// </summary>
        /// <param name="X">Координата Х вершины</param>
        /// <param name="Y">Координата У вершины</param>
        /// <param name="Z">Координата Z вершины</param>
        public void ChangePoint(int X = 0, int Y = 0, int Z = 0)
        {
            Point = new Point3D(X, Y, Z);
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="edgeWeight">Вес</param>
        public void AddEdge(IVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(this, vertex, edgeWeight));
        }
        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
    }
}

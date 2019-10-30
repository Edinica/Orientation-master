using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Media3D;
using System.IO;
using System;

namespace test1
{
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph
    {
        //а/вы/а/ыва/ыв/а/ыв
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<IVertex> Vertices { get; set; }//устарело

        // где каждый 1й лист это этаж а 2й уже позиция точки на этаже
        public List<List<VertexWall>>  walls  = new List<List<VertexWall>>();
        public List<List<VertexRoom>>  rooms  = new List<List<VertexRoom>>();
        public List<List<VertexChain>> chains = new List<List<VertexChain>>();
        //-----------------------

        /// <summary>
        /// Конструктор
        /// </summary>
        public Graph()
        {
            walls.Add(new List<VertexWall>());
            rooms.Add(new List<VertexRoom>());
            chains.Add(new List<VertexChain>());
        }

        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="X">Координата Х вершины</param>
        /// <param name="Y">Координата У вершины</param
        /// <param name="Z">Координата У вершины</param>
        public void AddVertexChain(string vertexName, int X, int Y, int Z)
        {
            chains[Z].Add(new VertexChain(vertexName, X, Y, Z));
        }
        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="X">Координата Х вершины</param>
        /// <param name="Y">Координата У вершины</param
        /// <param name="Z">Координата У вершины</param>
        public void AddVertexRoom(string vertexName, int X, int Y, int Z)
        {
            rooms[Z].Add(new VertexRoom(vertexName, X, Y, Z));
        }
        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="X">Координата Х вершины</param>
        /// <param name="Y">Координата У вершины</param
        /// <param name="Z">Координата У вершины</param>
        public void AddVertexWalls(string vertexName, int X, int Y, int floor)
        {
            walls[floor].Add(new VertexWall(vertexName, X, Y, floor));
        }

        /// <summary>
        /// Поиск вершины
        /// </summary>
        /// <param name="range">Координаты новой точки</param>
        /// <returns>Найденная вершина</returns>                     
        public IVertex FindVertex(Point range, int floor, string what)
        {
            if (what == "wall")
            {
                foreach (var v in walls[floor])
                {
                    if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                              (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                        return v; }
                return null;
            }
            else if (what == "room")
            {
                foreach (var v in rooms[floor])
                {
                    if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                              (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                        return v; }
                return null;
            }
            else
            {
                foreach (var v in chains[floor])
                {
                    if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                              (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                        return v; }
                return null; }
        }
        /// <summary>
        /// Поиск стены
        /// </summary>
        /// <param name="range">Координаты новой точки</param>
        /// <returns>Найденная вершина</returns>                     
        public IVertex FindWall(Point range, int floor)
        {
            // если точка принадлежит прямой 
            // образованной двумя точками
            // 
            foreach (var v in walls[floor])
            {
                if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                          (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                    return v;
            }


            return null;
        }

        /// <summary>
        /// Принадлежит ли точка линиям с погрешность 4x4
        /// </summary>
        public bool On_line(Point3D first, Point3D second, Point prover, out int x, out int k, out int m)
        {
            k = 0; m = 0;
            x = (int)Math.Abs((prover.Y - 4) * (second.X - first.X) +
                           +(prover.X - 4) * (first.Y - second.Y) +
                           +first.X * second.Y - second.X * first.Y);//берется минимальная точка для сравнения
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    /*int z = (int)Math.Abs((prover.Y + j) * (second.X - first.X) + (prover.X + i) * (first.Y - second.Y) + first.X * second.Y - second.X * first.Y);
                    if (/*(prover.X+i)*(second.Y-first.Y)-first.X*(second.Y-first.Y)==
						(prover.Y+j)*(second.X-first.X)-first.Y*(second.X-first.X)

                        /*first.X*first.Y-2*second.X*first.Y+
						+second.X*second.Y z <= 50) { x = z; return true; }*/
                    int y = (int)Math.Abs((prover.Y + j) * (second.X - first.X) + (prover.X + i) * (first.Y - second.Y) + first.X * second.Y - second.X * first.Y);
                    if (y < x)
                    { x = y; k = i; m = j; }
                }
            if (x <= 150) return true;
            else
                return false;
            /*(first.Y - second.Y) * prover.X+i + (second.X - first.X) * prover.Y+j
					+ ((first.X * second.Y) - (first.Y * second.X)) == 0*/

        }
        //-----------------------
        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstnum">Номер первой вершины</param>
        /// <param name="secondnum">Номер второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(int firstnum, int secondnum, int weight)
        {
            if (Vertices[firstnum] != null && Vertices[secondnum] != null)
            {
                GraphEdge NewOne = new GraphEdge(Vertices[firstnum], Vertices[secondnum], weight);
                // Vertices[firstnum].AddEdge(Vertices[secondnum], weight);
                // Vertices[secondnum].AddEdge(Vertices[firstnum], weight);
            }
        }
        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstnum"> Первая вершина</param>
        /// <param name="secondnum">Вторая вершина</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(IVertex first, IVertex second, int weight)
        {
            if (first.GetType() == typeof(VertexWall))
                if (first != null && second != null)
                {
                    GraphEdge NewOne = new GraphEdge(first, second, weight);
                    //first.AddEdge(second, weight);
                    //second.AddEdge(first, weight);
                }
        }
        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstnum">Первая вершина</param>
        /// <param name="secondnum">Номер второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(IVertex first, int secondnum, int floor, int weight)
        {
            if (first.GetType() == typeof(VertexWall))
                if (first != null && walls[floor][secondnum] != null)
                {
                    GraphEdge NewOne = new GraphEdge(first, walls[floor][secondnum], weight);
                    //first.AddEdge(walls[floor][secondnum], weight);
                    //walls[floor][secondnum].AddEdge(first, weight);
                }
        }
        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstnum">Номер первой вершины</param>
        /// <param name="secondnum">Вторая вершина</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>                                        
        public void AddEdge(int firstnum, IVertex second, int weight)
        {                                                           
            if (second.GetType() == typeof(VertexWall))
                if (second != null && Vertices[firstnum] != null)
                {
                    GraphEdge NewOne = new GraphEdge(second, Vertices[firstnum], weight);
                    // second.AddEdge(Vertices[firstnum], weight);
                    // Vertices[firstnum].AddEdge(second, weight);
                }
        }
        //-----------------------

        /// <summary>
        /// Объеденить вершины
        /// </summary>
        /// <param name="firstnum"> Первая вершина</param>
        /// <param name="secondnum">Вторая вершина</param>
        public void CompleteVertex(IVertex first, IVertex second)
        {
            IVertex temp;
            if (first is VertexWall) temp = new VertexWall();
            else if (first is VertexRoom) temp = new VertexRoom();
            else temp = new VertexChain();

            temp.Point = new Point3D(second.Point.X, second.Point.Y, second.Point.Z);
            temp.Edges.AddRange(first.Edges);
            temp.Edges.AddRange(second.Edges);
            temp.Edges.GroupBy(x => x.SecondVertex).Select(x => x.First()).ToList();
            Vertices.Remove(first);
            Vertices.Remove(second);
            Vertices.Add(temp);
        }
        //нужно сделаать так, что б это отправлять на мобилку
        public override string ToString() { return ""; }
		public void ToFile()
		{
			foreach(var e in walls)
			{


				e.ToString();
				// и тип каждый так вызвываешь и записываешь файл, вот так вот тип абстрактненько епонятно
				//каждый что?
				// ну вот для всех перегрузил ту стринг классов, потом вызываешь для каждого объекта и сразу записываешь в файл эту информацию
				//для стен вершин комнат...???
				// ну да, я вот тут подумаль.....
			}

			ToString();
		}
    }
}

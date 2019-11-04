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
        public List<Floor> Floors;

        public List<Room>  Rooms;
        //-----------------------

        /// <summary>
        /// Конструктор
        /// </summary>
        public Graph()
        {
            Floors = new List<Floor>();
            Rooms  = new List<Room>();
        }

        public void CreateNewFloor(int level=-999)
        {
            Floors.Add(new Floor(level));
        }
        public bool isFloor(int level)
        {  
            foreach (var i in Floors)
            {
                if (i.Level == level){ 
                    return true; }
            }
            return false;
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
            Floors[Z].chains.Add(new VertexChain(vertexName, X, Y, Z));
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
            Floors[Z].rooms.Add(new VertexRoom(vertexName, X, Y, Z));
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
            Floors[floor].walls.Add(new VertexWall(vertexName, X, Y, floor));
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
                foreach (var v in Floors[floor].walls)
                {
                    if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                              (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                        return v; }
                return null;
            }
            else if (what == "room")
            {
                foreach (var v in Floors[floor].rooms)
                {
                    if (36 >= (v.Point.X - range.X) * (v.Point.X - range.X) +
                              (v.Point.Y - range.Y) * (v.Point.Y - range.Y))
                        return v; }
                return null;
            }
            else
            {
                foreach (var v in Floors[floor].chains)
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
            foreach (var v in Floors[floor].walls)
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
        public void AddEdge(int firstnum, int secondnum, String type, int floor, int weight)
        {
            switch (type)
            {
                case "Rooms":
                    break;
                case "Chains":
                    break;
                case "Walls":
                    if (Floors[floor].walls[firstnum] != null && Floors[floor].walls[secondnum] != null)
                    {
                        GraphEdge NewOne = new GraphEdge(Floors[0].walls[firstnum], Floors[0].walls[secondnum], weight);
                        // Vertices[firstnum].AddEdge(Vertices[secondnum], weight);
                        // Vertices[secondnum].AddEdge(Vertices[firstnum], weight);
                    }
                    break;
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
                if (first != null && Floors[floor].walls[secondnum] != null)
                {
                    GraphEdge NewOne = new GraphEdge(first, Floors[floor].walls[secondnum], weight);
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
        public void AddEdge(int firstnum, IVertex second,int floor, int weight)
        {
            if (second.GetType() == typeof(VertexWall))
                if (second != null && Floors[floor].walls[firstnum] != null) { 

                    GraphEdge NewOne = new GraphEdge(Floors[floor].walls[firstnum], second, weight);
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
          //  Vertices.Remove(first);
          //  Vertices.Remove(second);
          //  Vertices.Add(temp);
          //ВОТ ТУТ ХЗ ЧТО НО ЗРЯ ТЫ ВОТ ЕТО ИСПОЛЬЗОВАЛ, НУЖНЫ ПРОВЕРКИ ТИПОВ БУДУТ
        }
        //нужно сделаать так, что б это отправлять на мобилку
        public override string ToString() { return ""; }
		public void ToFile()
		{
			foreach(var e in Floors)
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

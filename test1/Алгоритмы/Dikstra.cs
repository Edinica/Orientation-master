﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    // НУ ВОТ ЭТО ВООБЩЕ, НА МЕГА БУДУЮЗЕЕ
    // КОРОЧ УДАЛЯЙ
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public IVertex Vertex { get; set; }

        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public int EdgesWeightSum { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public IVertex PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(IVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }


    /// <summary>
    /// Алгоритм Дейкстры
    /// </summary>
    public class Dijkstra
    {
        Graph graph;

        List<GraphVertexInfo> infos;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>
        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Информация о вершине</returns>
        GraphVertexInfo GetVertexInfo(IVertex v)
        {
            foreach (var i in infos)        
                if (i.Vertex.Equals(v))          
                    return i;            
            return null;
        }

        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)         
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }   
            return minVertexInfo;
        }

        /// <summary>
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startName">Название стартовой вершины</param>
        /// <param name="finishName">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
     //   public string FindShortestPath(string startName, string finishName)
     //   {
     //          return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
     //   }

        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public string FindShortestPath(IVertex startVertex, IVertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)               
                    break;          
               // SetSumToNextVertex(current);
            }
            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
      // void SetSumToNextVertex(GraphVertexInfo info)
      // {
      //     info.IsUnvisited = false;
      //     foreach (var e in info.Vertex.Edges)
      //     {
      //         var nextInfo = GetVertexInfo(e.ConnectedVertex);
      //         var sum = info.EdgesWeightSum + e.EdgeWeight;
      //         if (sum < nextInfo.EdgesWeightSum)
      //         {
      //             nextInfo.EdgesWeightSum = sum;
      //             nextInfo.PreviousVertex = info.Vertex;
      //         }
      //     }
      // }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        string GetPath(IVertex startVertex, IVertex endVertex)
        {
            var path = endVertex.ToString();
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                path = endVertex.ToString() + path;
            }
            return path;
        }
    }
}

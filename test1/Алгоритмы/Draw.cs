using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using MathNet.Numerics.LinearAlgebra.Double;

namespace test1
{
    /// <summary>
    /// Класс отрисовки
    /// </summary>
    public class Draw
    {
        // мусор
        private double[,] GraphToMass(List<IVertex> obj)
        {
            double[,] arrray = new double[obj.Count, 4];

            if (obj.Count == 0) return arrray;
            for (byte i = 0; i < obj.Count; i++)
            {
                arrray[i, 0] = obj[i].Point.X;
                arrray[i, 1] = obj[i].Point.Y;
                arrray[i, 2] = obj[i].Point.Z;
                arrray[i, 3] = 1.0;
            }
            return arrray;
        }
        /*
        public void ShowPoints(Graphics grf, double x, double y, double z) {
            Point Point_0 = new Point(0, 0);
            Point_0.Y = 500 / 2 - 50;
            Point_0.X = 500 / 2;


            double[,] mass;
//            mass = GraphToMass(graph.Vertices);
            var initialFigure = Matrix.Build.DenseOfArray(mass);


            MathNet.Numerics.LinearAlgebra.Matrix<double> transferalMatrix = Matrix.Build.DenseOfArray(new[,] {
                            { Math.Cos(y), Math.Sin(x)*Math.Sin(y),  0, 0},
                            { 0,              Math.Cos(x),           0 ,0},
                            { Math.Sin(y), -Math.Sin(x)*Math.Cos(y), 0, 0 },
                            { 0,                 0,                  0, 1.0 }});

            initialFigure.Multiply(transferalMatrix, initialFigure);

            for (byte i = 0; i < initialFigure.RowCount - 1; i++)
            {
                grf.DrawLine(new Pen(Color.Red), (float)initialFigure[i, 0] + Point_0.X, (float)initialFigure[i, 1] + Point_0.Y,
                                                  (float)initialFigure[i + 1, 0] + Point_0.X, (float)initialFigure[i + 1, 1] + Point_0.Y);
            }
        }
*/

        // устарело, терерь мы ресуем не граф а стены
        public static void DrawGraph(Graphics pic, Graph graf, int flor, Point centr)
        {
            for(int f=0;f<graf.Floors.Count;f++)
            for (int i = 0; i < graf.Floors[f].walls.Count; i++)
            {
                if (graf.Floors[f].walls[i].Point.Z != (flor - 1) * 50) continue;

                pic.FillRectangle(Brushes.Black, (float)graf.Floors[f].walls[i].Point.X + centr.X - 1,
                                                 (float)graf.Floors[f].walls[i].Point.Y + centr.Y - 1, 2, 2);

                for (int j = 0; j < graf.Floors[f].walls[i].Edges.Count; j++)
                {
                    pic.DrawLine(new Pen(Brushes.Black, 2),
                                (float)graf.Floors[f].walls[i].Point.X + centr.X,
                                (float)graf.Floors[f].walls[i].Point.Y + centr.Y,
                                (float)graf.Floors[f].walls[i].Edges[j].SecondVertex.Point.X + centr.X,
                                (float)graf.Floors[f].walls[i].Edges[j].SecondVertex.Point.Y + centr.Y);
                }

            }
        }

        // рисуем стены
        public static void DrawWalls(Graphics pic, Graph graf, int floor, Point centr,int size)
        {
            for (int i = 0; i < graf.Floors[floor].walls.Count; i++)
            {
                // каждую точку
                pic.FillRectangle(Brushes.Black, (float)graf.Floors[floor].walls[i].Point.X - size,
                                                 (float)graf.Floors[floor].walls[i].Point.Y - size, 2* size, 2* size);
                // все линии    
                for (int j = 0; j < graf.Floors[floor].walls[i].Edges.Count; j++)
                {
                    pic.DrawLine(new Pen(Brushes.Black, size),
                                (float)graf.Floors[floor].walls[i].Point.X,
                                (float)graf.Floors[floor].walls[i].Point.Y,
                                (float)graf.Floors[floor].walls[i].Edges[j].SecondVertex.Point.X,
                                (float)graf.Floors[floor].walls[i].Edges[j].SecondVertex.Point.Y);
                }

            }
        }


    }
}

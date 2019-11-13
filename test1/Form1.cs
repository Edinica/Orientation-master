using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = trackBar4.Value.ToString() + " этаж";
            grap = Graphics.FromHwnd(pictureBox1.Handle);
            grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//повышение качства рисовки
            brush = new SolidBrush(Color.Violet);
            picture = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            grap = Graphics.FromImage(picture);
            pen = new Pen(Color.Black);

            centr = new Point(pictureBox1.Width/2,pictureBox1.Height/2);
            label4.Text = centr.ToString();
            build.CreateNewFloor(trackBar4.Value);
        }

        public Graphics grap;
        Bitmap picture;
        SolidBrush brush;
        Pen pen;
		bool move;
		int chosen =-1;
		IVertex movable;//

        public Graph build = new Graph(); // вот сам граф, тоесть, ну здание или т.п

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // эт короче дубилрование предыдущего этажа на текущий
        // ТААААК А ВОТ ЭТА ЧТУКА СКОРЕЕ ВСЕГО НАХЕР СЛОМАЛАСЬ КСТА
        private void Button1_Click(object sender, EventArgs e)
        { 
            // если на этаже уже есть точки - не дублиррурем
            if (build.Floors[trackBar4.Value - 1].walls.Count() != 0) { return; }

            // проецирование нижнего этажа на верхний
            for (int i = 0; i < build.Floors[trackBar4.Value - 2].walls.Count(); i++)
            {
                build.AddVertexWalls("", (int)build.Floors[trackBar4.Value - 2].walls[i].Point.X,
                                         (int)build.Floors[trackBar4.Value - 2].walls[i].Point.Y,
                                         (int)build.Floors[trackBar4.Value - 2].walls[i].Point.Z+1);
            }

            // дублировани связей
            for (int i = 0; i < build.Floors[trackBar4.Value - 2].walls.Count(); i++)
            for (int j = 0; j < build.Floors[trackBar4.Value - 2].walls[i].Edges.Count(); j++)
            {
                build.Floors[trackBar4.Value - 1].walls[i].AddEdge(
                   build.FindVertex(
                       new Point((int)build.Floors[trackBar4.Value - 2].walls[i].Edges[j].SecondVertex.Point.X,
                                 (int)build.Floors[trackBar4.Value - 2].walls[i].Edges[j].SecondVertex.Point.Y),
                       trackBar4.Value - 1, "wall"),5);
            }

            // создания связей между этажами
            for (int i = 0; i < build.Floors[trackBar4.Value - 2].walls.Count; i++)
            {
                build.AddEdge(
                    build.Floors[trackBar4.Value-2].walls[i],
                    build.FindVertex(new Point(
                        (int)build.Floors[trackBar4.Value - 1].walls[i].Point.X,
                        (int)build.Floors[trackBar4.Value - 1].walls[i].Point.Y),
                        trackBar4.Value - 1, "wall"),0);
            }

            Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,1);
            pictureBox1.Image = picture;
            // MessageBox.Show(buil.Vertices.Count.ToString());
        }

        Point centr; // точка отсчета экранных координат
        Point StartMove; // начало смящения

        private void TrackBar4_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar4.Value.ToString()+" этаж";
            grap.Clear(Color.White);


            if (build.isFloor(trackBar4.Value))
            {
                label5.Text = build.Floors.Count.ToString() + " этажей";
                label6.Text = build.Floors[trackBar4.Value - 1].walls.Count().ToString() + " точек на этаже ";

                Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,1);
            }
            pictureBox1.Image = picture;
        }

        
        IVertex firstvertex= null; // первая нажатая точка на экране, от которой пойдет линия
        bool    firstclick = true; // нажимаем первый раз или уже ставим 2ю точку?

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
				if (Control.ModifierKeys == Keys.Control)//перенос точки по щелчку по ней
				{
					//chosen = true;

					if (chosen == -1)// если нет там точки - создаем
					{
						//movable = build.FindVertex(new Point(e.X, e.Y), trackBar4.Value - 1, "wall");
						chosen = build.FindNumberVertex(new Point(e.X, e.Y), trackBar4.Value - 1, "wall");
						//perenos ^= true;
					}
					else
					{//проработать это в mousemove
						
					}
				}
				else
				{
					move ^= true;
					chosen = -1;
				}
				
				pictureBox1.MouseMove += PictureBox1_MouseMove;
                StartMove = new Point(e.X, e.Y);
            }// смещение графа режим просмотра
            else if (radioButton2.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (firstclick)
                    { // первый клик
                        pictureBox1.MouseMove += PictureBox1_MouseMove; // запускаем движение
                        var temp = build.FindVertex(new Point(e.X, e.Y), trackBar4.Value - 1, "wall");
                        // если нажадли туды, где уже есть точка - берем ее
                        if (temp == null)// если нет там точки - создаем
                        {
                            build.AddVertexWalls("", e.X, e.Y, (trackBar4.Value - 1));
                            firstvertex = build.Floors[trackBar4.Value - 1].walls.Last();
                        }
                        else { firstvertex = temp; }
                        firstclick = false;
                    }
                    else
                    { // второй клик
						int k=0; int m = 0;//при совпадении со стеной рисуется ближайшая правильная точка на ней
                        pictureBox1.MouseMove -= PictureBox1_MouseMove;
						int x = 0;//выводится значение сравнения
                        var ispoint = build.FindVertex(new Point(e.X, e.Y), trackBar4.Value - 1, "wall");
						int iswall=-1;
                        // если второй раз кликнули на точку должны взять ее
                        //build.FindWall(new Point(e.X, e.Y), trackBar4.Value - 1);
						for (int i = 1; i < build.Floors[trackBar4.Value - 1].walls.Count; i+=2)
							{
							k = 0;m = 0;
							if (
								(build.Floors[trackBar4.Value - 1].walls.Count > 1) &&///1) 
								build.Floors[trackBar4.Value - 1].uslovie
								(build.Floors[trackBar4.Value - 1].walls[i - 1], build.Floors[trackBar4.Value - 1].walls[i],e.X,e.Y)&&//2)
								//если точка лежит в пределе отрезка
								build.On_line(build.Floors[trackBar4.Value - 1].walls[i - 1].Point,
								build.Floors[trackBar4.Value - 1].walls[i].Point,
								new Point(e.X, e.Y), out x, out k, out m)) {//3)
								iswall = i - 1; MessageBox.Show("да " + (i - 1).ToString() + "\n" + x.ToString());
								//добавить разбиение ребра на 2 ребра
								//build.Floors[trackBar4.Value - 1].walls[i - 1].Edges[0].SecondVertex = new VertexWall("", e.X + k, e.Y + m, trackBar4.Value - 1);
								//функция которая принимает 2 вершины, удаляет удаляет связи между собой 
								//build.Floors[trackBar4.Value - 1].walls[i].Edges[0].FurstVertex = new VertexWall("", e.X + k, e.Y + m, trackBar4.Value - 1);

								break; }
								else MessageBox.Show("0" + "\n" + i.ToString()///x.ToString()
									);
							}
					   //я тут подумал о использовании лямбж выражений для рисования фигни как эта
					   /*напомни эт как
						* 
						* 
					    */
                        if (ispoint == null || iswall!=-1)
                        {
                            build.AddVertexWalls("", e.X, e.Y, (trackBar4.Value - 1));
                            build.AddEdge(firstvertex, build.Floors[trackBar4.Value - 1].walls.Count - 1, trackBar4.Value - 1, 0);
                            grap.FillRectangle(Brushes.Black, (float)build.Floors[trackBar4.Value - 1].walls.Last().Point.X - 1,
                                                              (float)build.Floors[trackBar4.Value - 1].walls.Last().Point.Y - 1, 3, 3);
                            pictureBox1.Image = picture;
                        }
                        else { build.AddEdge(firstvertex, ispoint, 0); }
                        firstclick = true;
                        firstvertex = null;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    var temp = build.FindVertex(new Point(e.X, e.Y), trackBar4.Value - 1, "wall");
                    if (temp != null)
                    {
                        firstvertex = temp;
                        pictureBox1.MouseMove += PictureBox1_MouseMove;
                    }
                }
            }// создание плана здания
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if      (radioButton1.Checked)
            {
				if (chosen != -1) chosen = -1;
				else
				{
					move ^= true;

					movable = null;

					pictureBox1.MouseMove -= PictureBox1_MouseMove;
				}
            }// смещение графа 
            else if (radioButton2.Checked)
            {
                if (firstvertex != null)
                {
                    grap.Clear(Color.White);
                    Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,1);
                }
            }// создание плана здания

            pictureBox1.Image = picture;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if      (radioButton1.Checked )
            {
				if(move)
				{ grap.Clear(Color.White);
					centr.X -= StartMove.X - e.X;
					centr.Y -= StartMove.Y - e.Y;
					for (int i = 0; i < build.Floors[trackBar4.Value - 1].walls.Count; i++)
					{
						build.Floors[trackBar4.Value - 1].walls[i].ChangePoint(
							(int)build.Floors[trackBar4.Value - 1].walls[i].Point.X - (StartMove.X - e.X),
							(int)build.Floors[trackBar4.Value - 1].walls[i].Point.Y - (StartMove.Y - e.Y), trackBar4.Value - 1);
					}
					StartMove.X = e.X;
					StartMove.Y = e.Y;
					//grap.DrawEllipse(pen, centr.X - 25, centr.Y - 25, 50, 50);

					Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,3);
					pictureBox1.Image = picture;
					label4.Text = centr.ToString();
				}
				if(chosen!=-1){
					build.Floors[trackBar4.Value - 1].walls[chosen].ChangePoint(e.X, e.Y, trackBar4.Value - 1);
					grap.Clear(Color.White);
					Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,3);

					pictureBox1.Image = picture;
				}
			}// смещение графа 
            else if (radioButton2.Checked && (firstvertex != null))
            {
                grap.Clear(Color.White);

                if(firstvertex!=null)grap.DrawLine(new Pen(Brushes.Black, 1),
                              new Point((int)firstvertex.Point.X, (int)firstvertex.Point.Y),
                              new Point(e.X, e.Y));

                Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr,1);
                // координаты, расстояние до и градусы 
                Pen pen = new Pen(Brushes.LightGray, 1);
                pen.DashStyle = DashStyle.Dash;

                grap.DrawLine(pen, new Point((int)firstvertex.Point.X, (int)firstvertex.Point.Y),
                                   new Point((int)firstvertex.Point.X, e.Y));

                grap.DrawLine(pen, new Point((int)firstvertex.Point.X, (int)firstvertex.Point.Y),
                                   new Point(e.X, (int)firstvertex.Point.Y));

                grap.DrawLine(pen, new Point(e.X, e.Y),
                                   new Point(e.X, (int)firstvertex.Point.Y));

                grap.DrawLine(pen, new Point(e.X, e.Y),
                                   new Point((int)firstvertex.Point.X, e.Y));

                grap.DrawString(Convert.ToInt32(Math.Sqrt(Math.Pow(-firstvertex.Point.X+ e.X, 2) + Math.Pow(-firstvertex.Point.Y + e.Y, 2))).ToString()+"m.", new Font("Arial", 8), new SolidBrush(Color.Black),e.X-40, e.Y);


                var stat = new Point((int)firstvertex.Point.X, (int)firstvertex.Point.Y);
				// ВОТ ТУТ ДОЛЖНЫ ГРАДУМЫ РАБОТАТЬ НО НЕ
				double x = -Convert.ToDouble(e.Y - stat.Y) / Convert.ToDouble(e.X - stat.X);

				double result = Math.Abs(Math.Atan(x) / Math.PI * 180);
				/*(main.X * right.X + main.Y * right.Y) / 
				(Math.Sqrt(main.X * main.X + main.Y * main.Y) * Math.Sqrt(right.X * right.X + right.Y * right.Y));
				*/
				if(Math.Sqrt(Math.Pow(-firstvertex.Point.X + e.X, 2) + Math.Pow(-firstvertex.Point.Y + e.Y, 2)) != 0)
				grap.DrawString(
                       Math.Round(result,3) + "*", 
                       new Font("Arial", 8), new SolidBrush(Color.Black),
                       (int)firstvertex.Point.X, (int)firstvertex.Point.Y);
				else grap.DrawString(
					   0 + "*",
					   new Font("Arial", 8), new SolidBrush(Color.Black),
					   (int)firstvertex.Point.X, (int)firstvertex.Point.Y);

				for (int i=0;i< build.Floors[trackBar4.Value - 1].walls.Count;i++)
					grap.DrawString(
					   i + "*",
					   new Font("Arial", 8), new SolidBrush(Color.Black),
					   (int)build.Floors[trackBar4.Value - 1].walls[i].Point.X - 10,
					   (int)build.Floors[trackBar4.Value - 1].walls[i].Point.Y - 10);
				pictureBox1.Image = picture;
            }// создание плана здания

        }
        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("df");
           // MessageBox.Show(build.Vertices.Count.ToString());
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("df");
        }

        private void PictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("df");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //build.Vertices.ToArray();
            build.ToFile();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // НОВЫЙ ЭТАЖ ЕСЛИ СОЗДАЛИ - ТО ВОТЬ
            if (!build.isFloor(trackBar4.Value))
                 build.CreateNewFloor(trackBar4.Value);
            trackBar4.Maximum++; 
        }

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				grap.Clear(Color.White);
				Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr, 3);
				pictureBox1.Image = picture;
			}
			else
			{
				grap.Clear(Color.White);
				Draw.DrawWalls(grap, build, trackBar4.Value - 1, centr, 1);
				pictureBox1.Image = picture;
			}
		}
	}
}

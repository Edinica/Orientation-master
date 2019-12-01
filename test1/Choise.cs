using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
	public partial class Choise : Form
	{
		public Choise()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (true)//выбор здания
			{
				Hide();
				Plan main = new Plan();
				main.ShowDialog();
				Show();
			}
			else MessageBox.Show("Вы не выбрали здания!");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
				Hide();
				Redactor main = new Redactor();
				main.ShowDialog();
				Show();
			
		}
	}
}

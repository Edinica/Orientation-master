﻿using System;
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
	public partial class Authorization : Form
	{
		
		public Authorization()
		{
			InitializeComponent();
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (true)//проверка логина
			{
				Hide();
				Choise main = new Choise();
				main.ShowDialog();
				Show();
			}
			else MessageBox.Show("Непраильный логин или пароль!");
		}

		private void Authorization_Load(object sender, EventArgs e)
		{
			
			
		}
	}
}
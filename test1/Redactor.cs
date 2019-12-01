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
	public partial class Redactor : Form
	{
		public Redactor()
		{
			InitializeComponent();
		}

		private void Redactor_FormClosed(object sender, FormClosedEventArgs e)
		{
			Close();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hitman
{
	public partial class confirmation : Form
	{
		//Variable declaration
		public int sec = Form1.endsec;
		public static bool x = false;
		public confirmation()
		{
			InitializeComponent();
			label2.Text = "This computer will shutdown in: " + sec + " seconds";
			label2.Update();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			
		}

		private void button2_Click(object sender, EventArgs e)	//No button
		{
			File.WriteAllText("verification.txt", "false");
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)	//Yes button
		{
			File.WriteAllText("verification.txt", "true");
			x = true;
			this.Close();
		}
	}
}

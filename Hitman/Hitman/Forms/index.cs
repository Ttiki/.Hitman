using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Hitman
{
	public partial class Form1 : Form
	{
		//Variables declaration:
		public static int sec, min, hour;
		public static bool IsOn = Convert.ToBoolean((File.ReadAllText("verification.txt")));
		public static int endsec;
		public int x;

		//End of Variable declaration:
		public Form1()
		{
			InitializeComponent();
			timer1.Start();
			notifyIcon1.ShowBalloonTip(1000);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Display a MsgBox asking the user to close the form.
			if (MessageBox.Show("Warning! If you close the app, the shutdown sequence will be stopped. Are you sure you want to close ?", "Close Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				// Cancel the Closing event
				e.Cancel = true;
			}
			File.WriteAllText("verification.txt", "false");
		}

		//Notify Icon code section
		private void Form1_Resize(object sender, EventArgs e)
		{
			/*if the form is minimized  
			*hide it from the task bar  
			*and show the system tray icon (represented by the NotifyIcon control)*/
			if (this.WindowState == FormWindowState.Minimized)
			{
				Hide();
				notifyIcon1.Visible = true;
			}
		}

		private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
		{
			if (IsOn == false)
			{
				notifyIcon1.BalloonTipText = "There is none shutdown sequence launched for the moment!";
			}
			else
			{
				notifyIcon1.BalloonTipText = "The shutdown sequence has been launched! The computer will shutdown in:{0}, {1}, {3}" + hour + ":" + min + ":" + sec;
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}
		//End of the notify icon code section

		//Begin of buttons part (Help & Big Red Button BRB)

		private void button2_Click(object sender, EventArgs e) //Help Button
		{
			System.Diagnostics.Process.Start(@"..\..\html\hitman_informations.html");
		}

		public void button1_Click(object sender, EventArgs e) //Big Red Button
		{
			endsec = sec + (min * 60) + (hour * 3600);
			Form form2 = new confirmation();
			form2.Show();
			IsOn = Convert.ToBoolean(File.ReadAllText("verification.txt"));
		}

		private void button3_Click(object sender, EventArgs e) //Abort button
		{
			System.Diagnostics.Process.Start("Shutdown", "/a");
			Console.WriteLine("The shutdown of the computer has been aborted!");
			File.WriteAllText("verification.txt", "false");
			button3.Enabled = IsOn;
			
		}

		/*End of buttons part
		 * -----------------------------------------------------------------------------------------------------------------------
		 * Begin of time chooser part*/

		public void trackBar1_Scroll(object sender, EventArgs e)	//TrackBar: Seconds
		{
			textBox1.Text = trackBar1.Value.ToString();
			sec = trackBar1.Value;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)	//TextBox : seconds
		{
			sec = Convert.ToInt16(textBox1.Text);
		}

		public void trackBar2_Scroll(object sender, EventArgs e)	//Trackbar : Minutes
		{
			textBox2.Text = trackBar2.Value.ToString();
			min = trackBar2.Value;
		}

		private void textBox2_TextChanged(object sender, EventArgs e)	//TextBox : Minutes
		{
			min = Convert.ToInt16(textBox2.Text);
		}

		public void textBox3_TextChanged(object sender, EventArgs e)	//TextBox : Hour
		{
			trackBar3.Value = Convert.ToInt32(this.textBox3.Text);
			hour = Convert.ToInt16(textBox3.Text);
		}

		public void trackBar3_Scroll(object sender, EventArgs e)	//Trackbar : Hours
		{
			textBox3.Text = trackBar3.Value.ToString();
			hour = trackBar3.Value;
		}

		/*End of time chooser part
		 * -----------------------------------------------------------------------------------------------------------------------
		 * Begin of the Conclusion time*/


		private void label10_Click(object sender, EventArgs e) //Conclusion time : Seconds
		{
			
		}

		private void label9_Click(object sender, EventArgs e) //Conclusion time : Minutes
		{
			
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox3_Click(object sender, EventArgs e)
		{

		}

		private void label8_Click(object sender, EventArgs e) //Conclusion time : Hours
		{
			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			label9.Text = textBox2.Text;
			label10.Text = textBox1.Text;
			label8.Text = textBox3.Text;
			Console.WriteLine(IsOn);
			IsOn = Convert.ToBoolean(File.ReadAllText("verification.txt"));
			this.button3.Enabled = IsOn;
			if (IsOn == true && confirmation.x == true)
			{
				Process.Start("shutdown", " /s /t "+ endsec);
				Console.WriteLine("The computer will shutdown in:" + sec + " seconds!");
				Console.WriteLine("Recieved");
				confirmation.x = false;
			}
		}
	}
}

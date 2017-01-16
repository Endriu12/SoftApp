using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;


namespace TestApp
{
    public partial class TortoiseYukon : Form
    {
        //add method who use WinAPI
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, int childAfter, string className, string windowTitle);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        const int WM_CHAR = 0x0102;
        string path = @"password.txt";
        string program = "TortoiseGit -  git CLI stdin wrapper";
        string childwindow = "Edit";


        public TortoiseYukon()
        {
            InitializeComponent();
        }

        private void showProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter write_text;  //Class file recording
            FileInfo file = new FileInfo(path);
            write_text = file.AppendText(); //if the file does not exist it will be created
            write_text.WriteLine(textBox1.Text); 
            write_text.Close(); 

            Timer tmrShow = new Timer();
            tmrShow.Interval = 5000; // 5 
            tmrShow.Tick += tmrShow_Tick; // event
            tmrShow.Enabled = true;//timer start

            this.Hide();
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {
            StreamReader streamReader = new StreamReader(path); //Open the file for reading
            string str = ""; 
            while (!streamReader.EndOfStream) 
            {
                str += streamReader.ReadLine(); //The variable str row to record the contents of the file
            }
            IntPtr hWnd = FindWindow(null, program);
            if (hWnd.Equals(IntPtr.Zero))
                {
                   
                }
                else  {
                    IntPtr hWndEdit = FindWindowEx(hWnd, 0, childwindow, null);
                    SetForegroundWindow(hWnd);
                    SendKeys.SendWait(str);
                    SendKeys.SendWait("~");//enter 
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        } 
    }
}
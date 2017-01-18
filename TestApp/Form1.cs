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
using TestApp.Properties;
using System.Xml;


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
        string myxmlfile = @"myxml.xml";
        //  string difference = @"YourVersion.txt";
        //  string program = "TortoiseGit -  git CLI stdin wrapper";
        string childwindow = "Edit";
        string windowl = "label1";


        public TortoiseYukon()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(myxmlfile);

            foreach (XmlNode node in doc.DocumentElement)
            {
                string name = node.Attributes[1].Value;
                string password = node.Attributes[0].Value;
                listBox1.Items.Add(new Repository(name, password));
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            // ShowInTaskbar = false;
            Hide();
        }

        private void showProgramToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter vvod = new StreamWriter(path, false);
            vvod.WriteLine(textBox1.Text);
            vvod.Close();
            this.Hide();
            int timerT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["timer"]);
            Timer tmrShow = new Timer();
            tmrShow.Interval = timerT; // 5 
            tmrShow.Tick += tmrShow_Tick; // event
            tmrShow.Enabled = true;//timer start
            this.Hide();
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {
            string program = System.Configuration.ConfigurationManager.AppSettings["program"];
            IntPtr hWnd = FindWindow(null, program);
            if (hWnd.Equals(IntPtr.Zero))
            { }
            else
            {
                StreamReader streamReader = new StreamReader(path); //Open the file for reading
                string str = "";
                while (!streamReader.EndOfStream)
                {
                    str += streamReader.ReadLine(); //The variable str row to record the contents of the file
                }
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

        private void TortoiseYukon_Shown(object sender, EventArgs e)
        {
            string[] array = File.ReadAllLines(path);
            if (array.Length != 0)
            {
                Hide();
            }
        }    

    }
    class Repository
    {
        public string Name { get; private set; }
        public string Password { get; private set; }

        public Repository(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return Password;
        }
    }
}
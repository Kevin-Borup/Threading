using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_ThreadingExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(Sleep);
            t.Name = "Sleep Thread";
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Hallo";
        }

        private void Sleep()
        {
            Thread.Sleep(5000);
        }
    }
}

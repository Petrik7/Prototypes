using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestOutOfMemory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ByteTester st;

        private void button1_Click(object sender, EventArgs e)
        {
            st = new ByteTester();
            buttonRunTest.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            st.RunTimer();
        }
    }
}
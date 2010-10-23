using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;

namespace MainGUI
{
    public partial class MainForm : Form
    {
        private Capture curWebcam;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxID.Text) == false)
            {

            }
        }

        private void buttonEnroll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxID.Text) == false)
            {

            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}

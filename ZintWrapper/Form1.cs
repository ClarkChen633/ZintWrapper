using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;

namespace ZintWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img;
            ZintLib.Render();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            img = Image.FromFile("baro.png");
            pictureBox1.Image = img;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap myBitmap;
        bool isLoaded = false;
        private void LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "JPG影象(*.jpg)|*.jpg|BMP影象(*.bmp)|*.bmp|所有檔案(*.*)|*.*";
            of.FilterIndex = 0;
            of.ShowDialog();
            String filename = of.FileName.ToString();
            this.myBitmap = new Bitmap(filename);
            // Stretches the image to fit the pictureBox.
            Bitmap myImage = this.myBitmap;//new Bitmap(fileToDisplay);
                                           //pictureBox1.ClientSize = new Size(xSize, ySize);
            pictureBox1.Image = (Image)myImage;
            isLoaded = true;
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        public bool isDragging = false;
        int startX, startY;
        int endX, endY;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            if (isLoaded == false) return;
            startX = e.X;
            startY = e.Y;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isLoaded == false) return; 
            Graphics objGraphic = e.Graphics; 
            Pen pen = new Pen(Color.White);
            
            objGraphic.DrawLine(pen, startX, startY, startX, endY);
            objGraphic.DrawLine(pen, startX, startY, endX, startY);
            objGraphic.DrawLine(pen, startX, endY, endX, endY);
            objGraphic.DrawLine(pen, endX, startY, endX, endY);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true && isLoaded == true) { 
                endX = e.X;
                endY = e.Y;
               
                pictureBox1.Refresh();
            }
        }
    }
}

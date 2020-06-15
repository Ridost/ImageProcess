using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        public Bitmap myBitmap;
        public string TempImagePath;
        public string CurrentImagePath;
        public string CurrentImageName;
        DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
        bool isLoaded = false;
        private void LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "JPG影象(*.jpg)|*.jpg|BMP影象(*.bmp)|*.bmp|所有檔案(*.*)|*.*";
            of.FilterIndex = 0;
            of.ShowDialog();
            CurrentImagePath = of.FileName.ToString();
            CurrentImageName = of.SafeFileName;
            this.myBitmap = new Bitmap(CurrentImagePath);
            new Bitmap(CurrentImagePath).Save("temp.png");
            // Stretches the image to fit the pictureBox.
            Bitmap myImage = this.myBitmap;//new Bitmap(fileToDisplay);
                                           //pictureBox1.ClientSize = new Size(xSize, ySize);
            pictureBox1.Image = (Image)myImage;
            isLoaded = true;
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
   
            hScrollBar1.Value = 255;
            hScrollBar2.Value = 50;
            comboBox1.Items.Add("Gray-Level");
            comboBox1.Items.Add("NegativeEffect");
            comboBox1.Items.Add("Carbon");
            comboBox1.Items.Add("Cold-Feeling");
            comboBox1.Items.Add("Warm-Feeling");
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

        

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll && isLoaded)
            {
                string basedir = dir.Parent.Parent.Parent.Parent.FullName;
                string pyexePath = basedir + "\\Scripts\\Python\\contrast.py";
                string outputPath = "temp.png";
                string gamma;
                if (hScrollBar2.Value < 50) gamma = ((float)(hScrollBar2.Value + 1) / 50).ToString();
                else if (hScrollBar2.Value > 51) gamma = ((hScrollBar2.Value - 50) / 4).ToString();
                else gamma = "1";
                Console.WriteLine(gamma);
                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " " + CurrentImagePath + " " + gamma + " temp.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }
        }

        private void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll && isLoaded)
            {
                string basedir = dir.Parent.Parent.Parent.Parent.FullName;
                string pyexePath = basedir + "\\Scripts\\Python\\brightness.py";
                string outputPath = "temp.png";
                string brightness = (hScrollBar1.Value-255).ToString();

                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " "+CurrentImagePath+" " + brightness + " temp.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }
        }

        private void hScrollBar1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoaded) {
                string basedir = dir.Parent.Parent.Parent.Parent.FullName;
                string pyexePath = basedir + "\\Scripts\\Python\\basic_func.py";
                string outputPath = "temp.png";
                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath +" "+comboBox1.SelectedIndex.ToString()+" "+CurrentImagePath+" temp.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }
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

                if (startX > endX) {
                    int temp = startX;
                    startX = endX;
                    endX = temp;
                }
                if (startY > endY) {
                    int temp = startY;
                    startY = endY;
                    endY = temp;
                }
                string basedir = dir.Parent.Parent.Parent.Parent.FullName;
                string pyexePath = basedir + "\\Scripts\\Python\\brightness.py";
                string outputPath = "temp.png";
                string brightness = "180";

                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " " + CurrentImagePath + " " + brightness + " temp.png "+startX+" "+startY+" "+(endX-startX)+" "+(endY-startY);//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
                pictureBox1.Refresh();
            }
        }
    }
}

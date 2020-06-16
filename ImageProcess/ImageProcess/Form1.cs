using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
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
            new Bitmap(CurrentImagePath).Save("temp_origin.png");
            new Bitmap(CurrentImagePath).Save("temp_revised.png");
            // Stretches the image to fit the pictureBox.
            Bitmap myImage = this.myBitmap;//new Bitmap(fileToDisplay);
                                           //pictureBox1.ClientSize = new Size(xSize, ySize);
            pictureBox1.Image = (Image)myImage;
            isLoaded = true;
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            hScrollBar1.Value = 255;
            hScrollBar2.Value = 50;
            comboBox1.Items.Add("Gray-Level");
            comboBox1.Items.Add("NegativeEffect");
            comboBox1.Items.Add("Carbon");
            comboBox1.Items.Add("Cold-Feeling");
            comboBox1.Items.Add("Warm-Feeling");

            comboBox2.Items.Add("White");
            comboBox2.Items.Add("Black");
            comboBox2.Items.Add("Red");
            comboBox2.Items.Add("Yellow");
            comboBox2.Items.Add("Green");
            comboBox2.Items.Add("BrightBlue");
            comboBox2.Items.Add("Blue");
            comboBox2.Items.Add("Purple");
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
            if (e.Button == MouseButtons.Right) {
                startX = 0;
                startY = 0;
                endX = 0;
                endY = 0;
                pictureBox1.Refresh();
                return;
            }
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
                string outputPath = "temp_revised.png";
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
                p.StartInfo.Arguments = pyexePath + " " + "temp_origin.png" + " " + gamma + " temp_revised.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp_revised.png", FileMode.Open, FileAccess.Read);
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
                string outputPath = "temp_revised.png";
                string brightness = (hScrollBar1.Value-255).ToString();

                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " "+"temp_origin.png"+" " + brightness + " temp_revised.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp_revised.png", FileMode.Open, FileAccess.Read);
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
                string outputPath = "temp_origin.png";
                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath +" "+comboBox1.SelectedIndex.ToString()+" "+"temp_origin.png"+" temp_revised.png";//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp_revised.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (startX == 0 && startY == 0 && endX == 0 && endY == 0) return;
            pictureBox1.Image = Cut(this.myBitmap,startX,startY,endX-startX,endY-startY);
            pictureBox1.Image.Save("temp_origin.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startX = 0; startY = 0; endX = 0; endY = 0;
            FileStream fs;
            fs = new FileStream(CurrentImagePath, FileMode.Open, FileAccess.Read);
            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
            new Bitmap(CurrentImagePath).Save("temp_origin.png");
            new Bitmap(CurrentImagePath).Save("temp_revised.png");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && isLoaded)
            {
                string basedir = dir.Parent.Parent.Parent.Parent.FullName;
                string pyexePath = basedir + "\\Scripts\\Python\\advance_func.py";
                string outputPath = "temp_origin.png";
                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " 0 temp_origin.png temp_revised.png " + textBox1.Text + " " + textBox2.Text + " " + comboBox2.SelectedIndex.ToString();
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.WriteLine(p.StartInfo.Arguments);
                p.Close();

                FileStream fs;
                fs = new FileStream("temp_revised.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();

            }
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
                string outputPath = "temp_origin.png";
                string brightness = "100";

                Process p = new Process();
                //p.StartInfo.FileName = pyexePath;//需要執行的檔案路徑
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.UseShellExecute = false; //必需
                p.StartInfo.RedirectStandardOutput = true;//輸出引數設定
                p.StartInfo.RedirectStandardInput = true;//傳入引數設定
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = pyexePath + " " + "temp_origin.png" + " " + brightness + " temp_revised.png "+startX+" "+startY+" "+(endY-startY) +" "+ (endX - startX);//引數以空格分隔，如果某個引數為空，可以傳入””
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.Write(output);//輸出
                p.Close();

                FileStream fs;
                fs = new FileStream("temp_revised.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
                pictureBox1.Refresh();
            }
        }
        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }
    
    }
}

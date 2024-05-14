using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApplicationAssignment
{
    public partial class Form1 : Form
    {
        private Point Start, End;
        private List<Triangle> trianglesList;
        Bitmap bm;
        Graphics g;
        bool isDrawing = false;
        Point px, py;
        Pen pen = new Pen(Color.Black, 1);
        Pen erase = new Pen(Color.White, 10);
        int x, y, sX, sY, cX, cY;

        ColorDialog cd = new ColorDialog();
        Color New_Color;

        Stack<Bitmap> undoStack = new Stack<Bitmap>();

        private Point startCrop;
        private Rectangle cropRect;
        private bool isCropping = false;
        private Bitmap originalBitmap;

        public enum TOOL
        {
            SELECT,
            PEN,
            ERASER,
            FILLCOLOR,
            LINE,
            ELLIPSE,
            RECTANGLE,
            TRIANGLE,
            CROP
        }
        public TOOL curTool = TOOL.SELECT;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            g = DrawingArea.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            bm = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            DrawingArea.Image = bm;
            originalBitmap = new Bitmap(bm);
            trianglesList = new List<Triangle>();
            pen.Width = (float)pen_width.Value;
            undoStack.Push((Bitmap)DrawingArea.Image.Clone());
        }
        private void DrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            Start = End = e.Location;
            //lastPoint = e.Location;
            py = e.Location;

            cX = e.X;
            cY = e.Y;

            if (curTool == TOOL.CROP)
            {
                isCropping = true;
                startCrop = e.Location;
                Cursor = Cursors.Cross;
                originalBitmap = (Bitmap)DrawingArea.Image.Clone();
            }
        }
        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {

            //int startPointX = lastPoint.X < e.X ? lastPoint.X : e.X;
            //int startPointY = lastPoint.Y < e.Y ? lastPoint.Y : e.Y;

            //int shapeWidth = (lastPoint.X > e.X ? lastPoint.X : e.X) - startPointX;
            //int shapeHeight = (lastPoint.Y > e.Y ? lastPoint.Y : e.Y) - startPointY;

            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing)
                {
                    if (curTool == TOOL.PEN)
                    {
                        px = e.Location;
                        g.DrawLine(pen, px, py);
                        py = px;
                    }
                    if (curTool == TOOL.ERASER)
                    {
                        px = e.Location;
                        g.DrawLine(erase, px, py);
                        py = px;
                    }
                }
                if(curTool == TOOL.TRIANGLE)
                {
                    End = e.Location;
                    this.Invalidate();
                }
                if (isCropping)
                {
                    cropRect = new Rectangle(
                        Math.Min(startCrop.X, e.X),
                        Math.Min(startCrop.Y, e.Y),
                        Math.Abs(startCrop.X - e.X),
                        Math.Abs(startCrop.Y - e.Y)
                    );
                    Invalidate();
                }

                //if (curTool == TOOL.TRIANGLE)
                //{
                //    Point point1 = new Point() { X = startPointX, Y = startPointY + shapeHeight };
                //    Point point2 = new Point() { X = startPointX + (shapeWidth / 2), Y = startPointY };
                //    Point point3 = new Point() { X = startPointX + shapeWidth, Y = startPointY + shapeHeight };
                //    g.DrawPolygon(pen, new Point[] { point1, point2, point3 });
                //}
            }
            DrawingArea.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;

            if (curTool == TOOL.ELLIPSE)
            {
                toolStripStatusLabel1.Text = " Pos: " + e.X + "," + e.Y;
            }
            if (curTool == TOOL.RECTANGLE)
            {
                toolStripStatusLabel1.Text = " Pos: " + e.X + "," + e.Y;
            }
            if (curTool == TOOL.LINE)
            {
                toolStripStatusLabel1.Text = " Pos: " + e.X + "," + e.Y;
            }
        }
        private void DrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            sX = x - cX;
            sY = y - cY;

            if (curTool == TOOL.ELLIPSE)
            {
                g.DrawEllipse(pen, cX, cY, sX, sY);
            }
            if (curTool == TOOL.RECTANGLE)
            {
                g.DrawRectangle(pen, cX, cY, sX, sY);
            }
            if (curTool == TOOL.LINE)
            {
                g.DrawLine(pen, cX, cY, x, y);
            }
            if (curTool == TOOL.TRIANGLE)
            {
                //Triangle triangle = new Triangle(Start, End);
                //trianglesList.Add(triangle);
                //Invalidate();
                End = e.Location;
                this.Invalidate();
                Point point1 = Start;
                Point point2 = new Point((Start.X + End.X) / 2, End.Y);
                Point point3 = End;
                g.DrawPolygon(pen, new Point[] { point1, point2, point3 });
            }
            if (isCropping)
            {
                isCropping = false;
                Cursor = Cursors.Default;

                cropRect = new Rectangle(
                    Math.Min(startCrop.X, e.X),
                    Math.Min(startCrop.Y, e.Y),
                    Math.Abs(startCrop.X - e.X),
                    Math.Abs(startCrop.Y - e.Y)
                );
                Bitmap croppedBitmap = new Bitmap(cropRect.Width, cropRect.Height);
                using (Graphics g = Graphics.FromImage(croppedBitmap))
                {
                    g.DrawImage(originalBitmap, new Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect, GraphicsUnit.Pixel);
                }


                DrawingArea.Image = croppedBitmap;
                Invalidate();
            }

            undoStack.Push((Bitmap)DrawingArea.Image.Clone());

        }
        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isDrawing)
            {
                if (curTool == TOOL.ELLIPSE)
                {
                    g.DrawEllipse(pen, cX, cY, sX, sY);

                }
                if (curTool == TOOL.RECTANGLE)
                {
                    g.DrawRectangle(pen, cX, cY, sX, sY);
                }
                if (curTool == TOOL.LINE)
                {
                    g.DrawLine(pen, cX, cY, x, y);
                }
                if (curTool == TOOL.TRIANGLE)
                {
                    //End = PointToClient(MousePosition);
                    //Triangle triangle = new Triangle(Start, End);
                    //triangle.Draw(g, pen);
                    Point point1 = Start;
                    Point point2 = new Point((Start.X + End.X) / 2, End.Y);
                    Point point3 = End;
                    g.DrawPolygon(pen, new Point[] { point1, point2, point3 });
                }
                if (isCropping)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.White)), cropRect);
                    e.Graphics.DrawRectangle(Pens.Black, cropRect);
                }

            }
            //foreach (var triangle in trianglesList)
            //{
            //    triangle.Draw(g, pen);
            //}

        }
        private void Btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            New_Color = cd.Color;
            DrawingArea.BackColor = cd.Color;
            pen.Color = cd.Color;
            pick_color.BackColor = cd.Color;
        }
        private void Btn_pencil_Click(object sender, EventArgs e)
        {
           
            curTool = TOOL.PEN;
        }
        private void Btn_erase_Click(object sender, EventArgs e)
        {
            
            curTool = TOOL.ERASER;
        }
        private void Btn_ellipse_Click(object sender, EventArgs e)
        {
            
            curTool = TOOL.ELLIPSE;
        }
        private void Btn_rectangle_Click(object sender, EventArgs e)
        {
            
            curTool = TOOL.RECTANGLE;
        }

        private void Btn_line_Click(object sender, EventArgs e)
        {
            
            curTool = TOOL.LINE;
        }

        private void Btn_fill_Click(object sender, EventArgs e)
        {
            curTool = TOOL.FILLCOLOR;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            curTool = TOOL.CROP;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to save currrent drawing?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem.PerformClick();
            }
            else
            {
                g.Clear(Color.White);
                DrawingArea.Image = bm;
                curTool = TOOL.SELECT;
            }
        }
        private void ColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_color.PerformClick();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure you want to exit?", "Exit Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            { e.Cancel = true; }
        }

        private void ImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadToolStripMenuItem.PerformClick();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalBitmap = new Bitmap(DrawingArea.Image);
        }

        private void Btn_triangle_Click(object sender, EventArgs e)
        {
            curTool = TOOL.TRIANGLE;
            //toolStripStatusLabel1.Text = "Triangle is not working properly";
        }
        private void Btn_clear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want delete all ?", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                g.Clear(Color.White);
                DrawingArea.Image = bm;
                curTool = TOOL.SELECT;
                trianglesList.Clear();
            }

        }
        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (DrawingArea.Image != null)
            {
                var sdf = new SaveFileDialog();
                sdf.Filter = "Image(*.png)|*.png";
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Bitmap btm = bm.Clone(new Rectangle(0, 0, DrawingArea.Width, DrawingArea.Height), bm.PixelFormat);
                    btm.Save(sdf.FileName, ImageFormat.Png);
                    MessageBox.Show("Image Saved Sucessully");
                }
                else if (DrawingArea.Image == null)
                {
                    MessageBox.Show("Draw Something First");
                }
            }

        }
        static Point set_Point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Width / pb.Width;
            float py = 1f * pb.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }

        private void Validate(Bitmap bm, Stack<Point> sp, int x, int y, Color Old_Color, Color New_Color)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == Old_Color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, New_Color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color New_Clr)
        {
            Color Old_Color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, New_Clr);
            if (Old_Color == New_Clr) { return; }

            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    Validate(bm, pixel, pt.X - 1, pt.Y, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X, pt.Y - 1, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X + 1, pt.Y, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X, pt.Y + 1, Old_Color, New_Clr);
                }
            }
        }

        private void Pen_width_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)pen_width.Value;
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadToolStripMenuItem.PerformClick();
        }

        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadToolStripMenuItem.PerformClick();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (bm != null)
            {
                bm = (Bitmap)DrawingArea.Image;
                bm.RotateFlip(RotateFlipType.Rotate180FlipX);
                DrawingArea.Invalidate();
                DrawingArea.Image = bm;
                DrawingArea.Invalidate();
            }
        }

        private void btnFlip180V_Click(object sender, EventArgs e)
        {
            if (bm != null)
            {
                bm = (Bitmap)DrawingArea.Image;
                bm.RotateFlip(RotateFlipType.Rotate180FlipY);
                DrawingArea.Invalidate();
                DrawingArea.Image = bm;
                DrawingArea.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                Bitmap lastState = undoStack.Pop();
                DrawingArea.Image = lastState;
                g = Graphics.FromImage(DrawingArea.Image);
                DrawingArea.Invalidate();
            }
            
        }

        private void DrawingArea_MouseClick(object sender, MouseEventArgs e)
        {
            if (curTool == TOOL.FILLCOLOR)
            {
                Point point = set_Point(DrawingArea, e.Location);
                Fill(bm, point.X, point.Y, New_Color);
            }
        }


        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_clear.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            undoStack.Push((Bitmap)DrawingArea.Image.Clone());
            Bitmap image = (Bitmap)DrawingArea.Image;
            ApplyNegativeFilter(image);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            undoStack.Push((Bitmap)DrawingArea.Image.Clone());
            Bitmap image = (Bitmap)DrawingArea.Image;
            ApplySepiaFilter(image);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            undoStack.Push((Bitmap)DrawingArea.Image.Clone());
            Bitmap image = (Bitmap)DrawingArea.Image;
            ApplyGrayscaleFilter(image);
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DrawingArea.ImageLocation = ofd.FileName;
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (DrawingArea.Image != null)
            {
                Size size = new Size((int)Math.Round(DrawingArea.Image.Width * 1.05), (int)Math.Round(DrawingArea.Image.Height * 1.05));
                DrawingArea.Image = new Bitmap(DrawingArea.Image, size);
                bm = (Bitmap)DrawingArea.Image;
                DrawingArea.Invalidate();

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DrawingArea.Image != null)
            {
                Size size = new Size((int)Math.Round(DrawingArea.Image.Width * 0.9), (int)Math.Round(DrawingArea.Image.Height * 0.9));
                DrawingArea.Image = new Bitmap(DrawingArea.Image, size);
                bm = (Bitmap)DrawingArea.Image;
                DrawingArea.Invalidate();

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (bm != null)
            {
                bm = (Bitmap)DrawingArea.Image;
                bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                DrawingArea.Invalidate();
                DrawingArea.Image = bm;
                DrawingArea.Invalidate();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bm = (Bitmap)DrawingArea.Image;
            bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
            DrawingArea.Invalidate();
            DrawingArea.Image = bm;
            DrawingArea.Invalidate();
        }

        private void HelpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_save.PerformClick();
        }
        private void SaveAsBinaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sdf = new SaveFileDialog();
            sdf.Filter = "Binary(*.bin)|*.bin|(*.*|*.*";
            if (sdf.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(sdf.FileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, trianglesList);
                    MessageBox.Show("File Saved as Binary In Bin Directory");
                }
            }
        }
        private void OpenBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bin Files|*.bin;...";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    Triangle trianglesList = (Triangle)bin.Deserialize(stream);
                }
                foreach (var triangle in trianglesList)
                {
                    triangle.Draw(g, pen);
                }
            }
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ApplySepiaFilter(Bitmap image)
        {
            // Define the Sepia color matrix
            ColorMatrix sepiaMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0.393f, 0.349f, 0.272f, 0, 0},
                new float[] {0.769f, 0.686f, 0.534f, 0, 0},
                new float[] {0.189f, 0.168f, 0.131f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            // Create an ImageAttributes object and set the color matrix
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(sepiaMatrix);

            // Apply the filter to the image
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            DrawingArea.Invalidate();
        }

        private void ApplyNegativeFilter(Bitmap image)
        {
            // Define the negative color matrix
            ColorMatrix negativeMatrix = new ColorMatrix(new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });

            // Create an ImageAttributes object and set the color matrix
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(negativeMatrix);

            // Apply the filter to the image
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            DrawingArea.Invalidate();
        }

        private void ApplyGrayscaleFilter(Bitmap image)
        {
            // Define the grayscale color matrix
            ColorMatrix grayscaleMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            // Create an ImageAttributes object and set the color matrix
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(grayscaleMatrix);

            // Apply the filter to the image
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            DrawingArea.Invalidate();
        }
    }
}

namespace PaintApplicationAssignment
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBoxPanel = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnFlip180V = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pen_width = new System.Windows.Forms.NumericUpDown();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_triangle = new System.Windows.Forms.Button();
            this.btn_rectangle = new System.Windows.Forms.Button();
            this.btn_ellipse = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_erase = new System.Windows.Forms.Button();
            this.btn_fill = new System.Windows.Forms.Button();
            this.btn_color = new System.Windows.Forms.Button();
            this.pick_color = new System.Windows.Forms.Button();
            this.btn_pencil = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.MenuBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pen_width)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.colorsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1661, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_file_24;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_opened_folder_26;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_broom_32;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::PaintApplicationAssignment.Properties.Resources.Save_32px;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_close_24;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.ImageToolStripMenuItem_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.colorsToolStripMenuItem.Text = "Colors";
            this.colorsToolStripMenuItem.Click += new System.EventHandler(this.ColorsToolStripMenuItem_Click);
            // 
            // MenuBoxPanel
            // 
            this.MenuBoxPanel.Controls.Add(this.button10);
            this.MenuBoxPanel.Controls.Add(this.button9);
            this.MenuBoxPanel.Controls.Add(this.button8);
            this.MenuBoxPanel.Controls.Add(this.button7);
            this.MenuBoxPanel.Controls.Add(this.button6);
            this.MenuBoxPanel.Controls.Add(this.button5);
            this.MenuBoxPanel.Controls.Add(this.button4);
            this.MenuBoxPanel.Controls.Add(this.button3);
            this.MenuBoxPanel.Controls.Add(this.btnFlip180V);
            this.MenuBoxPanel.Controls.Add(this.button2);
            this.MenuBoxPanel.Controls.Add(this.button1);
            this.MenuBoxPanel.Controls.Add(this.pen_width);
            this.MenuBoxPanel.Controls.Add(this.btn_save);
            this.MenuBoxPanel.Controls.Add(this.btn_clear);
            this.MenuBoxPanel.Controls.Add(this.btn_triangle);
            this.MenuBoxPanel.Controls.Add(this.btn_rectangle);
            this.MenuBoxPanel.Controls.Add(this.btn_ellipse);
            this.MenuBoxPanel.Controls.Add(this.btn_line);
            this.MenuBoxPanel.Controls.Add(this.btn_erase);
            this.MenuBoxPanel.Controls.Add(this.btn_fill);
            this.MenuBoxPanel.Controls.Add(this.btn_color);
            this.MenuBoxPanel.Controls.Add(this.pick_color);
            this.MenuBoxPanel.Controls.Add(this.btn_pencil);
            this.MenuBoxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuBoxPanel.Location = new System.Drawing.Point(0, 28);
            this.MenuBoxPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MenuBoxPanel.Name = "MenuBoxPanel";
            this.MenuBoxPanel.Size = new System.Drawing.Size(184, 963);
            this.MenuBoxPanel.TabIndex = 2;
            // 
            // button10
            // 
            this.button10.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_rotate_right_24;
            this.button10.Location = new System.Drawing.Point(69, 306);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(46, 43);
            this.button10.TabIndex = 22;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_rotate_left_24;
            this.button9.Location = new System.Drawing.Point(12, 306);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(46, 43);
            this.button9.TabIndex = 21;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_zoom_out_30;
            this.button8.Location = new System.Drawing.Point(88, 490);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 35);
            this.button8.TabIndex = 20;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_zoom_in_30;
            this.button7.Location = new System.Drawing.Point(9, 490);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(74, 35);
            this.button7.TabIndex = 19;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(11, 451);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(162, 26);
            this.button6.TabIndex = 18;
            this.button6.Text = "Grayscale Filter";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 418);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(162, 28);
            this.button5.TabIndex = 17;
            this.button5.Text = "Negative Filter";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 382);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(162, 31);
            this.button4.TabIndex = 16;
            this.button4.Text = "Sepia Filter";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 54);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 26);
            this.button3.TabIndex = 15;
            this.button3.Text = "Undo";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnFlip180V
            // 
            this.btnFlip180V.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_flip_vertical_30;
            this.btnFlip180V.Location = new System.Drawing.Point(65, 257);
            this.btnFlip180V.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFlip180V.Name = "btnFlip180V";
            this.btnFlip180V.Size = new System.Drawing.Size(46, 43);
            this.btnFlip180V.TabIndex = 14;
            this.btnFlip180V.UseVisualStyleBackColor = true;
            this.btnFlip180V.Click += new System.EventHandler(this.btnFlip180V_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_flip_horizontal_30;
            this.button2.Location = new System.Drawing.Point(123, 261);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 39);
            this.button2.TabIndex = 13;
            this.toolTip1.SetToolTip(this.button2, "Triangle");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_crop_301;
            this.button1.Location = new System.Drawing.Point(66, 206);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 43);
            this.button1.TabIndex = 12;
            this.toolTip1.SetToolTip(this.button1, "Triangle");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pen_width
            // 
            this.pen_width.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pen_width.Location = new System.Drawing.Point(9, 6);
            this.pen_width.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pen_width.Name = "pen_width";
            this.pen_width.Size = new System.Drawing.Size(50, 22);
            this.pen_width.TabIndex = 11;
            this.toolTip1.SetToolTip(this.pen_width, "Pen And Shapes Width Size");
            this.pen_width.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.pen_width.ValueChanged += new System.EventHandler(this.Pen_width_ValueChanged);
            // 
            // btn_save
            // 
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.Image = global::PaintApplicationAssignment.Properties.Resources.Save_32px;
            this.btn_save.Location = new System.Drawing.Point(9, 257);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(50, 43);
            this.btn_save.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btn_save, "Save Drawing");
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_clear.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_broom_32;
            this.btn_clear.Location = new System.Drawing.Point(123, 94);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(50, 43);
            this.btn_clear.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btn_clear, "Clear Drawing");
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.Btn_clear_Click);
            // 
            // btn_triangle
            // 
            this.btn_triangle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_triangle.Image = global::PaintApplicationAssignment.Properties.Resources.Triangle_32px;
            this.btn_triangle.Location = new System.Drawing.Point(123, 157);
            this.btn_triangle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_triangle.Name = "btn_triangle";
            this.btn_triangle.Size = new System.Drawing.Size(50, 43);
            this.btn_triangle.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btn_triangle, "Triangle");
            this.btn_triangle.UseVisualStyleBackColor = true;
            this.btn_triangle.Click += new System.EventHandler(this.Btn_triangle_Click);
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rectangle.Image = global::PaintApplicationAssignment.Properties.Resources.Rectangle_Stroked_32px;
            this.btn_rectangle.Location = new System.Drawing.Point(9, 157);
            this.btn_rectangle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(50, 43);
            this.btn_rectangle.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btn_rectangle, "Rectangle");
            this.btn_rectangle.UseVisualStyleBackColor = true;
            this.btn_rectangle.Click += new System.EventHandler(this.Btn_rectangle_Click);
            // 
            // btn_ellipse
            // 
            this.btn_ellipse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ellipse.Image = global::PaintApplicationAssignment.Properties.Resources.Circle_32px;
            this.btn_ellipse.Location = new System.Drawing.Point(66, 157);
            this.btn_ellipse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ellipse.Name = "btn_ellipse";
            this.btn_ellipse.Size = new System.Drawing.Size(50, 43);
            this.btn_ellipse.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btn_ellipse, "Ellipse");
            this.btn_ellipse.UseVisualStyleBackColor = true;
            this.btn_ellipse.Click += new System.EventHandler(this.Btn_ellipse_Click);
            // 
            // btn_line
            // 
            this.btn_line.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_line.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_line_24;
            this.btn_line.Location = new System.Drawing.Point(9, 206);
            this.btn_line.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(50, 43);
            this.btn_line.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn_line, "Line");
            this.btn_line.UseVisualStyleBackColor = true;
            this.btn_line.Click += new System.EventHandler(this.Btn_line_Click);
            // 
            // btn_erase
            // 
            this.btn_erase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_erase.Image = global::PaintApplicationAssignment.Properties.Resources.Erase_32px;
            this.btn_erase.Location = new System.Drawing.Point(66, 94);
            this.btn_erase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_erase.Name = "btn_erase";
            this.btn_erase.Size = new System.Drawing.Size(50, 43);
            this.btn_erase.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_erase, "Eraser");
            this.btn_erase.UseVisualStyleBackColor = true;
            this.btn_erase.Click += new System.EventHandler(this.Btn_erase_Click);
            // 
            // btn_fill
            // 
            this.btn_fill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fill.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_fill_color_30;
            this.btn_fill.Location = new System.Drawing.Point(9, 94);
            this.btn_fill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_fill.Name = "btn_fill";
            this.btn_fill.Size = new System.Drawing.Size(50, 43);
            this.btn_fill.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_fill, "Fill Selected Color In Shapes");
            this.btn_fill.UseVisualStyleBackColor = true;
            this.btn_fill.Click += new System.EventHandler(this.Btn_fill_Click);
            // 
            // btn_color
            // 
            this.btn_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_color.Image = global::PaintApplicationAssignment.Properties.Resources.icons8_color_64;
            this.btn_color.Location = new System.Drawing.Point(118, 6);
            this.btn_color.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(50, 43);
            this.btn_color.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_color, "Color Picker");
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.Btn_color_Click);
            // 
            // pick_color
            // 
            this.pick_color.BackColor = System.Drawing.Color.Black;
            this.pick_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pick_color.Location = new System.Drawing.Point(61, 4);
            this.pick_color.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pick_color.Name = "pick_color";
            this.pick_color.Size = new System.Drawing.Size(50, 43);
            this.pick_color.TabIndex = 1;
            this.toolTip1.SetToolTip(this.pick_color, "Current Color For Pen And Shapes");
            this.pick_color.UseVisualStyleBackColor = false;
            // 
            // btn_pencil
            // 
            this.btn_pencil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pencil.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_pencil.Image = global::PaintApplicationAssignment.Properties.Resources.Edit_32px;
            this.btn_pencil.Location = new System.Drawing.Point(123, 208);
            this.btn_pencil.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_pencil.Name = "btn_pencil";
            this.btn_pencil.Size = new System.Drawing.Size(50, 43);
            this.btn_pencil.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_pencil, "Pen");
            this.btn_pencil.UseVisualStyleBackColor = true;
            this.btn_pencil.Click += new System.EventHandler(this.Btn_pencil_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 991);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1661, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // DrawingArea
            // 
            this.DrawingArea.BackColor = System.Drawing.Color.White;
            this.DrawingArea.Location = new System.Drawing.Point(191, 30);
            this.DrawingArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(1470, 961);
            this.DrawingArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DrawingArea.TabIndex = 3;
            this.DrawingArea.TabStop = false;
            this.DrawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            this.DrawingArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseClick);
            this.DrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.DrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.DrawingArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseUp);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.HelpToolStripMenuItem.Text = "Help";
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1603, 925);
            this.Controls.Add(this.DrawingArea);
            this.Controls.Add(this.MenuBoxPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paint Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MenuBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pen_width)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.Panel MenuBoxPanel;
        private System.Windows.Forms.Button btn_pencil;
        private System.Windows.Forms.Button pick_color;
        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Button btn_fill;
        private System.Windows.Forms.Button btn_erase;
        private System.Windows.Forms.Button btn_rectangle;
        private System.Windows.Forms.Button btn_ellipse;
        private System.Windows.Forms.Button btn_line;
        private System.Windows.Forms.Button btn_triangle;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.NumericUpDown pen_width;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnFlip180V;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicEditor
{
	class CuttingPanel : Panel
	{
		Bitmap bitmap;
		Pen pen;
		CoverPanel cover;
		BufferedGraphics BG;

		Boolean ResizeWMode = false;
		Boolean ResizeHMode = false;
		Boolean ResizeAllMode = false;
		Boolean MoveMode = false;
		Int32 loc_dx = 1000000;
		Int32 loc_dy = 1000000;
		Int32 lastX;
		Int32 lastY;

		public CuttingPanel(Bitmap bmp, Pen pn)
		{
			BG = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.ClientRectangle);
			bitmap = bmp;
			pen = pn;
			this.MouseDown += new MouseEventHandler(CuttingPanel_MouseDown);
			this.MouseMove += new MouseEventHandler(CuttingPanel_MouseMove);
			this.MouseUp += new MouseEventHandler(CuttingPanel_MouseUp);
			this.Paint += new PaintEventHandler(CuttingPanel_Paint);
			this.Resize += new EventHandler(CuttingPanel_Resize);
		}

		public Bitmap CurrBitmap { get { return bitmap; } }

		void CuttingPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (e.X > ClientRectangle.Width - 5 && e.Y > ClientRectangle.Height - 5)
					ResizeAllMode = true;
				else if (e.X > ClientRectangle.Width - 5)
					ResizeWMode = true;
				else if (e.Y > ClientRectangle.Height - 5)
					ResizeHMode = true;
				else
				{
					MoveMode = true;
					loc_dx = Location.X - e.X;
					loc_dy = Location.Y - e.Y;
					this.Visible = false;
					cover = new CoverPanel(bitmap, pen);
					cover.Location = this.Location;
					cover.Size = this.Size;
					cover.Parent = this.Parent;
				}
			}
			else
			{
				ResizeWMode = false;
				ResizeHMode = false;
				ResizeAllMode = false;
				MoveMode = false;
				loc_dx = loc_dy = 1000000;
			}
		}

		void CuttingPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.X > ClientRectangle.Width - 5 && e.Y > ClientRectangle.Height - 5)
				this.Cursor = Cursors.SizeNWSE;
			else if (e.X > ClientRectangle.Width - 5)
				this.Cursor = Cursors.SizeWE;
			else if (e.Y > ClientRectangle.Height - 5)
				this.Cursor = Cursors.SizeNS;
			else this.Cursor = Cursors.Default;

			lastX = e.X;
			lastY = e.Y;
			if (ResizeAllMode || ResizeWMode)
			{
				if (e.X > 5)
					this.Width = e.X;
				this.Invalidate();
				//return;
			}
			if (ResizeAllMode || ResizeHMode)
			{
				if (e.Y > 5)
					this.Height = e.Y;
				this.Invalidate();
				return;
			}
			if (MoveMode && loc_dx < 100000 && loc_dy < 100000)
			{
				cover.Location = new Point(e.X + loc_dx, e.Y + loc_dy);
			}
		}

		void CuttingPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (MoveMode && loc_dx < 100000 && loc_dy < 100000)
			{
				MoveMode = false;
				this.Visible = true;
				this.Location = new Point(e.X + loc_dx, e.Y + loc_dy);
				cover.Dispose();
				cover = null;
			}
			ResizeWMode = false;
			ResizeHMode = false;
			ResizeAllMode = false;
			loc_dx = loc_dy = 1000000;
		}
		
		void CuttingPanel_Paint(object sender, PaintEventArgs e)
		{
			BG.Graphics.DrawImage(bitmap, this.ClientRectangle);
			BG.Graphics.DrawRectangle(pen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
			BG.Render();
		}

		void CuttingPanel_Resize(object sender, EventArgs e)
		{
			BG = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.ClientRectangle);
			/*Bitmap bmp_new = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
			using (Graphics gr = Graphics.FromImage(bmp_new))
			{
				gr.DrawImage(bitmap, this.ClientRectangle);
			}
			bitmap.Dispose();
			bitmap = bmp_new;*/
			BG.Graphics.DrawImage(bitmap, this.ClientRectangle);
			BG.Graphics.DrawRectangle(pen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
			BG.Render();
		}
	}
}

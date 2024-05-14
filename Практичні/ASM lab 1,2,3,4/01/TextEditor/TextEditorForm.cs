using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml.Linq;
using System.Drawing.Printing;
using System.IO;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TextEditor
{
    public partial class TextEditorForm : Form
    {
        private string filePath;

        public PrintDialog Dialog { get; set; }
        public PrintDocument Document { get; set; }

        public Encoding CurrentEncoding { get; set; }
        public bool IsFile { get; set; }
        public string CurrentFileName { get; set; }

        public TextEditorForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Dialog = new PrintDialog();
            Document = new PrintDocument();
            Document.PrintPage += new PrintPageEventHandler(PrintEvent);
            IsFile = false;
            CurrentEncoding = Encoding.UTF8;
            checkedListBox1.SetItemChecked(0, true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_Click(sender, e);
            int selectedIndex = checkedListBox1.SelectedIndex;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != selectedIndex)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }

            Encoding encoding;

            switch (selectedIndex)
            {
                case 0:
                    encoding = Encoding.UTF8;
                    break;
                case 1:
                    encoding = Encoding.GetEncoding("windows-1251");
                    break;
                case 2:
                    encoding = Encoding.GetEncoding("ISO-8859-1");
                    break;
                case 3:
                    encoding = Encoding.GetEncoding("IBM866");
                    break;
                case 4:
                    encoding = Encoding.GetEncoding("windows-1254");
                    break;
                default:
                    encoding = Encoding.Default;
                    break;
            }

            ChangeCurrentEncoding(encoding);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "txt";
            dialog.Title = "Create a new file:";
            dialog.FileName = "file";
            dialog.RestoreDirectory = true;
            dialog.AddExtension = true;
            dialog.Filter = "Text files (*.txt)|*.txt";
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                filePath = dialog.FileName;
                richTextBox1.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                System.IO.File.WriteAllText(filePath, richTextBox1.Text);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Ctrl+N - Create a new text file\n Ctrl+O - Oen file\n Ctrl+S - Save the file\n " +
              "Ctrl+Shift+S - Save file as\n Ctrl+P - Print file \n Ctrl+E - Exit\n" +
              " \n The text editor was developed by Olha Kravets, a student of the PMO-41 group (2024)", caption: "Help");
        }


        public void PrintEvent(object sender, PrintPageEventArgs e)
        {
            var myFont = new Font("Times New Roman", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            float leftMargin = e.MarginBounds.Left;
            e.Graphics.DrawString(richTextBox1.Text, myFont, Brushes.Black, leftMargin, 150, new StringFormat());
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.Document = Document;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                Document.Print();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "TXT|*.txt", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var sr = new StreamReader(new FileStream(ofd.FileName, FileMode.Open), CurrentEncoding))
                        {
                            var text = sr.ReadToEnd();
                            richTextBox1.Text = text;

                            CurrentFileName = ofd.FileName;
                            IsFile = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                System.IO.File.WriteAllText(filePath, richTextBox1.Text, CurrentEncoding);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog() { Filter = "TXT|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var streamWriter = new StreamWriter(sfd.FileName, false, CurrentEncoding))
                    {
                        var text = richTextBox1.Text;
                        streamWriter.Write(text);

                        CurrentFileName = sfd.FileName;
                        IsFile = true;
                    }

                    MessageBox.Show("Data saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    IsFile = true;
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
        }

        private void ChangeCurrentEncoding(Encoding encoding)
        {
            CurrentEncoding = encoding;

            if (IsFile)
            {
                using (var sr = new StreamReader(new FileStream(CurrentFileName, FileMode.Open), CurrentEncoding))
                {
                    var text = sr.ReadToEnd();
                    richTextBox1.Text = text;
                }
            }
        }

        private string GetCurrentEncodedText(string text, Encoding prevEncoding)
        {
            byte[] prevBytes = prevEncoding.GetBytes(text);
            byte[] newBytes = Encoding.Convert(prevEncoding, CurrentEncoding, prevBytes);

            return CurrentEncoding.GetString(newBytes);
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            richTextBox1.Text = GetCurrentEncodedText(richTextBox1.Text, CurrentEncoding);
        }
    }
}
